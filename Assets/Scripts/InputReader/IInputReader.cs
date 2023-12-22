namespace InputReader
{
    public interface IInputReader
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        float HorizontalRotation { get; }
        float VerticalRotation { get; }
        bool Attack { get; }
        bool Jump { get; }
    }
}