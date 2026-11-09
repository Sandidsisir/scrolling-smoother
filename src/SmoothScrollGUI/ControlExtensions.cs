using System;
using System.Reflection;
using System.Windows.Forms;

namespace SmoothScrollGUI;

public static class ControlExtensions
{
    public static T Clone<T>(this T source) where T : Control
    {
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        T clone = Activator.CreateInstance<T>();

        foreach (PropertyInfo property in properties)
        {
            if (property.CanWrite && property.Name != "WindowTarget")
            {
                property.SetValue(clone, property.GetValue(source, null), null);
            }
        }

        return clone;
    }
}
