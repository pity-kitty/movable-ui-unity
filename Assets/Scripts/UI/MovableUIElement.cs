using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.UI
{
    public class MovableUIElement : MonoBehaviour, IDragHandler, ISelectHandler, IDeselectHandler, IPointerDownHandler
    {
        [SerializeField] private string elementName;
        [SerializeField] private CanvasGroup elementCanvasGroup;

        private Action<MovableUIElement> onSelect;

        public string ElementName => elementName;
        public float Scale => transform.localScale.x;
        public float Alpha => elementCanvasGroup.alpha;

        public void Initialize(Action<MovableUIElement> onSelectReference)
        {
            onSelect = onSelectReference;
        }

        public void SetScale(float value)
        {
            transform.localScale = new Vector3(value, value, value);
        }

        public void SetAlpha(float value)
        {
            elementCanvasGroup.alpha = value;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var pointerDelta = new Vector3(eventData.delta.x, eventData.delta.y, 0f);
            transform.position += pointerDelta;
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelect(this);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }
    }
}