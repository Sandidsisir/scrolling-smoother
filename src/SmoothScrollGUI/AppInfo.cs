using System.Drawing;

namespace SmoothScrollGUI;

public class AppInfo
{
    private string _name;

    public string Path;
    public Icon Icon;

    public string Name
    {
        get { return _name ?? (_name = RunningApps.GetAppName(Path)); }
        set { _name = value; }
    }

    public AppInfo(string path = null, string name = null, Icon icon = null)
    {
        Name = name;
        Path = path;
        Icon = icon;
    }
}
