using UnityEngine;

namespace Bullet.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyBulletEntity : MonoBehaviour, IBulletEntity
    {
        [SerializeField] private int strengthDamage;
    }
}