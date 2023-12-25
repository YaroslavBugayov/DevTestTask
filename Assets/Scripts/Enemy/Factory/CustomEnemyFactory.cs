using System;
using Enemy.Entities;
using Enemy.Enums;
using UnityEngine;
using Zenject;

namespace Enemy.Factory
{
    public class CustomEnemyFactory : IFactory<EnemyTypes, Vector3, Transform, IEnemyEntity>
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
            _blueEnemy = (GameObject) Resources.Load(BlueEnemyPath);
            _redEnemy = (GameObject) Resources.Load(RedEnemyPath);
        }
            
        public IEnemyEntity Create(EnemyTypes enemyType, Vector3 position, Transform parent)
        {
            return enemyType switch
            {
                EnemyTypes.EnemyBlue => 
                    _diContainer.InstantiatePrefabForComponent<IEnemyEntity>(
                        _blueEnemy, position, Quaternion.identity, parent
                    ),
                EnemyTypes.EnemyRed => 
                    _diContainer.InstantiatePrefabForComponent<IEnemyEntity>(
                        _redEnemy, position, Quaternion.identity, parent
                    ),
                _ => throw new Exception("Enemy type not found")
            };
        }
    }
}