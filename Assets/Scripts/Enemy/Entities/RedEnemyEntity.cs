using System;
using System.Collections;
using Bullet.Enums;
using Core.Services;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy.Entities
{
    public class RedEnemyEntity : MonoBehaviour, IEnemyEntity, IDamageable, IDisposable
    {
        public int Health { get; private set; } = 50;
        private const int MaxHeath = 50;
        private const float SpeedUp = 1f;
        private const float SpeedForward = 2.5f;
        private Transform _player;
        private RedEnemyState _state;
        private IProjectUpdater _projectUpdater;
        
        [Inject]
        public void Construct(PlayerEntity playerEntity, IProjectUpdater projectUpdater)
        {
            _player = playerEntity.transform;
            _projectUpdater = projectUpdater;
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;
        }

        private void Start()
        {
            StartCoroutine(nameof(State));
        }

        private void OnFixedUpdate()
        {
            transform.position = GetMovement(_state);
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable player = collision.transform.GetComponent<PlayerEntity>();
            if (player != null)
            {
                player.TakeDamage(15);
                Destroy(gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, MaxHeath);
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator State()
        {
            _state = RedEnemyState.FlyingUp;
            yield return new WaitForSeconds(2f);
            _state = RedEnemyState.Pursuit;
        }

        private Vector3 GetMovement(RedEnemyState state)
        {
            return state switch
            {
                RedEnemyState.FlyingUp => 
                    Vector3.MoveTowards(
                        transform.position,
                        transform.position + Vector3.up,
                        SpeedUp * Time.fixedDeltaTime
                    ),
                RedEnemyState.Pursuit => 
                    Vector3.MoveTowards(
                        transform.position,
                        _player.position,
                        SpeedForward * Time.fixedDeltaTime
                    ),
                RedEnemyState.FlyingForward => 
                    Vector3.MoveTowards(
                        transform.position,
                        transform.position + transform.right,
                        SpeedForward * Time.fixedDeltaTime
                    ),
                _ => throw new Exception("Unknown state")
            };
        }

        private void OnDestroy()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
        }
    }
}