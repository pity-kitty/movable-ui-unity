using UnityEngine;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate.Extensions
{
    public class ValueSlider : MonoBehaviour
    {
        private const char PercentSymbol = '%';
        
        [SerializeField] private Slider slider;
        [SerializeField] private Text sliderValue;

        public Slider Slider => slider;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            var percent = Mathf.RoundToInt(value * 100);
            sliderValue.text = $"{percent}{PercentSymbol}";
        }
    }
}