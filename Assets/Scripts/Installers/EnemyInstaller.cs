using Enemy;
using Enemy.Entities;
using Enemy.Enums;
using Enemy.Factory;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform spawnParent;
        
        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyTypes, Vector3, Transform, IEnemyEntity, EnemyFactory>()
                .FromFactory<CustomEnemyFactory>();
            
            Container
                .Bind<EnemySpawner>()
                .AsSingle()
                .WithArguments(spawnPoints, spawnParent)
                .NonLazy();
        }
    }
}