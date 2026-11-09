using System;

namespace PopupControl;

[Flags]
public enum PopupAnimations
{
    None = 0,
    LeftToRight = 0x000001,
    RightToLeft = 0x000002,
    TopToBottom = 0x000004,
    BottomToTop = 0x000008,
    Center = 0x000010,
    Slide = 0x040000,
    Blend = 0x080000,
    Roll = 0x100000,
    SystemDefault = 0x200000
}
