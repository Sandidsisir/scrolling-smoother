using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PopupControl;

namespace SmoothScrollGUI;

public partial class ActiveAppsList : UserControl
{
    private readonly List<AppInfo> _apps;
    private readonly List<RadioButton> _buttons = new List<RadioButton>();

    public AppInfo SelectedItem;
    public Control MyParent;

    public event EventHandler SelectionDone;

    public ActiveAppsList()
        : this(null)
    {
    }

    public ActiveAppsList(List<AppInfo> apps)
    {
        InitializeComponent();

        _apps = apps;
        Width = Helpers.TransformToRoundPixels(420.0);
        radioButtonDummy.Visible = false;
        radioButtonDummy.Size = new Size(Width, Helpers.TransformToRoundPixels(33.0));
    }

    public void LayoutNow()
    {
        int count = 0;
        int padding = Helpers.TransformToRoundPixels(6.0);
        int itemHeight = radioButtonDummy.Size.Height;
        int iconSize = itemHeight - 2 * padding;

        SuspendLayout();

        foreach (AppInfo app in _apps)
        {
            RadioButton button = radioButtonDummy.Clone();
            button.Text = " " + app.Name;
            button.Visible = true;
            button.Image = Helpers.ResizeImage(app.Icon.ToBitmap(), iconSize, iconSize);
            button.Location = new Point(0, count * (itemHeight - 1));
            button.FlatAppearance.BorderColor = Color.White;
            button.FlatAppearance.MouseDownBackColor = Color.LightGray;
            button.MouseClick += radioButton_Click;
            Controls.Add(button);
            _buttons.Add(button);
            count++;
        }

        RadioButton browseButton = radioButtonDummy.Clone();
        browseButton.Text = "Browse...";
        browseButton.Image = null;
        browseButton.Visible = true;
        browseButton.Location = new Point(0, count * (itemHeight - 1));
        browseButton.BackColor = Color.FromArgb(240, 240, 240);
        browseButton.FlatAppearance.BorderColor = Color.FromArgb(240, 240, 240);
        browseButton.FlatAppearance.MouseDownBackColor = Color.LightGray;
        browseButton.MouseClick += browseButton_Click;
        Controls.Add(browseButton);
        count++;

        int height = count * (itemHeight - 1);

        if (MyParent is Popup parentPopup)
        {
            parentPopup.Size = new Size(parentPopup.Size.Width, height);
        }

        if (Parent is Popup controlPopup)
        {
            controlPopup.Size = new Size(controlPopup.Size.Width, height);
        }

        ResumeLayout();
    }

    private void radioButton_Click(object sender, EventArgs e)
    {
        AsyncUtils.DelayCall(150, () =>
        {
            if (Parent is Popup popup)
            {
                popup.Close();
            }
        });

        if (sender is RadioButton button)
        {
            button.Select();
            int index = _buttons.FindIndex(candidate => candidate == button);
            SelectedItem = index > -1 ? _apps[index] : null;
        }

        SelectionDone?.Invoke(this, EventArgs.Empty);
    }

    private void browseButton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            SelectedItem = new AppInfo(openFileDialog1.FileName);
            SelectionDone?.Invoke(this, EventArgs.Empty);
        }

        AsyncUtils.DelayCall(150, () =>
        {
            if (Parent is Popup popup)
            {
                popup.Close();
            }
        });
    }
}
