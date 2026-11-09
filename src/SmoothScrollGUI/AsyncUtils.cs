using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SmoothScrollGUI;

internal static class AsyncUtils
{
    public static void DelayCall(int milliseconds, Action action)
    {
        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        Task.Run(() =>
        {
            Thread.Sleep(milliseconds);
            dispatcher.BeginInvoke(action);
        });
    }

    public static Timer SetTimeout(int milliseconds, Action action)
    {
        Timer timer = null;
        timer = new Timer(_ =>
        {
            action();
            timer.Dispose();
        }, null, milliseconds, -1);
        return timer;
    }

    public static void DelayTimeout(ref Timer timer, int milliseconds, Action action)
    {
        timer?.Dispose();
        timer = SetTimeout(milliseconds, action);
    }
}
