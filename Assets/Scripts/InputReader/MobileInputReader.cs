using System;
using Core.Services;
using InputReader.ButtonHandlers;
using UnityEngine;
using Zenject;

namespace InputReader
{
    public class MobileInputReader : MonoBehaviour, IInputReader
    {
        [SerializeField] private TapButtonHandler pauseButton, ultaButton;
        [SerializeField] private HoldButtonHandler jumpButton, attackButton;
        [SerializeField] private Joystick movementJoystick, rotationJoystick;
        
        private IProjectUpdater _projectUpdater;
        
        [Inject]
        public void Construct(IProjectUpdater projectUpdater)
        {
            _projectUpdater = projectUpdater;
            _projectUpdater.UpdateCalled += OnUpdate;
        }
        
        public event Action JumpClicked;
        public event Action AttackClicked;
        public event Action PauseClicked;
        public event Action UltaClicked;

        public float HorizontalDirection => movementJoystick.Horizontal;
        public float VerticalDirection => movementJoystick.Vertical;
        public float HorizontalRotation => rotationJoystick.Horizontal;
        public float VerticalRotation => rotationJoystick.Vertical;
        public bool Attack => attackButton.IsActive;
        public bool Jump => jumpButton.IsActive;
        public bool Pause => pauseButton.IsActive;
        public bool Ulta => ultaButton.IsActive;
        
        private void OnUpdate()
        {
            if (Attack) AttackClicked?.Invoke();
            if (Jump) JumpClicked?.Invoke();
            if (Ulta) UltaClicked?.Invoke();
            if (Pause) PauseClicked?.Invoke();
        }
        
        public void Dispose()
        {
            _projectUpdater.UpdateCalled += OnUpdate;
        }
    }
}
