using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement
    {
        private readonly Rigidbody _rigidbody;
        private readonly PlayerStats _stats;
        
        public PlayerMovement(Rigidbody rigidbody, PlayerStats playerStats)
        {
            _rigidbody = rigidbody;
            _stats = playerStats;
        }

        public void Move(float horizontalDirection, float verticalDirection)
        {
            var movement = new Vector3(
                horizontalDirection * _stats.Speed, 
                _rigidbody.velocity.y, 
                verticalDirection * _stats.Speed);
            _rigidbody.velocity = movement;
        }
        
    }
}