using Enemy.Entities;
using Enemy.Enums;
using Enemy.Factory;
using UnityEngine;
using Zenject;

namespace Installers.Scene
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawner enemySpawner;
        
        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyType, Vector3, Transform, IEnemyEntity, EnemyFactory>()
                .FromFactory<CustomEnemyFactory>();
            
            Container
                .Bind<EnemySpawner>()
                .FromInstance(enemySpawner)
                .AsSingle()
                .NonLazy();
        }
    }
}