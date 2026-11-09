using System;
using System.Drawing;
using System.Windows.Forms;
using SmoothScrollGUI.Properties;

namespace SmoothScrollGUI;

public partial class MessageBoxFormSmart : Form
{
    private readonly Bitmap[] _icons =
    {
        Resources.info2_100px,
        Resources.error_100px,
        Resources.warning_100px,
        Resources.help_100px,
        Resources.check2_100px
    };

    public Color colorPanelBorder = Color.FromArgb(223, 223, 223);

    public bool DontShowAgainChecked
    {
        get { return dontShowCheck.Checked; }
    }

    public MessageBoxFormSmart(string title, string detail, MessageBoxButtons buttons)
    {
        InitializeComponent();

        Text = $"{Application.ProductName} {Application.ProductVersion}";
        titleLabel.Text = title;
        detailLabel.Text = detail;
        MinimumSize = new Size(Width, Height);
        MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        InitButtons(buttons);
    }

    public void InitButtons(MessageBoxButtons buttons)
    {
        if (buttons == MessageBoxButtons.OK)
        {
            acceptButton.Text = "Ok";
            cancelButton.Visible = false;
            acceptButton.Location = cancelButton.Location;
        }
    }

    public void SetIcon(Image image)
    {
        pictureBox1.Image = image;
    }

    public void SetIcon(MBSI icon)
    {
        Bitmap bitmap = _icons[(int)icon];
        if (bitmap != null)
        {
            SetIcon(bitmap);
        }
    }

    private void panelBottom_Paint(object sender, PaintEventArgs e)
    {
        using (var pen = new Pen(colorPanelBorder, 2f))
        {
            e.Graphics.DrawRectangle(pen, e.ClipRectangle);
        }
    }

    private void acceptButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }
}
