using System;
using Bullet.Services;
using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Bullet.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletEntity : MonoBehaviour, IBulletEntity
    {
        [SerializeField] private int damage;
        [SerializeField] private GameObject particle;
        private ICollisionHandler _bulletCollisionHandler;
        private PlayerStats _playerStats;

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        
        private void Awake() => _bulletCollisionHandler = new BulletCollisionHandler(gameObject, damage, particle, _playerStats);

        private void OnCollisionEnter(Collision collision) => _bulletCollisionHandler.HandleCollision(collision);
    }
}