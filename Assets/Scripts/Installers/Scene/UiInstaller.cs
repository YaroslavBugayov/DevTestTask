using UI;
using UnityEngine;
using Zenject;

namespace Installers.Scene
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private UiController uiController;
        [SerializeField] private MenuController menuController;
        
        public override void InstallBindings()
        {
            Container.Bind<UiController>().FromInstance(uiController).AsSingle();
            Container.Bind<MenuController>().FromInstance(menuController).AsSingle();
        }
    }
}