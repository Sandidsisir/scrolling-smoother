using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmoothScrollGUI;

public class AlignedButton : Button
{
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.HighQuality;

        Color color = Enabled ? ForeColor : Color.FromArgb(80, ForeColor);

        using (var brush = new SolidBrush(color))
        using (var format = new StringFormat())
        {
            int alignment = (int)Math.Log((double)TextAlign, 2.0);
            format.LineAlignment = (StringAlignment)(alignment / 4);
            format.Alignment = (StringAlignment)(alignment % 4);

            var bounds = new Rectangle(0, 0, Width, Height);
            SizeF textSize = graphics.MeasureString(AccessibleName, Font);

            double x = (bounds.Width - textSize.Width) / 2.0 + Padding.Left;
            double y = (bounds.Height - textSize.Height) / 2.0 + Padding.Top;
            bounds.Offset((int)Math.Floor(x), (int)Math.Floor(y));
            bounds.Size = new Size((int)Math.Ceiling(textSize.Width), (int)Math.Ceiling(textSize.Height));

            graphics.DrawString(AccessibleName, Font, brush, bounds, format);
        }
    }
}
