using System;
using Enemy.Entities;
using Enemy.Enums;
using Enemy.Services;
using Interfaces;
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
            switch (enemyType)
            {
                case EnemyType.EnemyBlue:
                    var enemy = _diContainer.InstantiatePrefabForComponent<IEnemyEntity>(_blueEnemy, position,
                        Quaternion.identity, parent);
                    _diContainer.Bind<ICollisionHandler>().To<RedEnemyCollisionHandler>().AsSingle();
                    return enemy;
                case EnemyType.EnemyRed:
                    return _diContainer.InstantiatePrefabForComponent<IEnemyEntity>(_redEnemy, position,
                        Quaternion.identity, parent);
                default:
                    throw new Exception("Enemy type not found");
            }
        }
    }
}