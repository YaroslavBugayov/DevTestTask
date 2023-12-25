using UnityEngine;

namespace InputReader
{
    public class MobileInputReader : IInputReader
    {
        public float HorizontalDirection { get; private set; }
        public float VerticalDirection { get; private set; }
        public float HorizontalRotation { get; private set; }
        public float VerticalRotation { get; private set;  }
        public bool Attack { get; private set; }
        public bool Jump { get; }
        public void Dispose()
        {
            
        }
    }
}
