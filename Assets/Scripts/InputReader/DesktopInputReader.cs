using System;
using Core.Services;
using UnityEngine;
using Zenject;

namespace InputReader
{
    public class DesktopInputReader : IInputReader
    {
        [Inject]
        public void Construct(IProjectUpdater projectUpdater)
        {
            projectUpdater.UpdateCalled += OnUpdate;
        }
        
        public float HorizontalDirection => Input.GetAxis("Horizontal");
        public float VerticalDirection => Input.GetAxis("Vertical");
        public float HorizontalRotation => Input.GetAxis("Mouse X");
        public float VerticalRotation => Input.GetAxis("Mouse Y");
        public bool Attack { get; private set; }

        private void OnUpdate()
        {
            Attack = Input.GetButton("Fire1");
        }
    }
}
