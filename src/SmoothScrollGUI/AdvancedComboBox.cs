using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SmoothScrollGUI;

public class AdvancedComboBox : ComboBox
{
    private readonly Dictionary<string, Icon> _iconCache = new Dictionary<string, Icon>();

    public AdvancedComboBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        DrawMode = DrawMode.OwnerDrawFixed;
    }

    private Icon IconForExe(string exe)
    {
        if (!_iconCache.TryGetValue(exe, out Icon icon))
        {
            icon = Helpers.IconFromFilePath(exe);
            _iconCache[exe] = icon;
        }

        return icon;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index < 0 || !(Items[e.Index] is ListItem item))
        {
            return;
        }

        e.DrawBackground();
        e.DrawFocusRectangle();

        bool isDefault = (string)item.Value == "def";
        bool disabled = (e.State & DrawItemState.Disabled) == DrawItemState.Disabled;
        bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

        Color background = disabled || selected
            ? Color.LightGray
            : isDefault
                ? Color.FromArgb(251, 251, 251)
                : BackColor;

        using (var brush = new SolidBrush(background))
        {
            e.Graphics.FillRectangle(brush, e.Bounds);
        }

        if (isDefault)
        {
            using (var pen = new Pen(Color.FromArgb(240, 240, 240), Helpers.TransformToRoundPixels(1.0)))
            {
                e.Graphics.DrawLine(
                    pen,
                    new Point(e.Bounds.Left, e.Bounds.Bottom - 1),
                    new Point(e.Bounds.Right, e.Bounds.Bottom - 1));
            }
        }

        Color foreground = disabled ? Color.Gray : ForeColor;
        int padding = Helpers.TransformToRoundPixels(4.0);
        int iconSize = e.Bounds.Height - 2 * padding;
        var iconBounds = new Rectangle(e.Bounds.Left + padding, e.Bounds.Top + padding, iconSize, iconSize);

        if (isDefault)
        {
            int border = Helpers.TransformToRoundPixels(1.0);
            Color color = Color.FromArgb(160, ForeColor);
            int inset = (int)Math.Round(0.25 * iconSize);

            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(color, border))
            {
                e.Graphics.DrawRectangle(
                    pen,
                    new Rectangle(iconBounds.Left + border, iconBounds.Top + border, iconSize - 2 * border, iconSize - 2 * border));
                e.Graphics.FillRectangle(
                    brush,
                    new Rectangle(iconBounds.Left + inset, iconBounds.Top + inset, iconSize - 2 * inset, iconSize - 2 * inset));
            }
        }
        else
        {
            Icon icon = IconForExe(item.Value as string);
            if (icon != null)
            {
                e.Graphics.DrawIcon(icon, iconBounds);
            }
        }

        using (var format = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Near,
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.EllipsisCharacter
        })
        using (var brush = new SolidBrush(foreground))
        {
            int textLeft = e.Bounds.X + 2 * padding + iconSize;
            e.Graphics.DrawString(
                item.Text,
                e.Font,
                brush,
                new Rectangle(textLeft, e.Bounds.Y, e.Bounds.Width - textLeft, e.Bounds.Height),
                format);
        }

        base.OnDrawItem(e);
    }
}
