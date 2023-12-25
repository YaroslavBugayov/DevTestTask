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
        public override void InstallBindings()
        {
            Container.BindFactory<EnemyTypes, IEnemyEntity, EnemyFactory>().FromFactory<CustomEnemyFactory>();
            Container.Bind<EnemySpawner>().AsSingle().NonLazy();
        }
    }
}