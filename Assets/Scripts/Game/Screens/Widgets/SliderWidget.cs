using System;
using System.Reactive.Subjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens.Widgets
{
    public class SliderWidget : Widget
    {
        [SerializeField]
        protected Slider mainSlider;

        [SerializeField]
        protected TMP_Text mainText;

        [SerializeField]
        protected TMP_Text valueText;

        protected readonly ISubject<float> onUpdated = new Subject<float>();

        public float Value => mainSlider.value;
        public IObservable<float> OnUpdated => onUpdated;

        protected virtual void OnEnable()
        {
            mainSlider.onValueChanged.AddListener(HandleClick);
        }

        protected virtual void OnDisable()
        {
            mainSlider.onValueChanged.RemoveListener(HandleClick);
        }

        public void SetMessage(string message)
        {
            if (mainText == null)
            {
                return;
            }

            mainText.text = message;
        }

        public void SetValues(float min, float max, float value, bool wholeNumbers = false, bool notify = true)
        {
            mainSlider.wholeNumbers = wholeNumbers;
            mainSlider.minValue = min;
            mainSlider.maxValue = max;

            if (notify)
            {
                mainSlider.SetValueWithoutNotify(-1);
                mainSlider.value = value;
            }
            else
            {
                mainSlider.SetValueWithoutNotify(value);
            }

            UpdateValueText();
        }

        protected virtual void HandleClick(float value)
        {
            UpdateValueText();
            onUpdated.OnNext(value);
        }

        private void UpdateValueText()
        {
            if (valueText == null)
            {
                return;
            }

            if (mainSlider.wholeNumbers)
            {
                valueText.text = $"({mainSlider.value})";
            }
            else
            {
                valueText.text = $"({mainSlider.value:F})";
            }
        }
    }
}
