using Interfaces;
using Player;
using UnityEngine;

namespace Enemy.Services
{
    public class RedEnemyCollisionHandler : ICollisionHandler
    {
        private readonly GameObject _enemy;
        private readonly int _damage;
        
        public RedEnemyCollisionHandler(GameObject enemy, int damage)
        {
            _enemy = enemy;
            _damage = damage;
        }
        
        public void HandleCollision(Collision collision)
        {
            IDamageable player = collision.transform.GetComponent<PlayerEntity>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                Object.Destroy(_enemy);
            }
        }
    }
}