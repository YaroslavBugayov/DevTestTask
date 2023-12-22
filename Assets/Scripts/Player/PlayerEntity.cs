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
            _movement = new PlayerMovement(_rigidbody, _playerStats);
        }

        private void OnFixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            _movement.Move(_inputReader.HorizontalDirection, _inputReader.VerticalDirection);
        }
    }
}
