namespace SmoothScrollGUI;

public class ListItem
{
    public string Text { get; set; }
    public object Value { get; set; }

    public ListItem(string text, object value)
    {
        Text = text;
        Value = value;
    }

    public override string ToString()
    {
        return Text;
    }
}
