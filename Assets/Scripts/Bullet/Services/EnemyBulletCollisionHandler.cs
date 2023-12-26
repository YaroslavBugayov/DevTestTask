using Interfaces;
using Player;
using UnityEngine;

namespace Bullet.Services
{
    public class EnemyBulletCollisionHandler : ICollisionHandler
    {
        private readonly GameObject _bullet;
        private readonly int _strengthDamage;
        
        public EnemyBulletCollisionHandler(GameObject bullet, int strengthDamage)
        {
            _bullet = bullet;
            _strengthDamage = strengthDamage;
        }
        
        public void HandleCollision(Collision collision)
        {
            ILosingStrength player = collision.transform.GetComponent<PlayerEntity>();
            if (player != null)
            {
                player.TakeDamageToStrength(_strengthDamage);
                Object.Destroy(_bullet);
            }
        }
    }
}