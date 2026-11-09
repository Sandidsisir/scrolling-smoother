using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WinFormAnimation;

namespace SmoothScrollGUI;

public class ResetButton : Button
{
    private readonly Animator _animator = new Animator();
    private int _backOpacity;
    private double _paddingPercent;

    public Color BaseBackgroundColor = Color.Transparent;

    public int Opacity
    {
        get { return _backOpacity; }
        set
        {
            _backOpacity = value;
            Invalidate();
        }
    }

    public double PaddingPercent
    {
        get { return _paddingPercent; }
        set
        {
            _paddingPercent = value;
            Invalidate();
        }
    }

    public ResetButton()
    {
        SetStyle(ControlStyles.Opaque, true);
    }

    protected void AnimateOpacity(int target, ulong duration = 100uL)
    {
        _animator.Stop();
        _animator.Paths = PathExtensions.ToArray(new Path(Opacity, target, duration));
        _animator.Play(this, Animator.KnownProperties.Opacity);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        AnimateOpacity(20);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        AnimateOpacity(0);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        AnimateOpacity(40);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        AnimateOpacity(20);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics graphics = e.Graphics;
        graphics.Clear(BackColor);
        graphics.SmoothingMode = SmoothingMode.HighQuality;

        using (var backgroundBrush = new SolidBrush(BaseBackgroundColor))
        using (var overlayBrush = new SolidBrush(Color.FromArgb(Opacity, Color.Black)))
        {
            var bounds = new Rectangle(0, 0, Width, Height);
            graphics.FillEllipse(backgroundBrush, bounds);
            graphics.FillEllipse(overlayBrush, bounds);
        }

        int padding = (int)Math.Round(Width * PaddingPercent);
        var imageBounds = new Rectangle(
            padding,
            padding,
            Width - 2 * padding + Padding.Left,
            Height - 2 * padding + Padding.Top);

        DrawImageWithAlpha(graphics, Image, Enabled ? 1f : 0.5f, imageBounds.Size, imageBounds.Location);
    }

    private static void DrawImageWithAlpha(Graphics graphics, Image image, float alpha, Size size, Point offset)
    {
        if (image == null)
        {
            return;
        }

        var matrix = new ColorMatrix { Matrix33 = alpha };

        using (var attributes = new ImageAttributes())
        {
            attributes.SetColorMatrix(matrix);
            graphics.DrawImage(
                image,
                new Rectangle(offset.X, offset.Y, size.Width, size.Height),
                0,
                0,
                image.Width,
                image.Height,
                GraphicsUnit.Pixel,
                attributes);
        }
    }
}
