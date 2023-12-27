using System;
using UnityEngine;

namespace InputReader
{
    public class MobileInputReader : IInputReader
    {
        public event Action JumpClicked;
        public event Action AttackClicked;
        public event Action PauseClicked;
        public event Action UltaClicked;
        
        public float HorizontalDirection { get; private set; }
        public float VerticalDirection { get; private set; }
        public float HorizontalRotation { get; private set; }
        public float VerticalRotation { get; private set;  }
        public bool Attack { get; private set; }
        public bool Jump { get; private set; }
        public bool Pause { get; private set; }
        public bool Ulta { get; private set; }
        
        public void Dispose()
        {
            
        }
    }
}
