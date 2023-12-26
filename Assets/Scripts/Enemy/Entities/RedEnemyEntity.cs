using System;
using System.Collections;
using System.Collections.Generic;
using Bullet.Enums;
using Core.Services;
using Enemy.Enums;
using Enemy.Services;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy.Entities
{
    public class RedEnemyEntity : MonoBehaviour, IEnemyEntity, IDamageable, IDisposable
    {
        public int Health { get; private set; } = 50;
        private const int MaxHeath = 50;
        private const int Damage = 15;
        private Transform _player;
        private IProjectUpdater _projectUpdater;
        private ICollisionHandler _collisionHandler;
        private RedEnemyStateService _stateService;
        private RedEnemyMovement _movement;
        
        private List<IDisposable> _disposables;
        
        [Inject]
        public void Construct(PlayerEntity playerEntity, IProjectUpdater projectUpdater)
        {
            _player = playerEntity.transform;
            _projectUpdater = projectUpdater;
            _stateService = new RedEnemyStateService();
            _disposables = new List<IDisposable>();
        }
        
        private void Awake()
        {
            _collisionHandler = new RedEnemyCollisionHandler(gameObject, Damage);
            _movement = new RedEnemyMovement(gameObject, _stateService, _projectUpdater, _player);
            _disposables.Add(_movement);
        }

        private void Start() => StartCoroutine(nameof(StateChanger));

        private void OnCollisionEnter(Collision collision) => _collisionHandler.HandleCollision(collision);

        public void TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, MaxHeath);
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator StateChanger()
        {
            _stateService.SetState(RedEnemyState.FlyingUp);
            yield return new WaitForSeconds(2f);
            _stateService.SetState(RedEnemyState.Pursuit);
        }
        
        private void OnDestroy() => Dispose();

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}