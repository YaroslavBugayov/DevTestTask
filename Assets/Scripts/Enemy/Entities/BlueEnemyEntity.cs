using System;
using Bullet.Entities;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class BlueEnemyEntity : MonoBehaviour, IEnemyEntity, IDamageable
    {
        public int Health { get; private set; } = 100;
        private const int MaxHeath = 100;
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