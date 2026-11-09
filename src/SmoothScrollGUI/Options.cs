using System.IO;
using Newtonsoft.Json.Linq;

namespace SmoothScrollGUI;

public class Options
{
    public JObject apps;
    public JObject def;
    public JObject global;

    public void Load()
    {
        JObject root = JObject.Parse(File.ReadAllText(SSConstants.AppDataFolder("options.json")));
        apps = root["apps"] as JObject ?? new JObject();
        def = (JObject)root["def"];
        global = (JObject)root["global"];
    }
}
