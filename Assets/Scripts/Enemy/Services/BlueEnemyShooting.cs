using Bullet.Entities;
using Bullet.Enums;
using Bullet.Factory;
using UnityEngine;

namespace Enemy.Services
{
    public class BlueEnemyShooting
    {
        private readonly BulletFactory _bulletFactory;
        private readonly Transform _enemyTransform;
        private readonly Transform _playerTransform;
        private const float FirePause = 3f;
        private float _timeSinceLastShoot;

        public BlueEnemyShooting(BulletFactory bulletFactory, Transform enemyTransform, Transform playerTransform)
        {
            _bulletFactory = bulletFactory;
            _enemyTransform = enemyTransform;
            _playerTransform = playerTransform;
        }
        
        public void Shoot()
        {
            _timeSinceLastShoot += Time.deltaTime;
            
            if (_timeSinceLastShoot >= FirePause)
            {
                SpawnBullet();
                _timeSinceLastShoot = 0;
            }
        }

        private void SpawnBullet()
        {
            Vector3 direction = (_playerTransform.position - _enemyTransform.position).normalized;
            Vector3 position = _enemyTransform.position + direction * 0.3f;
            IBulletEntity bullet = _bulletFactory.Create(BulletType.EnemyBullet, position, _enemyTransform.rotation);
        }
    }
}