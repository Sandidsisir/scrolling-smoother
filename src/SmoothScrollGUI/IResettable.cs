using System;

namespace SmoothScrollGUI;

public interface IResettable
{
    bool InDefaultState { get; set; }

    event EventHandler<EventArgs> DefaultStateChanged;

    void ResetToDefault();

    void SetToEdited();
}
