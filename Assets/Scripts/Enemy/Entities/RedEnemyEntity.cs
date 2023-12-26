using System;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy.Entities
{
    public class RedEnemyEntity : MonoBehaviour, IEnemyEntity, IDamageable
    {
        public int Health { get; private set; } = 50;
        private const int MaxHeath = 50;
        private PlayerEntity _player;
        
        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            _player = playerEntity;
        }
        
        public void TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, MaxHeath);
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}