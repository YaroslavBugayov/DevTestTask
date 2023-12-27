using UnityEngine;
using UnityEngine.EventSystems;

namespace InputReader.ButtonHandlers
{
    public class TapButtonHandler : MonoBehaviour, IPointerDownHandler
    {
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                bool currentState = _isActive;
                _isActive = false;
                return currentState;
            }
        }

        public void OnPointerDown(PointerEventData eventData) => _isActive = true;

    }
}