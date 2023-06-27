using System.Collections.Generic;
using EdCon.MiniGameTemplate.Extensions;
using UnityEngine;

namespace EdCon.MiniGameTemplate.UI
{
    public class UISettings : MonoBehaviour
    {
        [SerializeField] private MovableUIElement[] movableElements;
        [SerializeField] private RectTransform highlightRect;

        [Header("Sliders")] 
        [SerializeField] private CanvasGroup scaleSliderCanvasGroup;
        [SerializeField] private ValueSlider scaleSlider;
        [SerializeField] private CanvasGroup opacitySliderCanvasGroup;
        [SerializeField] private ValueSlider opacitySlider;

        private Dictionary<string, MovableUIElement> elements = new Dictionary<string, MovableUIElement>();
        private MovableUIElement currentElement;

        private void Start()
        {
            InitializeUIElements();
            InitializeSubscriptions();
        }

        private void InitializeUIElements()
        {
            foreach (var element in movableElements)
            {
                element.Initialize(OnElementSelect, highlightRect);
                elements.Add(element.ElementName, element);
            }
        }

        private void InitializeSubscriptions()
        {
            scaleSlider.Slider.onValueChanged.AddListener(ScaleSliderValueChange);
            opacitySlider.Slider.onValueChanged.AddListener(OpacitySliderValueChange);
        }

        private void ScaleSliderValueChange(float value)
        {
            currentElement.SetScale(value);
        }

        private void OpacitySliderValueChange(float value)
        {
            currentElement.SetAlpha(value);
        }

        private void OnElementSelect(MovableUIElement element)
        {
            currentElement = element;
            SetSlidersValues(element.Scale, element.Alpha);
            ShowSliders(true);
            highlightRect.gameObject.SetActive(true);
        }

        private void SetSlidersValues(float scale, float opacity)
        {
            scaleSlider.Slider.value = scale;
            opacitySlider.Slider.value = opacity;
        }

        private void ShowSliders(bool state)
        {
            scaleSliderCanvasGroup.ShowCanvasGroup(state);
            opacitySliderCanvasGroup.ShowCanvasGroup(state);
        }

        public void SaveLayout()
        {
            ShowSliders(false);
            highlightRect.gameObject.SetActive(false);
        }
    }
}