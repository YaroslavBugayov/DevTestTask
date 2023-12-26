using System;
using Bullet.Entities;
using Bullet.Factory;
using Core.Services;
using Enemy.Services;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class BlueEnemyEntity : MonoBehaviour, IEnemyEntity, IDamageable, IDisposable
    {
        public int Health { get; private set; } = 100;
        private const int MaxHeath = 100;
        private IProjectUpdater _projectUpdater;
        private BlueEnemyShooting _shooting;
        
        [Inject]
        public void Construct(PlayerEntity playerEntity, IProjectUpdater projectUpdater, BulletFactory bulletFactory)
        {
            _shooting = new BlueEnemyShooting(bulletFactory, transform, playerEntity.transform);
            _projectUpdater = projectUpdater;
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;
        }
        
        public void TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, MaxHeath);
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnFixedUpdate() => _shooting.Shoot();

        private void OnDestroy() => Dispose();

        public void Dispose() => _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
    }
}