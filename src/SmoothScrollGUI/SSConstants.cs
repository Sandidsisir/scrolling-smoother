using System;
using System.IO;
using System.Reflection;

namespace SmoothScrollGUI;

public static class SSConstants
{
    public const string MainExeName = "SmoothScroll.exe";

    public static string AppDataFolder(string file = "")
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SmoothScroll",
            file);
    }

    public static string ExeFolder(string file = "")
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
    }

    public static string MainExePath()
    {
        string path = ExeFolder(MainExeName);
        if (File.Exists(path))
        {
            return path;
        }

        Assembly entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        return entryAssembly.Location;
    }
}
