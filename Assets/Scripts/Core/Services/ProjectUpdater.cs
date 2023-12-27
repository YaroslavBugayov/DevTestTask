using System;
using UnityEngine;

namespace Core.Services
{
    public class ProjectUpdater : MonoBehaviour, IProjectUpdater
    {
        public event Action<bool> PauseStateChanged;
        public event Action UpdateCalled;
        public event Action FixedUpdateCalled;
        public event Action LateUpdateCalled;
        public event Action UpdateForPauseCalled;

        private bool _isPaused;

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (_isPaused == value)
                    return;
                
                Time.timeScale = value ? 0 : 1;
                _isPaused = value;
                PauseStateChanged?.Invoke(_isPaused);
            }
        }
        
        private void Update()
        {
            UpdateForPause();
            if (_isPaused) return;
            UpdateCalled?.Invoke();
        }

        private void FixedUpdate()
        {
            if (_isPaused) return;
            FixedUpdateCalled?.Invoke();
        }

        private void LateUpdate()
        {
            if (_isPaused) return;
            LateUpdateCalled?.Invoke();
        }

        private void UpdateForPause()
        {
            UpdateForPauseCalled?.Invoke();
        }
    }
}
