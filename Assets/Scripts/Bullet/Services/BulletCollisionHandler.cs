using Interfaces;
using UnityEngine;

namespace Bullet.Services
{
    public class BulletCollisionHandler : ICollisionHandler
    {
        private readonly GameObject _bullet;
        private readonly int _damage;
        private readonly GameObject _particle;
        
        public BulletCollisionHandler(GameObject bullet, int damage, GameObject particle)
        {
            _bullet = bullet;
            _damage = damage;
            _particle = particle;
        }
        
        public void HandleCollision(Collision collision)
        {
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            target?.TakeDamage(_damage);

            GameObject particleInstance = Object.Instantiate(_particle, _bullet.transform.position, Quaternion.identity);
            Object.Destroy(particleInstance, 0.2f);
            
            Object.Destroy(_bullet);
        }
    }
}