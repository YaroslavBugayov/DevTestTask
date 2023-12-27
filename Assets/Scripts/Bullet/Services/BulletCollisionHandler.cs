using Bullet.Entities;
using Enemy.Entities;
using Interfaces;
using Player;
using UnityEngine;

namespace Bullet.Services
{
    public class BulletCollisionHandler : ICollisionHandler
    {
        private readonly GameObject _bullet;
        private readonly int _damage;
        private readonly GameObject _particle;
        private readonly PlayerStats _playerStats;
        
        public BulletCollisionHandler(GameObject bullet, int damage, GameObject particle, PlayerStats playerStats)
        {
            _bullet = bullet;
            _damage = damage;
            _particle = particle;
            _playerStats = playerStats;
        }
        
        public void HandleCollision(Collision collision)
        {
            if (collision.gameObject is EnemyBulletEntity)
            {
                
            }
            
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            bool isDied = target?.TakeDamage(_damage) ?? false;

            RedEnemyEntity[] redEnemies = Object.FindObjectsOfType<RedEnemyEntity>();
            BlueEnemyEntity[] blueEnemies = Object.FindObjectsOfType<BlueEnemyEntity>();

            bool isRicochet = _playerStats.Health <= 20 || Random.Range(0f, 1f) < 0.1;
            
            if (isDied && (redEnemies.Length + blueEnemies.Length > 0) && isRicochet)
            {
                Transform newTarget = FindNearestEnemy(redEnemies, blueEnemies);
                Vector3 direction = (newTarget.position - _bullet.transform.position).normalized;
                _bullet.GetComponent<Rigidbody>().velocity = direction * _playerStats.BulletSpeed;
            }
            else
            {
                GameObject particleInstance = Object.Instantiate(_particle, _bullet.transform.position, Quaternion.identity);
                Object.Destroy(particleInstance, 0.2f);
            
                Object.Destroy(_bullet);
            }
        }

        private Transform FindNearestEnemy(RedEnemyEntity[] redEnemies, BlueEnemyEntity[] blueEnemies)
        {
            float distance = Mathf.Infinity;
            Transform nearestEnemy = _bullet.transform;
            
            foreach (var enemy in redEnemies)
            {
                float tempDistance = Vector3.Distance(enemy.gameObject.transform.position, _bullet.transform.position);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    nearestEnemy = enemy.transform;
                }
            }
            
            foreach (var enemy in blueEnemies)
            {
                float tempDistance = Vector3.Distance(enemy.gameObject.transform.position, _bullet.transform.position);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    nearestEnemy = enemy.transform;
                }
            }

            return nearestEnemy;
        }
    }
}