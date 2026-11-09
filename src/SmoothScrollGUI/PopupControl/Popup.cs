using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PopupControl;

[ToolboxItem(false)]
public class Popup : ToolStripDropDown
{
    private ToolStripControlHost _host;
    private Control _opener;
    private Popup _ownerPopup;
    private Popup _childPopup;
    private bool _resizableTop;
    private bool _resizableLeft;
    private bool _isChildPopupOpened;
    private bool _resizable;
    private bool _nonInteractive;
    private VisualStyleRenderer _sizeGripRenderer;

    public Control Content { get; private set; }

    public PopupAnimations ShowingAnimation { get; set; }
    public PopupAnimations HidingAnimation { get; set; }
    public int AnimationDuration { get; set; }
    public bool FocusOnOpen { get; set; }
    public bool AcceptAlt { get; set; }

    public bool Resizable
    {
        get => _resizable && !_isChildPopupOpened;
        set => _resizable = value;
    }

    public bool NonInteractive
    {
        get => _nonInteractive;
        set
        {
            if (value == _nonInteractive)
            {
                return;
            }

            _nonInteractive = value;
            if (IsHandleCreated)
            {
                RecreateHandle();
            }
        }
    }

    public new Size MinimumSize { get; set; }
    public new Size MaximumSize { get; set; }

    protected override CreateParams CreateParams
    {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        get
        {
            CreateParams createParams = base.CreateParams;
            createParams.ExStyle |= NativeMethods.WS_EX_NOACTIVATE;

            if (NonInteractive)
            {
                createParams.ExStyle |=
                    NativeMethods.WS_EX_TRANSPARENT |
                    NativeMethods.WS_EX_TOOLWINDOW |
                    NativeMethods.WS_EX_LAYERED;
            }

            return createParams;
        }
    }

    public Popup(Control content)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        Content = content;
        FocusOnOpen = true;
        AcceptAlt = true;
        ShowingAnimation = PopupAnimations.SystemDefault;
        HidingAnimation = PopupAnimations.None;
        AnimationDuration = 100;

        AutoSize = false;
        DoubleBuffered = true;
        ResizeRedraw = true;

        _host = new ToolStripControlHost(content);
        Padding = Margin = _host.Padding = _host.Margin = Padding.Empty;

        if (NativeMethods.IsRunningOnMono)
        {
            content.Margin = Padding.Empty;
        }

        MinimumSize = content.MinimumSize;
        content.MinimumSize = content.Size;
        MaximumSize = content.MaximumSize;
        content.MaximumSize = content.Size;
        Size = content.Size;

        if (NativeMethods.IsRunningOnMono)
        {
            _host.Size = content.Size;
        }

        TabStop = content.TabStop = true;
        content.Location = Point.Empty;
        Items.Add(_host);

        content.Disposed += (sender, e) => Dispose(true);
        content.RegionChanged += (sender, e) => UpdateRegion();
        content.Paint += (sender, e) => PaintSizeGrip(e);

        UpdateRegion();
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);

        if (NativeMethods.IsRunningOnMono ||
            (Visible && ShowingAnimation == PopupAnimations.None) ||
            (!Visible && HidingAnimation == PopupAnimations.None))
        {
            return;
        }

        NativeMethods.AnimationFlags animationFlags = Visible
            ? NativeMethods.AnimationFlags.Roll
            : NativeMethods.AnimationFlags.Hide;

        PopupAnimations animations = Visible ? ShowingAnimation : HidingAnimation;

        if (animations == PopupAnimations.SystemDefault)
        {
            if (!SystemInformation.IsMenuAnimationEnabled)
            {
                animations = PopupAnimations.None;
            }
            else if (!SystemInformation.IsMenuFadeEnabled)
            {
                animations = PopupAnimations.Slide |
                    (Visible ? PopupAnimations.TopToBottom : PopupAnimations.BottomToTop);
            }
            else
            {
                animations = PopupAnimations.Blend;
            }
        }

        if ((animations & (PopupAnimations.Center | PopupAnimations.Slide | PopupAnimations.Blend | PopupAnimations.Roll)) == 0)
        {
            return;
        }

        if (_resizableTop)
        {
            if ((animations & PopupAnimations.BottomToTop) != 0)
            {
                animations = (animations & ~PopupAnimations.BottomToTop) | PopupAnimations.TopToBottom;
            }
            else if ((animations & PopupAnimations.TopToBottom) != 0)
            {
                animations = (animations & ~PopupAnimations.TopToBottom) | PopupAnimations.BottomToTop;
            }
        }

        if (_resizableLeft)
        {
            if ((animations & PopupAnimations.RightToLeft) != 0)
            {
                animations = (animations & ~PopupAnimations.RightToLeft) | PopupAnimations.LeftToRight;
            }
            else if ((animations & PopupAnimations.LeftToRight) != 0)
            {
                animations = (animations & ~PopupAnimations.LeftToRight) | PopupAnimations.RightToLeft;
            }
        }

        animationFlags = (NativeMethods.AnimationFlags)(
            (int)animationFlags |
            ((int)animations & (int)NativeMethods.AnimationFlags.Mask));

        NativeMethods.SetTopMost(this);
        NativeMethods.AnimateWindow(this, AnimationDuration, animationFlags);
    }

    [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (AcceptAlt && (keyData & Keys.Alt) == Keys.Alt)
        {
            if ((keyData & Keys.F4) != Keys.F4)
            {
                return false;
            }

            Close();
        }

        bool handled = base.ProcessDialogKey(keyData);

        if (!handled && (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift)))
        {
            bool backwards = (keyData & Keys.Shift) == Keys.Shift;
            Content.SelectNextControl(null, !backwards, true, true, true);
        }

        return handled;
    }

    protected void UpdateRegion()
    {
        if (Region != null)
        {
            Region.Dispose();
            Region = null;
        }

        if (Content.Region != null)
        {
            Region = Content.Region.Clone();
        }
    }

    public void Show(Control control, ToolStripDropDownDirection direction = ToolStripDropDownDirection.BelowRight)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        Show(control, control.ClientRectangle, direction);
    }

    public void Show(Rectangle area)
    {
        ShowInternal(null, area, ToolStripDropDownDirection.BelowRight);
    }

    public void Show(Control control, Rectangle area, ToolStripDropDownDirection direction = ToolStripDropDownDirection.BelowRight)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        SetOwnerItem(control);
        ShowInternal(control, area, direction);
    }

    private void ShowInternal(Control control, Rectangle area, ToolStripDropDownDirection direction)
    {
        _resizableTop = _resizableLeft = false;

        Rectangle workingArea = Screen.FromControl(control ?? this).WorkingArea;
        Point location;

        if (direction == ToolStripDropDownDirection.Left || direction == ToolStripDropDownDirection.Right)
        {
            location = new Point(area.Left, area.Top + area.Height / 2);
            location.Offset(0, -Size.Height / 2);
        }
        else
        {
            location = new Point(area.Left, area.Top + area.Height);
        }

        if (control != null)
        {
            location = control.PointToScreen(location);
        }

        if (location.X + Size.Width > workingArea.Left + workingArea.Width)
        {
            _resizableLeft = true;
            location.X = workingArea.Left + workingArea.Width - Size.Width;
        }

        if (location.Y + Size.Height > workingArea.Top + workingArea.Height)
        {
            _resizableTop = true;
            location.Y -= Size.Height + area.Height;
        }

        if (control != null)
        {
            location = control.PointToClient(location);
        }

        Show(control, location, direction);
    }

    private void SetOwnerItem(Control control)
    {
        if (control == null)
        {
            return;
        }

        if (control is Popup popup)
        {
            _ownerPopup = popup;
            popup._childPopup = this;
            OwnerItem = popup.Items[0];
            return;
        }

        if (_opener == null)
        {
            _opener = control;
        }

        if (control.Parent != null)
        {
            SetOwnerItem(control.Parent);
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        if (Content != null)
        {
            Content.MinimumSize = Size;
            Content.MaximumSize = Size;
            Content.Size = Size;
            Content.Location = Point.Empty;
        }

        base.OnSizeChanged(e);
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        if (!NativeMethods.IsRunningOnMono)
        {
            base.OnLayout(e);
            return;
        }

        Size preferredSize = GetPreferredSize(Size.Empty);
        if (AutoSize && preferredSize != Size)
        {
            Size = preferredSize;
        }

        SetDisplayedItems();
        OnLayoutCompleted(EventArgs.Empty);
        Invalidate();
    }

    protected override void OnOpening(CancelEventArgs e)
    {
        if (Content.IsDisposed || Content.Disposing)
        {
            e.Cancel = true;
            return;
        }

        UpdateRegion();
        base.OnOpening(e);
    }

    protected override void OnOpened(EventArgs e)
    {
        if (_ownerPopup != null)
        {
            _ownerPopup._isChildPopupOpened = true;
        }

        if (FocusOnOpen)
        {
            Content.Focus();
        }

        base.OnOpened(e);
    }

    protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
    {
        _opener = null;

        if (_ownerPopup != null)
        {
            _ownerPopup._isChildPopupOpened = false;
        }

        base.OnClosed(e);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
        if (!InternalProcessResizing(ref m, false))
        {
            base.WndProc(ref m);
        }
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public bool ProcessResizing(ref Message m)
    {
        return InternalProcessResizing(ref m, true);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    private bool InternalProcessResizing(ref Message m, bool contentControl)
    {
        if (m.Msg == NativeMethods.WM_NCACTIVATE &&
            m.WParam != IntPtr.Zero &&
            _childPopup != null &&
            _childPopup.Visible)
        {
            _childPopup.Hide();
        }

        if (!Resizable && !NonInteractive)
        {
            return false;
        }

        if (m.Msg == NativeMethods.WM_NCHITTEST)
        {
            return OnNcHitTest(ref m, contentControl);
        }

        if (m.Msg == NativeMethods.WM_GETMINMAXINFO)
        {
            return OnGetMinMaxInfo(ref m);
        }

        return false;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    private bool OnGetMinMaxInfo(ref Message m)
    {
        var info = (NativeMethods.MINMAXINFO)Marshal.PtrToStructure(
            m.LParam,
            typeof(NativeMethods.MINMAXINFO));

        if (!MaximumSize.IsEmpty)
        {
            info.maxTrackSize = MaximumSize;
        }

        info.minTrackSize = MinimumSize;
        Marshal.StructureToPtr(info, m.LParam, false);
        return true;
    }

    private bool OnNcHitTest(ref Message m, bool contentControl)
    {
        if (NonInteractive)
        {
            m.Result = (IntPtr)NativeMethods.HTTRANSPARENT;
            return true;
        }

        Point point = PointToClient(Cursor.Position);
        var gripBounds = new GripBounds(contentControl ? Content.ClientRectangle : ClientRectangle);

        if (_resizableTop)
        {
            if (_resizableLeft && gripBounds.TopLeft.Contains(point))
            {
                m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTTOPLEFT;
                return true;
            }

            if (!_resizableLeft && gripBounds.TopRight.Contains(point))
            {
                m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTTOPRIGHT;
                return true;
            }

            if (gripBounds.Top.Contains(point))
            {
                m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTTOP;
                return true;
            }
        }
        else
        {
            if (_resizableLeft && gripBounds.BottomLeft.Contains(point))
            {
                m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTBOTTOMLEFT;
                return true;
            }

            if (!_resizableLeft && gripBounds.BottomRight.Contains(point))
            {
                m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTBOTTOMRIGHT;
                return true;
            }

            if (gripBounds.Bottom.Contains(point))
            {
                m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTBOTTOM;
                return true;
            }
        }

        if (_resizableLeft && gripBounds.Left.Contains(point))
        {
            m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTLEFT;
            return true;
        }

        if (!_resizableLeft && gripBounds.Right.Contains(point))
        {
            m.Result = contentControl ? (IntPtr)NativeMethods.HTTRANSPARENT : (IntPtr)NativeMethods.HTRIGHT;
            return true;
        }

        return false;
    }

    public void PaintSizeGrip(PaintEventArgs e)
    {
        if (e == null || e.Graphics == null || !_resizable)
        {
            return;
        }

        Size clientSize = Content.ClientSize;

        using (var image = new Bitmap(16, 16))
        {
            using (Graphics graphics = Graphics.FromImage(image))
            {
                if (Application.RenderWithVisualStyles)
                {
                    if (_sizeGripRenderer == null)
                    {
                        _sizeGripRenderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
                    }

                    _sizeGripRenderer.DrawBackground(graphics, new Rectangle(0, 0, 16, 16));
                }
                else
                {
                    ControlPaint.DrawSizeGrip(graphics, Content.BackColor, 0, 0, 16, 16);
                }
            }

            GraphicsState state = e.Graphics.Save();
            e.Graphics.ResetTransform();

            if (_resizableTop)
            {
                if (_resizableLeft)
                {
                    e.Graphics.RotateTransform(180f);
                    e.Graphics.TranslateTransform(-clientSize.Width, -clientSize.Height);
                }
                else
                {
                    e.Graphics.ScaleTransform(1f, -1f);
                    e.Graphics.TranslateTransform(0f, -clientSize.Height);
                }
            }
            else if (_resizableLeft)
            {
                e.Graphics.ScaleTransform(-1f, 1f);
                e.Graphics.TranslateTransform(-clientSize.Width, 0f);
            }

            e.Graphics.DrawImage(image, clientSize.Width - 16, clientSize.Height - 16 + 1, 16, 16);
            e.Graphics.Restore(state);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && Content != null)
        {
            Control content = Content;
            Content = null;
            content.Dispose();
        }

        base.Dispose(disposing);
    }
}
