using System;
using Core.Services;
using Enemy.Enums;
using UnityEngine;

namespace Enemy.Services
{
    public class RedEnemyMovement : IDisposable
    {
        private const float SpeedUp = 1f;
        private const float SpeedForward = 2.5f;
        private readonly GameObject _enemy;
        private readonly RedEnemyStateService _stateService;
        private readonly IProjectUpdater _projectUpdater;
        private readonly Transform _player;
        private RedEnemyState _state;
        
        public RedEnemyMovement(GameObject enemy, RedEnemyStateService stateService, IProjectUpdater projectUpdater, Transform player)
        {
            _enemy = enemy;
            _player = player;
            
            _stateService = stateService;
            _stateService.StateChanged += OnStateChange;
            
            _projectUpdater = projectUpdater;
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;
        }

        private void OnFixedUpdate() => _enemy.transform.position = GetMovement(_state);

        private void OnStateChange(RedEnemyState state) => _state = state;

        private Vector3 GetMovement(RedEnemyState state)
        {
            Vector3 position = _enemy.transform.position;
            return state switch
            {
                RedEnemyState.FlyingUp => 
                    Vector3.MoveTowards(
                        position,
                        position + Vector3.up,
                        SpeedUp * Time.fixedDeltaTime
                    ),
                RedEnemyState.Pursuit => 
                    Vector3.MoveTowards(
                        position,
                        _player.position,
                        SpeedForward * Time.fixedDeltaTime
                    ),
                RedEnemyState.FlyingForward => 
                    Vector3.MoveTowards(
                        position,
                        position + _enemy.transform.right,
                        SpeedForward * Time.fixedDeltaTime
                    ),
                _ => throw new Exception("Unknown state")
            };
        }

        public void Dispose()
        {
            _stateService.StateChanged -= OnStateChange;
            _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
        }
    }
}