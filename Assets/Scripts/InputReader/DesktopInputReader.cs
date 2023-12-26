using System;
using Core.Services;
using UnityEngine;
using Zenject;

namespace InputReader
{
    public class DesktopInputReader : IInputReader
    {
        public event Action JumpClicked;
        public event Action AttackClicked;
        
        private IProjectUpdater _projectUpdater;
        
        [Inject]
        public void Construct(IProjectUpdater projectUpdater)
        {
            _projectUpdater = projectUpdater;
            _projectUpdater.UpdateCalled += OnUpdate;
        }
        
        public float HorizontalDirection => Input.GetAxis("Horizontal");
        public float VerticalDirection => Input.GetAxis("Vertical");
        public float HorizontalRotation => Input.GetAxis("Mouse X");
        public float VerticalRotation => Input.GetAxis("Mouse Y");
        public bool Attack { get; private set; }
        public bool Jump { get; private set; }

        private void OnUpdate()
        {
            Attack = Input.GetButton("Fire1");
            Jump = Input.GetButton("Jump");

            if (Attack) AttackClicked?.Invoke();
            if (Jump) JumpClicked?.Invoke();
        }
        
        public void Dispose()
        {
            _projectUpdater.UpdateCalled -= OnUpdate;
        }
    }
}
