using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SmoothScrollGUI;

public class TextBoxPlus : TextBox, IResettable
{
    private const int EM_SETCUEBANNER = 0x1501;

    public bool NumericOnly = true;
    public bool BoldWhenEdited = true;
    public string DefaultText = "";
    public string EditedFontName = "Segoe UI Semibold";
    public string NonEditedFontName = "Segoe UI";

    public bool InDefaultState { get; set; }

    public event EventHandler<EventArgs> DefaultStateChanged;

    [Browsable(true)]
    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public override bool AutoSize
    {
        get { return base.AutoSize; }
        set { base.AutoSize = value; }
    }

    [DefaultValue(false)]
    public override bool Multiline
    {
        get { return base.Multiline; }
        set
        {
            base.Multiline = value;
            base.AutoSize = !base.Multiline;
        }
    }

    public TextBoxPlus()
    {
        SetStyle(ControlStyles.Opaque, true);
        AutoSize = true;
        Multiline = false;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);

        SendMessage(Handle, EM_SETCUEBANNER, 0, DefaultText);

        if (NumericOnly)
        {
            Text = Regex.Replace(Text, "[^.0-9]", "");
        }

        if (Text == "")
        {
            ResetToDefault();
        }
        else
        {
            SetToEdited();
        }
    }

    public void ResetToDefault()
    {
        SendMessage(Handle, EM_SETCUEBANNER, 0, DefaultText);

        if (Text != "")
        {
            Text = "";
        }

        InDefaultState = true;
        if (BoldWhenEdited)
        {
            Font = new Font(NonEditedFontName, Font.Size, FontStyle.Regular);
        }

        Invalidate();
        PublishDefaultStateChange();
    }

    public void SetToEdited()
    {
        InDefaultState = false;
        if (BoldWhenEdited)
        {
            Font = new Font(EditedFontName, Font.Size, FontStyle.Bold);
        }

        Invalidate();
        PublishDefaultStateChange();
    }

    private void PublishDefaultStateChange()
    {
        DefaultStateChanged?.Invoke(this, EventArgs.Empty);
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
}
