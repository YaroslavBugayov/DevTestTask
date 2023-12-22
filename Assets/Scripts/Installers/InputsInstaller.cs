using System;
using InputReader;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InputsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
                Container.Bind<IInputReader>().To<MobileInputReader>().FromNew().AsSingle();
            else
                Container.Bind<IInputReader>().To<DesktopInputReader>().FromNew().AsSingle();
        }
    }
}