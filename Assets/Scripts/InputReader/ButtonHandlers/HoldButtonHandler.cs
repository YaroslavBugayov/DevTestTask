using UnityEngine;
using UnityEngine.EventSystems;

namespace InputReader.ButtonHandlers
{
    public class HoldButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsActive { get; private set; }
        
        public void OnPointerDown(PointerEventData eventData) => IsActive = true;

        public void OnPointerUp(PointerEventData eventData) => IsActive = false;
    }
}