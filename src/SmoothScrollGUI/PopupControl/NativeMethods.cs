using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace PopupControl;

internal static class NativeMethods
{
    [Flags]
    internal enum AnimationFlags
    {
        Roll = 0x000000,
        HorizontalPositive = 0x000001,
        HorizontalNegative = 0x000002,
        VerticalPositive = 0x000004,
        VerticalNegative = 0x000008,
        Center = 0x000010,
        Hide = 0x010000,
        Activate = 0x020000,
        Slide = 0x040000,
        Blend = 0x080000,
        Mask = 0x0FFFFF
    }

#pragma warning disable CS0649 // Fields are populated by the marshaller.
    internal struct MINMAXINFO
    {
        public Point reserved;
        public Size maxSize;
        public Point maxPosition;
        public Size minTrackSize;
        public Size maxTrackSize;
    }
#pragma warning restore CS0649

    internal const int WM_NCHITTEST = 0x0084;
    internal const int WM_NCACTIVATE = 0x0086;
    internal const int WM_GETMINMAXINFO = 0x0024;
    internal const int WM_COMMAND = 0x0111;
    internal const int WM_REFLECT = 0x2000;

    internal const int WS_EX_TRANSPARENT = 0x00000020;
    internal const int WS_EX_TOOLWINDOW = 0x00000080;
    internal const int WS_EX_LAYERED = 0x00080000;
    internal const int WS_EX_NOACTIVATE = 0x08000000;

    internal const int HTTRANSPARENT = -1;
    internal const int HTLEFT = 10;
    internal const int HTRIGHT = 11;
    internal const int HTTOP = 12;
    internal const int HTTOPLEFT = 13;
    internal const int HTTOPRIGHT = 14;
    internal const int HTBOTTOM = 15;
    internal const int HTBOTTOMLEFT = 16;
    internal const int HTBOTTOMRIGHT = 17;

    internal const int CBN_DROPDOWN = 7;

    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOMOVE = 0x0002;
    private const int SWP_NOACTIVATE = 0x0010;

    private static readonly HandleRef HwndTopMost = new HandleRef(null, new IntPtr(-1));

    private static bool? _isRunningOnMono;

    public static bool IsRunningOnMono
    {
        get
        {
            if (!_isRunningOnMono.HasValue)
            {
                _isRunningOnMono = Type.GetType("Mono.Runtime") != null;
            }

            return _isRunningOnMono.Value;
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [SuppressUnmanagedCodeSecurity]
    private static extern int AnimateWindow(HandleRef windowHandle, int time, AnimationFlags flags);

    internal static void AnimateWindow(Control control, int time, AnimationFlags flags)
    {
        try
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
            AnimateWindow(new HandleRef(control, control.Handle), time, flags);
        }
        catch (SecurityException)
        {
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    [SuppressUnmanagedCodeSecurity]
    private static extern bool SetWindowPos(
        HandleRef hWnd,
        HandleRef hWndInsertAfter,
        int x,
        int y,
        int cx,
        int cy,
        int flags);

    internal static void SetTopMost(Control control)
    {
        try
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
            SetWindowPos(
                new HandleRef(control, control.Handle),
                HwndTopMost,
                0,
                0,
                0,
                0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }
        catch (SecurityException)
        {
        }
    }

    internal static int HIWORD(int value)
    {
        return (short)((value >> 16) & 0xFFFF);
    }

    internal static int HIWORD(IntPtr value)
    {
        return HIWORD((int)(long)value);
    }

    internal static int LOWORD(int value)
    {
        return (short)(value & 0xFFFF);
    }

    internal static int LOWORD(IntPtr value)
    {
        return LOWORD((int)(long)value);
    }
}
