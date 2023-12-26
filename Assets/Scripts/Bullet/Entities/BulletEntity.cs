using System;
using Interfaces;
using UnityEngine;

namespace Bullet.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletEntity : MonoBehaviour, IBulletEntity
    {
        [SerializeField] private int damage;
        [SerializeField] private GameObject particle;
        
        private void OnCollisionEnter(Collision collision)
        {
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            target?.TakeDamage(damage);

            GameObject particleInstance =Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particleInstance, 0.2f);
            
            Destroy(gameObject);
        }
    }
}