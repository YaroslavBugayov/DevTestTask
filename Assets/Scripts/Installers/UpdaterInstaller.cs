using Core.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UpdaterInstaller : MonoInstaller
    {
        [SerializeField] private ProjectUpdater projectUpdater;
        public override void InstallBindings()
        {
            Container.Bind<IProjectUpdater>().FromInstance(projectUpdater).AsSingle().NonLazy();
        }
    }
}