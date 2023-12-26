using System;
using Enemy.Enums;

namespace Enemy.Services
{
    public class RedEnemyStateService
    {
        public Action<RedEnemyState> StateChanged;

        public RedEnemyState EnemyState { get; private set; }

        public void SetState(RedEnemyState state)
        {
            EnemyState = state;
            StateChanged?.Invoke(EnemyState);
        }
    }
}