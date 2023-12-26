using Enemy.Entities;
using Enemy.Enums;
using Enemy.Factory;
using Enemy.Services;
using UnityEngine;
using Zenject;

namespace Installers.Scene
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform spawnParent;
        
        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyType, Vector3, Transform, IEnemyEntity, EnemyFactory>()
                .FromFactory<CustomEnemyFactory>();
            
            Container
                .Bind<EnemySpawner>()
                .AsSingle()
                .WithArguments(spawnPoints, spawnParent)
                .NonLazy();
        }
    }
}