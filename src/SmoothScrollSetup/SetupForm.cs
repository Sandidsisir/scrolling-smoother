using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmoothScrollSetup;

internal class SetupForm : Form
{
    private readonly Label _status = new Label();
    private readonly Button _install = new Button();
    private readonly Button _uninstall = new Button();

    public SetupForm()
    {
        Text = "SmoothScroll Setup";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(420, 150);
        Font = new Font("Segoe UI", 9f);

        var heading = new Label
        {
            Text = $"SmoothScroll {Program.Ver}",
            Font = new Font("Segoe UI", 12f, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(14, 12)
        };

        _status.Location = new Point(16, 52);
        _status.AutoSize = true;
        _status.ForeColor = Color.DimGray;

        _install.Text = "Install";
        _install.Size = new Size(120, 34);
        _install.Location = new Point(14, 88);
        _install.Click += (sender, e) =>
            Run(Program.DoInstall, "Installed. SmoothScroll is running — check your tray.");

        _uninstall.Text = "Uninstall";
        _uninstall.Size = new Size(120, 34);
        _uninstall.Location = new Point(146, 88);
        _uninstall.Click += (sender, e) =>
        {
            DialogResult result = MessageBox.Show(
                "Remove SmoothScroll (settings kept)?",
                "Uninstall",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Run(Program.DoUninstall, "Uninstalled. Original settings kept.");
            }
        };

        Controls.AddRange(new Control[] { heading, _status, _install, _uninstall });
        RefreshInstalledState();
    }

    private void RefreshInstalledState()
    {
        bool installed = File.Exists(Path.Combine(Program.AppDir, "SmoothScroll.exe"));
        _uninstall.Enabled = installed;
        _status.Text = installed ? "Currently installed." : "Not installed yet.";
    }

    private void Run(Action<Action<string>> operation, string successMessage)
    {
        _install.Enabled = false;
        _uninstall.Enabled = false;

        try
        {
            operation(message => BeginInvoke((Action)(() => _status.Text = message)));
            MessageBox.Show(successMessage, "SmoothScroll", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "SmoothScroll Setup", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        _install.Enabled = true;
        RefreshInstalledState();
    }
}
