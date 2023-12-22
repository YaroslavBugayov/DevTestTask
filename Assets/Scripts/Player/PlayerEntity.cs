using System;
using Core.Services;
using InputReader;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        private PlayerMovement _movement;
        private Rigidbody _rigidbody;
        private IInputReader _inputReader;
        private PlayerStats _playerStats;

        [Inject]
        public void Construct(IProjectUpdater projectUpdater, IInputReader inputReader, PlayerStats playerStats)
        {
            _inputReader = inputReader;
            _playerStats = playerStats;
            projectUpdater.FixedUpdateCalled += OnFixedUpdate;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _movement = new PlayerMovement(_rigidbody, _playerStats, camera);
            
            // REMOVE THIS
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnFixedUpdate()
        {
            HandleMovement();
            HandleJump();
        }

        private void HandleMovement()
        {
            _movement.Move(_inputReader.HorizontalDirection, _inputReader.VerticalDirection);
            _movement.Rotate(_inputReader.HorizontalRotation, _inputReader.VerticalRotation);
        }

        private void HandleJump()
        {
            _movement.Jump(_inputReader.Jump);
        }
    }
}
