using System;
using Bullet.Entities;
using Bullet.Enums;
using UnityEngine;
using Zenject;

namespace Bullet.Factory
{
    public class CustomBulletFactory : IFactory<BulletType, Vector3, Quaternion, IBulletEntity>
    {
        private DiContainer _diContainer;
        private GameObject _playerBullet;
        private GameObject _enemyBullet;
        private const string PlayerBulletPath = "Bullets/Bullet";
        private const string EnemyBulletPath = "Bullets/EnemyBullet";
        
        public CustomBulletFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            Load();
        }

        private void Load()
        {
            _playerBullet = Resources.Load<GameObject>(PlayerBulletPath);
            _enemyBullet = Resources.Load<GameObject>(EnemyBulletPath);
        }

        public IBulletEntity Create(BulletType bulletType, Vector3 position, Quaternion rotation)
        {
            return bulletType switch
            {
                BulletType.PlayerBullet => 
                    _diContainer.InstantiatePrefabForComponent<IBulletEntity>(_playerBullet, position, rotation, null),
                BulletType.EnemyBullet => 
                    _diContainer.InstantiatePrefabForComponent<IBulletEntity>(_enemyBullet, position, rotation, null),
                _ => throw new Exception("Enemy type not found")
            };
        }
    }
}