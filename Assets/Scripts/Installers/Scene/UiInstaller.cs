using UI;
using UnityEngine;
using Zenject;

namespace Installers.Scene
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private UiController uiController;
        
        public override void InstallBindings()
        {
            Container.Bind<UiController>().FromInstance(uiController).AsSingle().NonLazy();
        }
    }
}