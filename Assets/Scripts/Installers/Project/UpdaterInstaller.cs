using Core.Services;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class UpdaterInstaller : MonoInstaller
    {
        [SerializeField] private ProjectUpdater projectUpdater;
        public override void InstallBindings()
        {
            // Container.Bind<IProjectUpdater>().FromInstance(projectUpdater).AsSingle().NonLazy();
            var projectUpdaterInstance = Container
                .InstantiatePrefabForComponent<ProjectUpdater>(projectUpdater);
            
            Container
                .Bind<IProjectUpdater>()
                .FromInstance(projectUpdaterInstance).AsSingle().NonLazy();
        }
    }
}