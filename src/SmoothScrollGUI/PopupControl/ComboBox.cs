using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PopupControl;

[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
[Description("Displays an editable text box with a drop-down list of permitted values.")]
[ToolboxItemFilter("System.Windows.Forms")]
[ToolboxItem(true)]
public class ComboBox : System.Windows.Forms.ComboBox
{
    private static Type _modalMenuFilter;
    private static MethodInfo _suspendMenuMode;
    private static MethodInfo _resumeMenuMode;

    private static Type ModalMenuFilter
    {
        get
        {
            if (_modalMenuFilter == null)
            {
                _modalMenuFilter = Type.GetType("System.Windows.Forms.ToolStripManager+ModalMenuFilter");
            }

            if (_modalMenuFilter == null)
            {
                _modalMenuFilter = new List<Type>(typeof(ToolStripManager).Assembly.GetTypes())
                    .Find(type => type.FullName == "System.Windows.Forms.ToolStripManager+ModalMenuFilter");
            }

            return _modalMenuFilter;
        }
    }

    private static MethodInfo SuspendMenuModeMethod
    {
        get
        {
            if (_suspendMenuMode == null && ModalMenuFilter != null)
            {
                _suspendMenuMode = ModalMenuFilter.GetMethod(
                    "SuspendMenuMode",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            return _suspendMenuMode;
        }
    }

    private static MethodInfo ResumeMenuModeMethod
    {
        get
        {
            if (_resumeMenuMode == null && ModalMenuFilter != null)
            {
                _resumeMenuMode = ModalMenuFilter.GetMethod(
                    "ResumeMenuMode",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            return _resumeMenuMode;
        }
    }

    private static void SuspendMenuMode()
    {
        SuspendMenuModeMethod?.Invoke(null, null);
    }

    private static void ResumeMenuMode()
    {
        ResumeMenuModeMethod?.Invoke(null, null);
    }

    protected override void OnDropDown(EventArgs e)
    {
        base.OnDropDown(e);
        SuspendMenuMode();
    }

    protected override void OnDropDownClosed(EventArgs e)
    {
        ResumeMenuMode();
        base.OnDropDownClosed(e);
    }
}
