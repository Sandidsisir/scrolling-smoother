using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SmoothScrollGUI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Costura.AssemblyLoader.Attach();

        if (Environment.OSVersion.Version.Major >= 6)
        {
            SetProcessDPIAware();
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        AppDomain.CurrentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
        Application.ThreadException += GlobalThreadExceptionHandler;

        string[] args = Environment.GetCommandLineArgs();
        if (args.Contains("warmup"))
        {
            return;
        }

        Application.Run(args.Contains("aboutWindow") ? new AboutForm() : new Form1());
    }

    private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        var exception = (Exception)e.ExceptionObject;
        File.AppendAllText(SSConstants.AppDataFolder("error.txt"), exception.Message + "\n" + exception.StackTrace);
        ShowThreadExceptionDialog("SmoothScroll Error", exception, MessageBoxButtons.OK);
    }

    private static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
    {
        File.AppendAllText(SSConstants.AppDataFolder("error.txt"), e.Exception.Message + "\n" + e.Exception.StackTrace);
        if (ShowThreadExceptionDialog("SmoothScroll Error", e.Exception) == DialogResult.Abort)
        {
            Application.Exit();
        }
    }

    private static DialogResult ShowThreadExceptionDialog(string title, Exception exception, MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore)
    {
        string message =
            "An application error occurred. The details below were written to the local error log:\n\n" +
            exception.Message + "\n\nStack Trace:\n" + exception.StackTrace;

        return MessageBox.Show(message, title, buttons, MessageBoxIcon.Hand);
    }

    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();
}
