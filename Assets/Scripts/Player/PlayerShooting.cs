using System;
using Bullet;
using UnityEngine;

namespace Player
{
    public class PlayerShooting
    {
        private readonly PlayerStats _playerStats;
        private readonly Camera _camera;
        private readonly BulletFactory _bulletFactory;
        private readonly GameObject _bulletPrefab;
        private readonly Transform _playerTransform;
        
        private float _timeSinceLastShoot;
        
        public PlayerShooting(
            PlayerStats playerStats, 
            Camera camera, 
            BulletFactory bulletFactory, 
            BulletEntity bullet, 
            Transform playerTransform
            )
        {
            _playerStats = playerStats;
            _camera = camera;
            _bulletFactory = bulletFactory;
            _bulletPrefab = bullet.gameObject;
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
            BulletEntity bullet = _bulletFactory.Create(_bulletPrefab, position, _playerTransform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_camera.transform.forward * _playerStats.BulletSpeed, ForceMode.Impulse);
        }
    }
}