using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SmoothScrollGUI;

public static class RunningApps
{
    public static List<AppInfo> ListAll()
    {
        Process[] processes = Process.GetProcesses();
        int currentProcessId = Process.GetCurrentProcess().Id;
        var result = new List<AppInfo>();
        var seenPaths = new HashSet<string>();

        foreach (Process process in processes)
        {
            try
            {
                string fileName = process.MainModule.FileName;

                if (process.MainWindowTitle.Length == 0 ||
                    !process.Responding ||
                    fileName.Contains(":\\WINDOWS") ||
                    seenPaths.Contains(fileName) ||
                    process.Id == currentProcessId ||
                    fileName.EndsWith("SmoothScrollGUI.exe"))
                {
                    continue;
                }

                seenPaths.Add(fileName);

                string path = GetAppNameForProcess(process.Handle);
                if (string.IsNullOrEmpty(path))
                {
                    path = fileName;
                }

                FileVersionInfo version = process.MainModule.FileVersionInfo;
                string name = version.FileDescription;
                if (string.IsNullOrEmpty(name))
                {
                    name = version.ProductName;
                }
                if (string.IsNullOrEmpty(name))
                {
                    name = Path.GetFileName(path);
                }

                result.Add(new AppInfo(path, name, Icon.ExtractAssociatedIcon(fileName)));
            }
            catch (Exception)
            {
                // The process exited or is not accessible; skip it.
            }
        }

        result.Sort((x, y) => x.Name.CompareTo(y.Name));
        return result;
    }

    public static string GetAppName(string path)
    {
        try
        {
            return GetAppName(path, FileVersionInfo.GetVersionInfo(path));
        }
        catch
        {
            return path + " (not found)";
        }
    }

    public static string GetAppName(string path, FileVersionInfo version)
    {
        string name = version.FileDescription;
        if (string.IsNullOrEmpty(name))
        {
            name = version.ProductName;
        }
        if (string.IsNullOrEmpty(name))
        {
            name = Path.GetFileName(path);
        }

        return name;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool QueryFullProcessImageName(
        [In] IntPtr hProcess,
        [In] int dwFlags,
        [Out] StringBuilder lpExeName,
        ref int lpdwSize);

    private static string GetAppNameForProcess(IntPtr handle)
    {
        int size = 1024;
        var buffer = new StringBuilder(size);
        QueryFullProcessImageName(handle, 0, buffer, ref size);
        return buffer.ToString(0, size);
    }
}
