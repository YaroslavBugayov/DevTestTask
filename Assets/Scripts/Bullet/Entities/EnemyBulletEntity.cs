using System;
using Bullet.Services;
using Core.Services;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Bullet.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyBulletEntity : MonoBehaviour, IBulletEntity, IDisposable, IDamageable
    {
        private const int StrengthDamage = 25;
        private const float Speed = 3.5f;
        private IProjectUpdater _projectUpdater;
        private Transform _playerTransform;
        private ICollisionHandler _collisionHandler;

        [Inject]
        public void Construct(IProjectUpdater projectUpdater, PlayerEntity playerEntity)
        {
            _projectUpdater = projectUpdater;
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;

            _playerTransform = playerEntity.transform;

            _collisionHandler = new EnemyBulletCollisionHandler(gameObject, StrengthDamage);
        }

        private void OnFixedUpdate()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _playerTransform.position,
                Speed * Time.fixedDeltaTime
            );
        }
        
        public bool TakeDamage(int damage)
        {
            Destroy(gameObject);
            return false;
        }

        private void OnCollisionEnter(Collision collision) => _collisionHandler.HandleCollision(collision);

        private void OnDestroy() => Dispose();
        public void Dispose() => _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
    }
}