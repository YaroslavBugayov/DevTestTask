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
        public event Action PauseClicked;
        public event Action UltaClicked;
        
        private IProjectUpdater _projectUpdater;
        
        [Inject]
        public void Construct(IProjectUpdater projectUpdater)
        {
            _projectUpdater = projectUpdater;
            _projectUpdater.UpdateCalled += OnUpdate;
            _projectUpdater.UpdateForPauseCalled += OnUpdateForPause;
            
            Cursor.lockState = CursorLockMode.Locked;
            _projectUpdater.PauseStateChanged += SetCursor;
        }
        
        public float HorizontalDirection => Input.GetAxis("Horizontal");
        public float VerticalDirection => Input.GetAxis("Vertical");
        public float HorizontalRotation => Input.GetAxis("Mouse X");
        public float VerticalRotation => Input.GetAxis("Mouse Y");
        public bool Attack => Input.GetButton("Fire1");
        public bool Jump => Input.GetButton("Jump");
        public bool Pause => Input.GetButtonDown("Pause");
        public bool Ulta => Input.GetButtonDown("Ulta");
        
        private void OnUpdate()
        {
            if (Attack) AttackClicked?.Invoke();
            if (Jump) JumpClicked?.Invoke();
            if (Ulta) UltaClicked?.Invoke();
        }

        private void OnUpdateForPause()
        {
            if (Pause) PauseClicked?.Invoke();
        }
        
        private void SetCursor(bool isPause) =>
            Cursor.lockState = isPause ? CursorLockMode.Confined : CursorLockMode.Locked;
        
        public void Dispose()
        {
            _projectUpdater.UpdateCalled -= OnUpdate;
            _projectUpdater.UpdateForPauseCalled -= OnUpdateForPause;
            _projectUpdater.PauseStateChanged -= SetCursor;
        }
    }
}
