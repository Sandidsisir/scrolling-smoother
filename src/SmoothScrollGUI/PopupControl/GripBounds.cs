using System.Drawing;

namespace PopupControl;

internal struct GripBounds
{
    private const int GripSize = 6;
    private const int CornerGripSize = 12;

    private readonly Rectangle _clientRectangle;

    public Rectangle ClientRectangle => _clientRectangle;

    public Rectangle Bottom
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Y = rectangle.Bottom - GripSize + 1;
            rectangle.Height = GripSize;
            return rectangle;
        }
    }

    public Rectangle BottomRight
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Y = rectangle.Bottom - CornerGripSize + 1;
            rectangle.Height = CornerGripSize;
            rectangle.X = rectangle.Width - CornerGripSize + 1;
            rectangle.Width = CornerGripSize;
            return rectangle;
        }
    }

    public Rectangle Top
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Height = GripSize;
            return rectangle;
        }
    }

    public Rectangle TopRight
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Height = CornerGripSize;
            rectangle.X = rectangle.Width - CornerGripSize + 1;
            rectangle.Width = CornerGripSize;
            return rectangle;
        }
    }

    public Rectangle Left
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Width = GripSize;
            return rectangle;
        }
    }

    public Rectangle BottomLeft
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Width = CornerGripSize;
            rectangle.Y = rectangle.Height - CornerGripSize + 1;
            rectangle.Height = CornerGripSize;
            return rectangle;
        }
    }

    public Rectangle Right
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.X = rectangle.Right - GripSize + 1;
            rectangle.Width = GripSize;
            return rectangle;
        }
    }

    public Rectangle TopLeft
    {
        get
        {
            Rectangle rectangle = ClientRectangle;
            rectangle.Width = CornerGripSize;
            rectangle.Height = CornerGripSize;
            return rectangle;
        }
    }

    public GripBounds(Rectangle clientRectangle)
    {
        _clientRectangle = clientRectangle;
    }
}
