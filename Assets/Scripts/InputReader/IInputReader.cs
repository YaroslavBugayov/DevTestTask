using System;

namespace InputReader
{
    public interface IInputReader: IDisposable
    {
        event Action JumpClicked;
        event Action AttackClicked;
        event Action PauseClicked;
        event Action UltaClicked;
        
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        float HorizontalRotation { get; }
        float VerticalRotation { get; }
        bool Attack { get; }
        bool Jump { get; }
        bool Pause { get; }
        bool Ulta { get; }
    }
}