using System;
using Bullet.Services;
using Interfaces;
using UnityEngine;

namespace Bullet.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletEntity : MonoBehaviour, IBulletEntity
    {
        [SerializeField] private int damage;
        [SerializeField] private GameObject particle;
        private ICollisionHandler _bulletCollisionHandler;

        private void Awake() => _bulletCollisionHandler = new BulletCollisionHandler(gameObject, damage, particle);

        private void OnCollisionEnter(Collision collision) => _bulletCollisionHandler.HandleCollision(collision);
    }
}