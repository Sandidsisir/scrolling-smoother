using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SmoothScrollSetup;

internal static class Program
{
    internal const string Ver = "1.2.4.0";

    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

    private static readonly string Root = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SmoothScroll");

    internal static readonly string AppDir = Path.Combine(Root, "app-" + Ver);

    private static readonly string ExePath = Path.Combine(AppDir, "SmoothScroll.exe");

    private static readonly string BackupDir = Path.Combine(Root, "orig_backup-" + Ver);

    [STAThread]
    private static int Main(string[] args)
    {
        bool silent = Array.Exists(args, arg => arg.Equals("/silent", StringComparison.OrdinalIgnoreCase));
        bool uninstall = Array.Exists(args, arg => arg.Equals("/uninstall", StringComparison.OrdinalIgnoreCase));

        if (silent)
        {
            try
            {
                if (uninstall)
                {
                    DoUninstall(null);
                }
                else
                {
                    DoInstall(null);
                }

                return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    Directory.CreateDirectory(Root);
                    File.AppendAllText(
                        Path.Combine(Root, "setup_error.txt"),
                        DateTime.Now + "\r\n" + ex + "\r\n\r\n");
                }
                catch
                {
                }

                return 1;
            }
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new SetupForm());
        return 0;
    }

    internal static void DoInstall(Action<string> log)
    {
        Kill("SmoothScroll");
        Kill("SmoothScrollGUI");

        BackupExistingInstall(log);

        Directory.CreateDirectory(AppDir);
        ClearAppDir();
        ExtractPayload();
        Log(log, "Files installed to " + AppDir);

        using (RegistryKey run = Registry.CurrentUser.CreateSubKey(RunKey))
        {
            run.SetValue("SmoothScroll", "\"" + ExePath + "\" --startup");
        }

        MakeShortcut(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Programs),
            "SmoothScroll.lnk"));

        MakeShortcut(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "SmoothScroll.lnk"));

        Process.Start(ExePath, "--install");
        Process.Start(Path.Combine(AppDir, "SmoothScrollGUI.exe"), "warmup");
        Log(log, "Done.");
    }

    internal static void DoUninstall(Action<string> log)
    {
        Kill("SmoothScroll");
        Kill("SmoothScrollGUI");

        using (RegistryKey run = Registry.CurrentUser.OpenSubKey(RunKey, writable: true))
        {
            run?.DeleteValue("SmoothScroll", throwOnMissingValue: false);
        }

        DeleteIfExists(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Programs),
            "SmoothScroll.lnk"));

        DeleteIfExists(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "SmoothScroll.lnk"));

        string startMenuFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Programs),
            "SmoothScroll");

        if (Directory.Exists(startMenuFolder))
        {
            Directory.Delete(startMenuFolder, recursive: true);
        }

        Log(log, "Shortcuts and autostart removed.");

        if (Directory.Exists(BackupDir))
        {
            Directory.CreateDirectory(AppDir);
            ClearAppDir();
            CopyDirectoryContents(BackupDir, AppDir);
            Log(log, "Original files restored from backup.");
        }
        else if (Directory.Exists(AppDir))
        {
            Directory.Delete(AppDir, recursive: true);
            Log(log, "App folder removed.");
        }

        // Keep HKCU\Software\SmoothScroll and %APPDATA%\SmoothScroll so the
        // user's settings survive a reinstall.
        Log(log, "Done.");
    }

    private static void BackupExistingInstall(Action<string> log)
    {
        if (!File.Exists(ExePath) || Directory.Exists(BackupDir))
        {
            return;
        }

        Directory.CreateDirectory(BackupDir);

        foreach (string file in Directory.GetFiles(AppDir))
        {
            File.Copy(file, Path.Combine(BackupDir, Path.GetFileName(file)), overwrite: true);
        }

        Log(log, "Original install backed up.");
    }

    private static void ExtractPayload()
    {
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("payload.zip"))
        using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                string destination = Path.Combine(
                    AppDir,
                    entry.FullName.Replace('/', Path.DirectorySeparatorChar));

                Directory.CreateDirectory(Path.GetDirectoryName(destination));
                entry.ExtractToFile(destination, overwrite: true);
            }
        }
    }

    private static void CopyDirectoryContents(string sourceDir, string destinationDir)
    {
        foreach (string file in Directory.GetFiles(sourceDir))
        {
            File.Copy(file, Path.Combine(destinationDir, Path.GetFileName(file)), overwrite: true);
        }
    }

    private static void ClearAppDir()
    {
        if (!Directory.Exists(AppDir))
        {
            return;
        }

        string currentExe = Path.GetFullPath(Application.ExecutablePath);

        foreach (string file in Directory.GetFiles(AppDir))
        {
            if (!string.Equals(Path.GetFullPath(file), currentExe, StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(file);
            }
        }

        foreach (string directory in Directory.GetDirectories(AppDir))
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    private static void Kill(string processName)
    {
        foreach (Process process in Process.GetProcessesByName(processName))
        {
            try
            {
                process.Kill();
                process.WaitForExit(3000);
            }
            catch
            {
            }
        }
    }

    private static void MakeShortcut(string linkPath)
    {
        dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
        dynamic shortcut = shell.CreateShortcut(linkPath);

        shortcut.TargetPath = ExePath;
        shortcut.WorkingDirectory = AppDir;
        shortcut.Description = "SmoothScroll";
        shortcut.Save();
    }

    private static void DeleteIfExists(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private static void Log(Action<string> log, string message)
    {
        log?.Invoke(message);
    }
}
