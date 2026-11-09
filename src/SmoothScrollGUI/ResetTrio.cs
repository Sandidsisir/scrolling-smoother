using System;
using System.Drawing;
using System.Windows.Forms;
using SmoothScrollGUI.Properties;

namespace SmoothScrollGUI;

public class ResetTrio
{
    public Button ResetButton;
    public IResettable Resettable;
    public Control Control;
    public Control ResetParent;

    public bool IsResettable { get; set; } = true;

    public ResetTrio(Control control)
    {
        Control = control;
        Resettable = control as IResettable;
    }

    public void UpdateWithXAnchor(Control anchorX, Control anchorY = null)
    {
        System.Version osVersion = Environment.OSVersion.Version;
        bool isWindows7 = osVersion.Major == 6 && osVersion.Minor == 1;

        Helpers.TransformToPixels(22.0, 22.0, out int width, out int height);

        var button = new ResetButton
        {
            Size = new Size(width, height),
            Name = "stepSizeReseter2",
            TabIndex = 7,
            Text = isWindows7 ? "X" : "\u21BA",
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(40, 40, 40),
            Font = new Font("Segoe UI", 14f, FontStyle.Regular, GraphicsUnit.Point, 0),
            UseVisualStyleBackColor = true,
            Visible = false,
            Padding = new Padding(1, 1, 0, 0),
            PaddingPercent = 0.1,
            Image = Resources.restore_34px_7
        };

        int yOffset = (Control.Size.Height - button.Size.Height) / 2;
        int xOffset = Helpers.TransformToRoundPixels(7.0);
        Point location = new Point(
            anchorX.Location.X + anchorX.Size.Width + xOffset,
            Control.Location.Y + yOffset);

        if (anchorY != null)
        {
            location.Offset(0, anchorY.Location.Y);
        }

        button.Location = location;

        Form1.Tooltip.SetToolTip(button, "Reset to default");
        (ResetParent ?? Control.Parent).Controls.Add(button);

        ResetButton = button;
        button.MouseClick += (sender, e) =>
        {
            Resettable.ResetToDefault();
            button.Visible = false;
        };

        if (Resettable != null)
        {
            Resettable.DefaultStateChanged += OnDefaultStateChanged;
        }
    }

    private void OnDefaultStateChanged(object sender, EventArgs e)
    {
        UpdateButton();
    }

    private void UpdateButton()
    {
        Control.BeginInvoke((Action)UpdateButtonSync);
    }

    private void UpdateButtonSync()
    {
        ResetButton.Visible = IsResettable && !Resettable.InDefaultState;
    }
}
