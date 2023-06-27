using System.Collections.Generic;
using System.IO;
using EdCon.MiniGameTemplate.Extensions;
using Newtonsoft.Json;
using UnityEngine;

namespace EdCon.MiniGameTemplate.UI
{
    public class UISettings : MonoBehaviour
    {
        private const string DefaultLayoutFileName = "default_layout.json";
        private const string CustomLayoutFileName = "custom_layout.json";
        
        [SerializeField] private MovableUIElement[] movableElements;
        [SerializeField] private RectTransform highlightRect;

        [Header("Sliders")] 
        [SerializeField] private CanvasGroup scaleSliderCanvasGroup;
        [SerializeField] private ValueSlider scaleSlider;
        [SerializeField] private CanvasGroup opacitySliderCanvasGroup;
        [SerializeField] private ValueSlider opacitySlider;

        private Dictionary<string, MovableUIElement> elements = new Dictionary<string, MovableUIElement>();
        private MovableUIElement currentElement;
        private string defaultFilePath;
        private string customFilePath;

        private void Start()
        {
            defaultFilePath = Path.Combine(Application.persistentDataPath, DefaultLayoutFileName);
            customFilePath = Path.Combine(Application.persistentDataPath, CustomLayoutFileName);
            InitializeUIElements();
            InitializeSubscriptions();
            if (!IOService.CheckFileExists(defaultFilePath)) SaveToFile(defaultFilePath);
            else if (IOService.CheckFileExists(customFilePath)) LoadFromFile(customFilePath);
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

        private void SaveToFile(string path)
        {
            var elementsCount = movableElements.Length;
            var elementsProperties = new UIElementProperties[elementsCount];
            for (int i = 0; i < elementsCount; i++)
            {
                var element = movableElements[i];
                var position = element.transform.position;
                var property = new UIElementProperties
                {
                    Name = element.ElementName,
                    PositionX = position.x,
                    PositionY = position.y,
                    Scale = element.Scale,
                    Alpha = element.Alpha
                };
                elementsProperties[i] = property;
            }

            var jsonData = JsonConvert.SerializeObject(elementsProperties);
            IOService.SaveData(path, jsonData);
        }

        private void LoadFromFile(string filePath)
        {
            var jsonData = IOService.ReadData(filePath);
            var elementsProperties = JsonConvert.DeserializeObject<UIElementProperties[]>(jsonData);
            foreach (var property in elementsProperties)
            {
                if (elements.TryGetValue(property.Name, out var element))
                {
                    element.SetProperties(property);
                }
            }
        }

        public void SaveLayout()
        {
            SaveToFile(customFilePath);
            ShowSliders(false);
            highlightRect.gameObject.SetActive(false);
        }

        public void LoadDefaultLayout()
        {
            LoadFromFile(defaultFilePath);
        }
    }
}