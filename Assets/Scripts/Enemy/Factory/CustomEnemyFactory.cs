using System;
using Enemy.Entities;
using Enemy.Enums;
using UnityEngine;
using Zenject;

namespace Enemy.Factory
{
    public class CustomEnemyFactory : IFactory<EnemyType, Vector3, Transform, IEnemyEntity>
    {
        private readonly DiContainer _diContainer;
        private GameObject _blueEnemy;
        private GameObject _redEnemy;
        private const string BlueEnemyPath = "Enemies/Blue";
        private const string RedEnemyPath = "Enemies/Red";

        public CustomEnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            Load();
        }

        private void Load()
        {
            _blueEnemy = Resources.Load<GameObject>(BlueEnemyPath);
            _redEnemy = Resources.Load<GameObject>(RedEnemyPath);
        }
            
        public IEnemyEntity Create(EnemyType enemyType, Vector3 position, Transform parent)
        {
            return enemyType switch
            {
                EnemyType.EnemyBlue => 
                    _diContainer.InstantiatePrefabForComponent<IEnemyEntity>(
                        _blueEnemy, position, Quaternion.identity, parent
                    ),
                EnemyType.EnemyRed => 
                    _diContainer.InstantiatePrefabForComponent<IEnemyEntity>(
                        _redEnemy, position, Quaternion.identity, parent
                    ),
                _ => throw new Exception("Enemy type not found")
            };
        }
    }
}