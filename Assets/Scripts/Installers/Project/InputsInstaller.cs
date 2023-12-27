using InputReader;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class InputsInstaller : MonoInstaller
    {
        [SerializeField] private MobileInputReader inputsCanvas;
        
        public override void InstallBindings()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                var canvasInstance = Container.InstantiatePrefabForComponent<MobileInputReader>(inputsCanvas);
                                
                Container.Bind<IInputReader>().FromInstance(canvasInstance).AsSingle();
            }
            else
            {
                Container.Bind<IInputReader>().To<DesktopInputReader>().FromNew().AsSingle();
            }
                
        }
    }
}