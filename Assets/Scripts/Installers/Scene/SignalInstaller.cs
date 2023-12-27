using Signals;
using Zenject;

namespace Installers.Scene
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<RestartLevelSignal>();
            Container.DeclareSignal<QuitGameSignal>();
            
            Container.BindSignal<RestartLevelSignal>().ToMethod(signal => signal.RestartLevel());
            Container.BindSignal<QuitGameSignal>().ToMethod(signal => signal.QuitGame());
        }
    }
}