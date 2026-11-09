using System.Windows.Forms;

namespace SmoothScrollGUI;

public static class MessageBoxSmart
{
    public static DialogResult Show(Form owner, MBSI icon, string title, string detail, MessageBoxButtons buttons, out bool dontShowAgain)
    {
        using (var form = new MessageBoxFormSmart(title, detail, buttons))
        {
            form.Icon = owner.Icon;
            form.SetIcon(icon);
            DialogResult result = form.ShowDialog(owner);
            dontShowAgain = form.DontShowAgainChecked;
            return result;
        }
    }

    public static bool PositiveResult(DialogResult result)
    {
        return result == DialogResult.OK || result == DialogResult.Yes;
    }
}
