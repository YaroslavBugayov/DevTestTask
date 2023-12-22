using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            var playerInstance = Container
                .InstantiatePrefabForComponent<PlayerEntity>(playerPrefab, spawnPoint.position, Quaternion.identity, null);

            Container
                .Bind<PlayerEntity>()
                .FromInstance(playerInstance)
                .AsSingle();
        }
    }
}