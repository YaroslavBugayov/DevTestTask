using System;
using Core.Services;
using UnityEngine;
using Zenject;

namespace InputReader
{
    public class DesktopInputReader : IInputReader
    {
        [Inject]
        public void Construct(IProjectUpdater projectUpdater)
        {
            projectUpdater.UpdateCalled += OnUpdate;
        }
        
        public float HorizontalDirection => Input.GetAxis("Horizontal");
        public float VerticalDirection => Input.GetAxis("Vertical");
        public bool Attack { get; private set; }

        private void OnUpdate()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire");
            }
        }
    }
}
