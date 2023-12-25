using System;
using System.Collections.Generic;
using Core;
using Core.Services;
using InputReader;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerEntity : MonoBehaviour, IDisposable
    {
        [SerializeField] private new Camera camera;
        private PlayerMovement _movement;
        private Rigidbody _rigidbody;
        private IInputReader _inputReader;
        private PlayerStats _playerStats;
        private IProjectUpdater _projectUpdater;
        private GroundRaycaster _groundRaycaster;
        private List<IDisposable> _disposables;

        [Inject]
        public void Construct(IProjectUpdater projectUpdater, IInputReader inputReader, PlayerStats playerStats)
        {
            _inputReader = inputReader;
            _playerStats = playerStats;
            _projectUpdater = projectUpdater;
            _disposables = new List<IDisposable>();
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _movement = new PlayerMovement(_rigidbody, _playerStats, camera);
            
            _groundRaycaster = new GroundRaycaster(transform, _projectUpdater);
            _disposables.Add(_groundRaycaster);
            
            // REMOVE THIS
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnFixedUpdate()
        {
            HandleMovement();
            HandleJump();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void HandleMovement()
        {
            _movement.Move(_inputReader.HorizontalDirection, _inputReader.VerticalDirection);
            _movement.Rotate(_inputReader.HorizontalRotation, _inputReader.VerticalRotation);
        }

        private void HandleJump()
        {
            _movement.Jump(_inputReader.Jump, _groundRaycaster.IsGrounded);
        }

        public void Dispose()
        {
            _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
