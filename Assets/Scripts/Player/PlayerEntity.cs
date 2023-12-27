using System;
using System.Collections.Generic;
using Bullet.Factory;
using Core;
using Core.Services;
using InputReader;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerEntity : MonoBehaviour, IDisposable, IDamageable, ILosingStrength
    {
        [SerializeField] private new Camera camera;
        
        private Rigidbody _rigidbody;
        
        private PlayerMovement _movement;
        private PlayerStats _playerStats;
        private PlayerShooting _shooting;
        
        private IInputReader _inputReader;
        private IProjectUpdater _projectUpdater;
        private GroundRaycaster _groundRaycaster;
        private BulletFactory _bulletFactory;
        
        private List<IDisposable> _disposables;

        [Inject]
        public void Construct(
            IProjectUpdater projectUpdater,
            IInputReader inputReader,
            PlayerStats playerStats,
            BulletFactory bulletFactory)
        {
            _inputReader = inputReader;
            _playerStats = playerStats;
            _bulletFactory = bulletFactory;
            _projectUpdater = projectUpdater;
            
            _disposables = new List<IDisposable>();
            
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;
            _projectUpdater.PauseStateChanged += SetCursor;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _movement = new PlayerMovement(_rigidbody, _playerStats, camera);
            _shooting = new PlayerShooting(_playerStats, camera, _bulletFactory, transform);
            
            _groundRaycaster = new GroundRaycaster(transform, _projectUpdater);
            _disposables.Add(_groundRaycaster);

            _inputReader.AttackClicked += HandleShooting;
            _inputReader.JumpClicked += HandleJump;
            _inputReader.PauseClicked += HandlePause;
            _inputReader.UltaClicked += HandleUlta;
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnFixedUpdate() => HandleMovement();

        public void TakeDamage(int damage) => _playerStats.TakeDamage(damage);
        
        public void TakeDamageToStrength(int strengthDamage) => _playerStats.TakeDamageToStrength(strengthDamage);
        
        private void HandleMovement()
        {
            _movement.Move(_inputReader.HorizontalDirection, _inputReader.VerticalDirection);
            _movement.Rotate(_inputReader.HorizontalRotation, _inputReader.VerticalRotation);
        }
        
        private void HandleJump() => _movement.Jump(_inputReader.Jump, _groundRaycaster.IsGrounded);
        
        private void HandleShooting() => _shooting.Shoot(_inputReader.Attack);
        
        private void HandleUlta() => _shooting.Ulta();
        
        private void HandlePause() => _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
        
        private void SetCursor(bool isPause) =>
            Cursor.lockState = isPause ? CursorLockMode.Confined : CursorLockMode.Locked;
        
        private void OnDestroy() => Dispose();
        
        public void Dispose()
        {
            _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
            _inputReader.AttackClicked -= HandleShooting;
            _inputReader.JumpClicked -= HandleJump;
            _inputReader.PauseClicked -= HandlePause;
            _inputReader.UltaClicked -= HandleUlta;
            
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
