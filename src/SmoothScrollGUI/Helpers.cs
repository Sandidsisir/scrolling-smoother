using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmoothScrollGUI;

internal static class Helpers
{
    private const int LOGPIXELSX = 88;
    private const int LOGPIXELSY = 90;

    private static readonly Dictionary<string, Icon> IconExtensionCache = new Dictionary<string, Icon>();

    public static bool ConfirmDialog(string text, string caption, MessageBoxButtons buttons = MessageBoxButtons.YesNo)
    {
        return MessageBox.Show(text, caption, buttons) == DialogResult.Yes;
    }

    public static void DisableControls(Control control)
    {
        foreach (Control child in control.Controls)
        {
            DisableControls(child);
        }

        control.Enabled = false;
    }

    public static void EnableControlsAbove(Control control)
    {
        if (control == null)
        {
            return;
        }

        control.Enabled = true;
        EnableControlsAbove(control.Parent);
    }

    public static void EnableControlsBelow(Control control)
    {
        if (control == null)
        {
            return;
        }

        control.Enabled = true;
        foreach (Control child in control.Controls)
        {
            EnableControlsBelow(child);
        }
    }

    public static void ControlClickFocusesOther(Control clickTarget, Control focusTarget)
    {
        clickTarget.MouseClick += (sender, e) => FocusAndSelect(focusTarget);
    }

    public static void FocusAndSelect(Control control)
    {
        if (control is TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, 0);
        }
    }

    public static Control WidestControl(Control[] controls)
    {
        Control widest = controls[0];
        int width = 0;

        foreach (Control control in controls)
        {
            if (control.Size.Width > width)
            {
                width = control.Size.Width;
                widest = control;
            }
        }

        return widest;
    }

    [DllImport("gdi32.dll")]
    private static extern int GetDeviceCaps(IntPtr hdc, int index);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

    public static double TransformToPixels(double unit)
    {
        IntPtr dc = GetDC(IntPtr.Zero);
        if (dc == IntPtr.Zero)
        {
            return unit;
        }

        try
        {
            return (int)(GetDeviceCaps(dc, LOGPIXELSX) / 96.0 * unit);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, dc);
        }
    }

    public static int TransformToRoundPixels(double unit)
    {
        return (int)Math.Round(TransformToPixels(unit));
    }

    public static void TransformToPixels(double unitX, double unitY, out int pixelX, out int pixelY)
    {
        IntPtr dc = GetDC(IntPtr.Zero);
        if (dc == IntPtr.Zero)
        {
            throw new ArgumentNullException("Failed to get DC.");
        }

        try
        {
            pixelX = (int)(GetDeviceCaps(dc, LOGPIXELSX) / 96.0 * unitX);
            pixelY = (int)(GetDeviceCaps(dc, LOGPIXELSY) / 96.0 * unitY);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, dc);
        }
    }

    public static Bitmap ResizeImage(Image image, int width, int height)
    {
        var destination = new Rectangle(0, 0, width, height);
        var bitmap = new Bitmap(width, height);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var attributes = new ImageAttributes())
            {
                attributes.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        return bitmap;
    }

    public static Icon LoadIconFromExtension(string extension)
    {
        if (IconExtensionCache.TryGetValue(extension, out Icon cached))
        {
            return cached;
        }

        string path = Path.Combine(Path.GetTempPath(), "dummy" + extension);
        using (File.Create(path))
        {
        }

        Icon icon = Icon.ExtractAssociatedIcon(path);
        File.Delete(path);
        IconExtensionCache[extension] = icon;
        return icon;
    }

    public static Icon IconFromFilePath(string filePath, bool defaultExe = true)
    {
        Icon icon = null;

        try
        {
            icon = Icon.ExtractAssociatedIcon(filePath);
        }
        catch (Exception)
        {
            string extension = Path.GetExtension(filePath);
            if (extension == "" && defaultExe)
            {
                extension = ".exe";
            }

            if (extension != "")
            {
                icon = LoadIconFromExtension(extension);
            }
        }

        return icon;
    }
}
