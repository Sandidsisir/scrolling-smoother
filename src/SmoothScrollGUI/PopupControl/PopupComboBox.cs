using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace PopupControl;

[Description("Displays an editable text box with a drop-down list of permitted values.")]
[ToolboxItemFilter("System.Windows.Forms")]
[ToolboxItem(true)]
[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
public class PopupComboBox : ComboBox
{
    private Popup _dropDown;
    private Control _dropDownControl;
    private DateTime _dropDownHideTime;

    public Control DropDownControl
    {
        get => _dropDownControl;
        set
        {
            if (_dropDownControl == value)
            {
                return;
            }

            _dropDownControl = value;

            if (_dropDown != null)
            {
                _dropDown.Closed -= dropDown_Closed;
                _dropDown.Dispose();
            }

            _dropDown = new Popup(value);
            _dropDown.Closed += dropDown_Closed;
        }
    }

    public new bool DroppedDown
    {
        get => _dropDown.Visible;
        set
        {
            if (DroppedDown)
            {
                HideDropDown();
            }
            else
            {
                ShowDropDown();
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new int DropDownWidth
    {
        get => base.DropDownWidth;
        set => base.DropDownWidth = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new int DropDownHeight
    {
        get => base.DropDownHeight;
        set => base.DropDownHeight = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool IntegralHeight
    {
        get => base.IntegralHeight;
        set => base.IntegralHeight = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new ObjectCollection Items => base.Items;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new int ItemHeight
    {
        get => base.ItemHeight;
        set => base.ItemHeight = value;
    }

    public new event EventHandler DropDown;
    public new event EventHandler DropDownClosed;

    public PopupComboBox()
    {
        _dropDownHideTime = DateTime.UtcNow;
        DropDownHeight = DropDownWidth = 1;
        IntegralHeight = false;
    }

    private void dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        _dropDownHideTime = DateTime.UtcNow;
    }

    public void ShowDropDown()
    {
        if (_dropDown == null)
        {
            return;
        }

        if ((DateTime.UtcNow - _dropDownHideTime).TotalSeconds > 0.5)
        {
            DropDown?.Invoke(this, EventArgs.Empty);
            _dropDown.Show(this);
        }
        else
        {
            _dropDownHideTime = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(1));
            Focus();
        }
    }

    public void HideDropDown()
    {
        if (_dropDown == null)
        {
            return;
        }

        _dropDown.Hide();
        DropDownClosed?.Invoke(this, EventArgs.Empty);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == NativeMethods.WM_REFLECT + NativeMethods.WM_COMMAND &&
            NativeMethods.HIWORD(m.WParam) == NativeMethods.CBN_DROPDOWN)
        {
            BeginInvoke(new MethodInvoker(ShowDropDown));
            return;
        }

        base.WndProc(ref m);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && _dropDown != null)
        {
            _dropDown.Dispose();
        }

        base.Dispose(disposing);
    }
}
