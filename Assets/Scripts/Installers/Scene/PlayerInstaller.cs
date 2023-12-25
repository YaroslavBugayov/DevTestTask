using Bullet;
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
                .BindFactory<GameObject, Vector3, Quaternion, BulletEntity, BulletFactory>()
                .AsSingle();
            
            var playerInstance = Container
                .InstantiatePrefabForComponent<PlayerEntity>(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            
            Container
                .Bind<PlayerEntity>()
                .FromInstance(playerInstance)
                .AsSingle();
        }
    }
}