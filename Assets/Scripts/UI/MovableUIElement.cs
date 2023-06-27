using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.UI
{
    public class MovableUIElement : MonoBehaviour, IDragHandler, ISelectHandler, IPointerDownHandler
    {
        [SerializeField] private string elementName;
        [SerializeField] private CanvasGroup elementCanvasGroup;
        [SerializeField] private RectTransform elementRect;

        private Action<MovableUIElement> onSelect;
        private RectTransform highlightRect;

        public string ElementName => elementName;
        public float Scale => transform.localScale.x;
        public float Alpha => elementCanvasGroup.alpha;

        public void Initialize(Action<MovableUIElement> onSelectReference, RectTransform highlightRectReference)
        {
            onSelect = onSelectReference;
            highlightRect = highlightRectReference;
        }

        public void SetScale(float value)
        {
            transform.localScale = new Vector3(value, value, value);
            highlightRect.localScale = elementRect.localScale;
        }

        public void SetAlpha(float value)
        {
            elementCanvasGroup.alpha = value;
        }

        public void SetProperties(UIElementProperties properties)
        {
            var elementTransform = transform;
            var scaleValue = properties.Scale;
            elementTransform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            elementCanvasGroup.alpha = properties.Alpha;
            var position = new Vector3(properties.PositionX, properties.PositionY, 0f);
            elementTransform.position = position;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var pointerDelta = new Vector3(eventData.delta.x, eventData.delta.y, 0f);
            transform.position += pointerDelta;
            highlightRect.position = elementRect.position;
        }

        public void OnSelect(BaseEventData eventData)
        {
            highlightRect.sizeDelta = elementRect.sizeDelta;
            highlightRect.pivot = elementRect.pivot;
            highlightRect.position = elementRect.position;
            onSelect(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }
    }
}