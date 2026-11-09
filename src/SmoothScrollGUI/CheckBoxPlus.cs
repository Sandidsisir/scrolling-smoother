using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmoothScrollGUI;

public class CheckBoxPlus : CheckBox, IResettable
{
    public bool InDefaultState { get; set; }

    public event EventHandler<EventArgs> DefaultStateChanged;

    public CheckBoxPlus()
    {
        SetStyle(ControlStyles.Opaque, true);
    }

    public void ResetToDefault()
    {
        InDefaultState = true;
        ThreeState = true;
        CheckState = CheckState.Indeterminate;
        Invalidate();
        PublishDefaultStateChange();
    }

    public void SetToEdited()
    {
        InDefaultState = false;
        ThreeState = false;
        CheckState = Checked ? CheckState.Checked : CheckState.Unchecked;
        Invalidate();
        PublishDefaultStateChange();
    }

    private void PublishDefaultStateChange()
    {
        DefaultStateChanged?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        SetToEdited();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (!InDefaultState)
        {
            return;
        }

        using (var brush = new SolidBrush(Color.FromArgb(Enabled ? 100 : 160, BackColor)))
        {
            Helpers.TransformToPixels(15.0, 0.0, out int pixelX, out _);
            e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pixelX, Size.Height));
        }
    }
}
