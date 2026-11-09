using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PopupControl;

namespace SmoothScrollGUI;

public partial class Form1 : Form
{
    private const string EnableForThisAppText = "Enable for this app";
    private const string EnableByDefaultText = "Enable for all apps by default";
    private const string ReaderPipeName = "SS_Settings_To";
    private const string WriterPipeName = "SS_Settings_From";

    private readonly PiedPiper _pipe = new PiedPiper(ReaderPipeName, WriterPipeName);
    private readonly Options _options = new Options();
    private readonly List<ResetTrio> _resettables = new List<ResetTrio>();

    private System.Threading.Timer _anyChangeTimer;

    public static ToolTip Tooltip;

    public Form1()
    {
        InitializeComponent();

        EnableDoubleBuffering(appPanel);
        EnableDoubleBuffering(appList);

        Text = $"{Application.ProductName} {Version.Short()}";
        InitializeToolTip();
        appList.DrawMode = DrawMode.OwnerDrawFixed;
        CenterToScreen();
        TopMost = false;

        InitializeResetButtons();
        AttachChangeHandlers();
        CenterGlobalOptions();

        _options.Load();
        LoadAppList();
        SelectAppByPath("def");
        LoadGlobalSettings();
        ResolveMissingAppNames();
    }

    private static void EnableDoubleBuffering(Control control)
    {
        typeof(Control)
            .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
            .SetValue(control, true, null);
    }

    private void InitializeResetButtons()
    {
        Control[] numericControls =
        {
            stepSizeBox,
            animationTimeBox,
            accelerationDeltaBox,
            accelerationMaxBox,
            pulseScaleBox
        };

        foreach (Control control in numericControls)
        {
            var trio = new ResetTrio(control) { ResetParent = appPanel };
            trio.UpdateWithXAnchor(appTextBoxesTable, appTextBoxesTable);
            trio.ResetButton.MouseClick += (sender, e) => AppPanelAnythingChanged(trio.Control, EventArgs.Empty);
            _resettables.Add(trio);
        }

        Control[] checkBoxes =
        {
            enabledForAppCheck,
            pulseAlgorithmCheck,
            horizontalSmoothingCheck,
            horizontalShiftKeyCheck,
            reverseDirectionCheck
        };

        Control anchor = Helpers.WidestControl(checkBoxes);
        foreach (Control control in checkBoxes)
        {
            var trio = new ResetTrio(control);
            trio.UpdateWithXAnchor(anchor);
            trio.ResetButton.MouseClick += (sender, e) => AppPanelAnythingChanged(trio.Control, EventArgs.Empty);
            _resettables.Add(trio);
        }
    }

    private void AttachChangeHandlers()
    {
        foreach (Control control in AllAppControls())
        {
            if (control is CheckBox checkBox)
            {
                checkBox.MouseClick += AppPanelAnythingChanged;
                checkBox.KeyUp += AppPanelAnythingChanged;
            }
            else if (control is TextBox textBox)
            {
                textBox.KeyUp += AppPanelAnythingChanged;
            }
        }

        Helpers.ControlClickFocusesOther(stepSizeLabel, stepSizeBox);
        Helpers.ControlClickFocusesOther(animationTimeLabel, animationTimeBox);
        Helpers.ControlClickFocusesOther(accelerationDeltaLabel, accelerationDeltaBox);
        Helpers.ControlClickFocusesOther(accelerationMaxLabel, accelerationMaxBox);
        Helpers.ControlClickFocusesOther(pulseScaleLabel, pulseScaleBox);
    }

    private void CenterGlobalOptions()
    {
        globalFlowPanel.Location = new Point(
            (ClientSize.Width - globalFlowPanel.Width) / 2,
            globalFlowPanel.Location.Y);
    }

    private void LoadAppList()
    {
        appList.Items.Clear();
        appList.Items.Add(new ListItem("Default (All Applications)", "def"));

        foreach (JProperty property in _options.apps.Properties())
        {
            AddAppListItem(property.Name, (JObject)property.Value);
        }
    }

    private void AddAppListItem(string path, JObject app)
    {
        string name = (string)app["name"] ?? path;
        appList.Items.Add(new ListItem(name, path));
    }

    private ListItem SelectAppByPath(string path)
    {
        for (int i = 0; i < appList.Items.Count; i++)
        {
            if (appList.Items[i] is ListItem item && Equals(path, item.Value))
            {
                appList.SelectedIndex = i;
                return item;
            }
        }

        return null;
    }

    private void SetAppListItemText(int index, string text)
    {
        var item = (ListItem)appList.Items[index];
        item.Text = text ?? "";
        appList.Items[index] = item;
    }

    private void ResolveMissingAppNames()
    {
        for (int i = 0; i < appList.Items.Count; i++)
        {
            var item = (ListItem)appList.Items[i];
            string path = (string)item.Value;

            if (item.Text.EndsWith(path))
            {
                string appName = RunningApps.GetAppName(path);
                SetAppListItemText(i, appName);
                ((JObject)_options.apps[path])["name"] = new JValue(appName);
                SendAppSettings(item, updateFromForm: false);
            }
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        ActiveControl = stepSizeLabel;
        TopMost = false;

        appList.ItemHeight = Helpers.TransformToRoundPixels(24.0);
        appList.Size = new Size(appPanel.Size.Width, appList.Size.Height);
        appList.Location = new Point(appList.Location.X, appPanel.Location.Y - appList.Size.Height + 1);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!IsOptionDisabled(_options.global["showMinimizeNote"]))
        {
            DialogResult result = MessageBoxSmart.Show(
                this,
                MBSI.MBSI_INFO,
                "The window will be minized to the tray area.",
                "Windows may hide the application icon.",
                MessageBoxButtons.OK,
                out bool dontShowAgain);

            if (dontShowAgain)
            {
                _options.global["showMinimizeNote"] = new JValue(false);
                SendGlobalSettings();
            }

            if (!MessageBoxSmart.PositiveResult(result))
            {
                e.Cancel = true;
            }
        }
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        _pipe.Stop();
    }

    private static bool IsOptionDisabled(JToken token)
    {
        return token != null && token.Type == JTokenType.Boolean && !token.Value<bool>();
    }

    private void SendAppSettings(ListItem item, bool updateFromForm = true)
    {
        if (item == null)
        {
            return;
        }

        string path = (string)item.Value;
        var message = new JObject();

        if (path == "def")
        {
            if (updateFromForm)
            {
                SaveDefaultOptions(_options.def);
            }

            message["name"] = "SetDefOptions";
            message["def"] = _options.def;
        }
        else
        {
            var app = (JObject)_options.apps[path];
            if (updateFromForm)
            {
                SaveAppOptions(app, _options.def);
            }

            app["path"] = path;
            message["name"] = "SetAppOptions";
            message["path"] = path;
            message["app"] = app;
        }

        _pipe.Send(message);
    }

    private void SendRemovedAppSettings(string path)
    {
        _pipe.Send(new JObject
        {
            ["name"] = "DeleteAppOptions",
            ["path"] = path
        });
    }

    private void SendGlobalSettings()
    {
        SaveGlobalOptions(_options.global);

        _pipe.Send(new JObject
        {
            ["name"] = "SetGlobalOptions",
            ["global"] = _options.global
        });
    }

    private void RequestDefaultSettingsReset()
    {
        _pipe.Send(new JObject
        {
            ["name"] = "ResetDefOptions"
        });
    }

    private void LoadAppSettings(ListItem item)
    {
        LoadAppSettings((string)item.Value);
    }

    private void LoadAppSettings(string appPath)
    {
        LoadAppSettings((JObject)_options.apps[appPath], _options.def);
    }

    private void LoadAppSettings(JObject app, JObject defaults)
    {
        foreach (JProperty property in defaults.Properties())
        {
            string controlName = GetControlName(property.Name, property.Value.Type);
            Control control = GetAppControl(controlName);
            ApplyControlValue(control, app[property.Name], (JValue)property.Value);
        }
    }

    private void LoadDefaultAppSettings()
    {
        LoadAppSettings(_options.def, _options.def);
    }

    private void LoadGlobalSettings()
    {
        foreach (JProperty property in _options.global.Properties())
        {
            string controlName = GetControlName(property.Name, property.Value.Type);
            Control control = globalFlowPanel.Controls[controlName];
            ApplyControlValue(control, property.Value, null);
        }
    }

    // A missing option means "inherit the default", which is represented by the
    // reset state of the matching control.
    private static void ApplyControlValue(Control control, JToken value, JValue defaultValue)
    {
        if (control is CheckBoxPlus checkBox)
        {
            if (value == null)
            {
                checkBox.ResetToDefault();
            }
            else
            {
                checkBox.Checked = value.Value<bool>();
                checkBox.SetToEdited();
            }
        }
        else if (control is TextBoxPlus textBox)
        {
            if (value == null)
            {
                textBox.ResetToDefault();
                return;
            }

            textBox.DefaultText = defaultValue?.ToString("0.######") ?? "";
            textBox.Text = ((JValue)value).ToString("0.######");
            textBox.SetToEdited();
        }
    }

    private void SaveAppOptions(JObject app, JObject defaults)
    {
        foreach (JProperty property in defaults.Properties())
        {
            string controlName = GetControlName(property.Name, property.Value.Type);
            object value = ReadControlValue(GetAppControl(controlName));

            if (value == null)
            {
                app.Remove(property.Name);
            }
            else
            {
                app[property.Name] = new JValue(value);
            }
        }
    }

    private void SaveDefaultOptions(JObject defaults)
    {
        foreach (JProperty property in defaults.Properties())
        {
            string controlName = GetControlName(property.Name, property.Value.Type);
            object value = ReadControlValue(GetAppControl(controlName));

            if (value != null)
            {
                defaults[property.Name] = new JValue(value);
            }
        }
    }

    private void SaveGlobalOptions(JObject global)
    {
        foreach (JProperty property in global.Properties())
        {
            string controlName = GetControlName(property.Name, property.Value.Type);
            object value = ReadControlValue(globalFlowPanel.Controls[controlName]);

            if (value != null)
            {
                global[property.Name] = new JValue(value);
            }
        }
    }

    private static object ReadControlValue(Control control)
    {
        if (control is IResettable resettable && resettable.InDefaultState)
        {
            return null;
        }

        if (control is CheckBox checkBox)
        {
            return checkBox.Checked;
        }

        if (control is TextBox textBox)
        {
            return Convert.ToDouble(textBox.Text);
        }

        return null;
    }

    // Option keys follow the control naming scheme used by the designer:
    // booleans map to *Check controls and numbers map to *Box controls.
    private static string GetControlName(string key, JTokenType type)
    {
        switch (key)
        {
            case "enabled":
                key = "enabledForApp";
                break;
            case "globalEnabled":
                key = "enabledForAllApps";
                break;
        }

        if (type == JTokenType.Boolean)
        {
            return key + "Check";
        }

        if (type == JTokenType.Integer || type == JTokenType.Float)
        {
            return key + "Box";
        }

        return key;
    }

    private void appList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (appList.SelectedIndex > 0)
        {
            var item = appList.SelectedItem as ListItem;
            if (item == null)
            {
                return;
            }

            RemoveAppButton.Enabled = true;
            ResetAllButton.Visible = false;
            SetResetButtonsEnabled(true);
            LoadAppSettings(item);
            enabledForAppCheck.Text = EnableForThisAppText;
            return;
        }

        RemoveAppButton.Enabled = false;
        ResetAllButton.Visible = true;
        SetResetButtonsEnabled(false);
        LoadDefaultAppSettings();
        enabledForAppCheck.Text = EnableByDefaultText;
    }

    private void appList_DropDownClosed(object sender, EventArgs e)
    {
        smoothScrollLabel.Focus();
        BeginInvoke((Action)(() => smoothScrollLabel.Focus()));
    }

    private void AppPanelAnythingChanged(object sender, EventArgs e)
    {
        var selectedItem = appList.SelectedItem as ListItem;
        AsyncUtils.DelayTimeout(
            ref _anyChangeTimer,
            500,
            () => SendAppSettings(selectedItem));
    }

    private void globalControls_AnythingChanged(object sender, EventArgs e)
    {
        AsyncUtils.DelayTimeout(ref _anyChangeTimer, 500, SendGlobalSettings);
    }

    private void enabledForAllAppsCheck_CheckedChanged(object sender, EventArgs e)
    {
        bool enabled = ((CheckBox)sender).Checked;
        appPanel.Enabled = enabled;
        appList.Enabled = enabled;
    }

    private void enabledForAppCheck_CheckedChanged(object sender, EventArgs e)
    {
        var checkBox = (CheckBox)sender;

        if (checkBox.Checked)
        {
            Helpers.EnableControlsBelow(appPanel);
            return;
        }

        Helpers.DisableControls(appPanel);
        Helpers.EnableControlsAbove(checkBox);
    }

    private void BlankSpace_MouseClick(object sender, MouseEventArgs e)
    {
        ActiveControl = null;
    }

    private void AddAppButton_Click(object sender, EventArgs e)
    {
        var existingPaths = new HashSet<string>(
            appList.Items.Cast<ListItem>().Select(item => (string)item.Value));

        List<AppInfo> apps = RunningApps.ListAll().FindAll(app => !existingPaths.Contains(app.Path));

        var list = new ActiveAppsList(apps);
        var popup = new Popup(list);

        list.SelectionDone += ActiveAppsListSelectionDone;
        list.MyParent = popup;
        list.LayoutNow();
        popup.Show((Control)sender, ToolStripDropDownDirection.Left);
    }

    private void ActiveAppsListSelectionDone(object sender, EventArgs e)
    {
        var list = sender as ActiveAppsList;
        AppInfo selected = list?.SelectedItem;
        if (selected == null)
        {
            return;
        }

        var app = new JObject
        {
            ["path"] = selected.Path,
            ["name"] = selected.Name
        };

        _options.apps[selected.Path] = app;
        AddAppListItem(selected.Path, app);
        SelectAppByPath(selected.Path);
    }

    private void RemoveAppButton_Click(object sender, EventArgs e)
    {
        if (appList.SelectedIndex == 0)
        {
            return;
        }

        var item = appList.SelectedItem as ListItem;
        if (item == null)
        {
            return;
        }

        string path = (string)item.Value;
        if (!Helpers.ConfirmDialog(
                "Are you sure you want to remove settings for \n" + item.Text + "?",
                "Removing Settings for App"))
        {
            return;
        }

        int selectedIndex = appList.SelectedIndex;
        appList.Items.RemoveAt(selectedIndex);
        appList.SelectedIndex = selectedIndex - 1;
        _options.apps.Remove(path);
        SendRemovedAppSettings(path);
    }

    private IEnumerable<Control> AllAppControls()
    {
        return appPanel.Controls.Cast<Control>().Concat(appTextBoxesTable.Controls.Cast<Control>());
    }

    private Control GetAppControl(string name)
    {
        return appPanel.Controls[name] ?? appTextBoxesTable.Controls[name];
    }

    private void SetResetButtonsEnabled(bool canReset)
    {
        foreach (ResetTrio resettable in _resettables)
        {
            resettable.IsResettable = canReset;
        }
    }

    private void InitializeToolTip()
    {
        Tooltip = toolTip1;
        toolTip1.AutoPopDelay = 5000;
        toolTip1.InitialDelay = 1000;
        toolTip1.ReshowDelay = 500;
        toolTip1.ShowAlways = true;
    }

    private void ResetAllButton_Click(object sender, EventArgs e)
    {
        RequestDefaultSettingsReset();
        AsyncUtils.DelayCall(1000, () =>
        {
            _options.Load();
            LoadDefaultAppSettings();
        });
    }
}
