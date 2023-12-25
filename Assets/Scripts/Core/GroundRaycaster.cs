using System;
using Core.Services;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GroundRaycaster : IDisposable
    {
        public bool IsGrounded { get; private set; }
        
        private readonly Transform _target;
        private readonly float _raycastDistance;
        private readonly IProjectUpdater _projectUpdater;
        private readonly LayerMask _layerMask;

        public GroundRaycaster(Transform target, IProjectUpdater projectUpdater, float raycastDistance = 0.2f)
        {
            _target = target;
            _raycastDistance = raycastDistance;
            _projectUpdater = projectUpdater;
            _layerMask = 1 << LayerMask.NameToLayer("Ground");
            _projectUpdater.UpdateCalled += OnUpdate;
        }

        private void OnUpdate()
        {
            Debug.DrawRay(_target.position, Vector3.down * _raycastDistance, Color.green);
            IsGrounded = Physics.Raycast(_target.position, Vector3.down, _raycastDistance, _layerMask);
        }
        
        public void Dispose()
        {
            _projectUpdater.UpdateCalled -= OnUpdate;
        }
    }
}