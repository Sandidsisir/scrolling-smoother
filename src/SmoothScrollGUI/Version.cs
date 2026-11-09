using System.Diagnostics;

namespace SmoothScrollGUI;

internal static class Version
{
    private static FileVersionInfo _versionInfo;

    public static string Full()
    {
        return GetVersionInfo().FileVersion;
    }

    public static string Short()
    {
        FileVersionInfo info = GetVersionInfo();
        return $"{info.FileMajorPart}.{info.FileMinorPart}.{info.FileBuildPart}";
    }

    public static FileVersionInfo GetVersionInfo()
    {
        if (_versionInfo == null)
        {
            _versionInfo = FileVersionInfo.GetVersionInfo(SSConstants.MainExePath());
        }

        return _versionInfo;
    }
}
