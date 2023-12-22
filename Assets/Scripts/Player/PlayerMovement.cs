using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement
    {
        private readonly Rigidbody _rigidbody;
        private readonly PlayerStats _stats;
        private readonly Camera _camera;
        private float _currentVerticalAngle = 0;
        
        public PlayerMovement(Rigidbody rigidbody, PlayerStats playerStats, Camera camera)
        {
            _rigidbody = rigidbody;
            _stats = playerStats;
            _camera = camera;
        }

        public void Move(float horizontalDirection, float verticalDirection)
        {
            var x = _rigidbody.transform.forward * verticalDirection * _stats.Speed;
            var z = _rigidbody.transform.right * horizontalDirection * _stats.Speed;
            var movement = new Vector3(0, _rigidbody.velocity.y, 0);
            movement += x;
            movement += z;
            _rigidbody.velocity = movement;
        }

        public void Rotate(float horizontalRotation, float verticalRotation)
        {
            _rigidbody.transform.Rotate(Vector3.up * horizontalRotation * _stats.TurnSpeed);
            
            _currentVerticalAngle -= verticalRotation * _stats.TurnSpeed;
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -90f, 90f);
            _camera.transform.localRotation = Quaternion.Euler(_currentVerticalAngle, 0f, 0f);
        }

        public void Jump(bool shouldJump)
        {
            if (shouldJump)
            {
                var movement = _rigidbody.velocity;
                movement.y = _stats.JumpForce;
                _rigidbody.velocity = movement;
            }
        }
    }
}