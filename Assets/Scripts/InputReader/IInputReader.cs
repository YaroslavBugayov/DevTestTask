namespace InputReader
{
    public interface IInputReader
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        bool Attack { get;  }
    }
}