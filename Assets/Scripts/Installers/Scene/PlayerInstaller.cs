using Bullet.Entities;
using Bullet.Enums;
using Bullet.Factory;
using Player;
using UnityEngine;
using Zenject;

namespace Installers.Scene
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerStats>()
                .FromNew()
                .AsCached();
            
            Container
                .BindFactory<BulletType, Vector3, Quaternion, BulletEntity, BulletFactory>()
                .FromFactory<CustomBulletFactory>();
            
            var playerInstance = Container
                .InstantiatePrefabForComponent<PlayerEntity>(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            
            Container
                .Bind<PlayerEntity>()
                .FromInstance(playerInstance)
                .AsSingle();
        }
    }
}