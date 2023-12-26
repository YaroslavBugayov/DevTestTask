using System;
using Bullet;
using Bullet.Entities;
using Bullet.Enums;
using Bullet.Factory;
using UnityEngine;

namespace Player
{
    public class PlayerShooting
    {
        private readonly PlayerStats _playerStats;
        private readonly Camera _camera;
        private readonly BulletFactory _bulletFactory;
        private readonly Transform _playerTransform;
        
        private float _timeSinceLastShoot;
        
        public PlayerShooting(
            PlayerStats playerStats, 
            Camera camera, 
            BulletFactory bulletFactory, 
            Transform playerTransform
            )
        {
            _playerStats = playerStats;
            _camera = camera;
            _bulletFactory = bulletFactory;
            _playerTransform = playerTransform;
        }

        public void Shoot(bool shouldShoot)
        {
            if (shouldShoot)
            {
                _timeSinceLastShoot += Time.deltaTime;
                
                if (_timeSinceLastShoot >= 1f / _playerStats.FireRate)
                {
                    SpawnBullet();
                    _timeSinceLastShoot = 0;
                }
            }
        }

        private void SpawnBullet()
        {
            Vector3 position = _camera.transform.position + _camera.transform.forward * 0.1f;
            IBulletEntity bullet = _bulletFactory.Create(BulletType.PlayerBullet, position, _playerTransform.rotation);
            BulletEntity bulletEntity = (BulletEntity) bullet;
            if (bulletEntity)
            {
                bulletEntity
                    .GetComponent<Rigidbody>()
                    .AddForce(_camera.transform.forward * _playerStats.BulletSpeed, ForceMode.Impulse);
            }
        }
    }
}