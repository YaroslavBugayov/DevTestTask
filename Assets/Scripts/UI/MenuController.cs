using Core.Services;
using Signals;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        private SignalBus _signalBus;
        private IProjectUpdater _projectUpdater;
        
        [Inject]
        public void Construct(SignalBus signalBus, IProjectUpdater projectUpdater)
        {
            _signalBus = signalBus;
            _projectUpdater = projectUpdater;
        }
        
        public void RestartLevel()
        {
            _signalBus.Fire<RestartLevelSignal>();
            _projectUpdater.IsPaused = false;
        }

        public void QuitGame()
        {
            _signalBus.Fire<QuitGameSignal>();
        }
    }
}