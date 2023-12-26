using Interfaces;
using Player;
using UnityEngine;

namespace Enemy.Services
{
    public class RedEnemyCollisionHandler : ICollisionHandler
    {
        private readonly GameObject _enemy;
        public RedEnemyCollisionHandler(GameObject enemy)
        {
            _enemy = enemy;
        }
        
        public void HandleCollision(Collision collision)
        {
            IDamageable player = collision.transform.GetComponent<PlayerEntity>();
            if (player != null)
            {
                player.TakeDamage(15);
                Object.Destroy(_enemy);
            }
        }
    }
}