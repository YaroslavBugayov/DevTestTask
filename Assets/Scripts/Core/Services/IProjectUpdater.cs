using System;

namespace Core.Services
{
    public interface IProjectUpdater
    {
        event Action<bool> PauseStateChanged;
        event Action UpdateCalled;
        event Action FixedUpdateCalled;
        event Action LateUpdateCalled;
        event Action UpdateForPauseCalled;
        
        bool IsPaused { get; set; }
    }
}