using System;

namespace InputReader
{
    public interface IInputReader: IDisposable
    {
        event Action JumpClicked;
        event Action AttackClicked;
        
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        float HorizontalRotation { get; }
        float VerticalRotation { get; }
        bool Attack { get; }
        bool Jump { get; }
    }
}