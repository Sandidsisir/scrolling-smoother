using System;
using System.Windows.Forms;

namespace SmoothScrollGUI;

public partial class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
        versionLabel.Text = $"Version {Version.Short()}";
        ActiveControl = smoothScrollLabel;
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
