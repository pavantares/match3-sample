using System;
using System.Reactive.Subjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens.Widgets
{
    public class ToggleWidget : Widget
    {
        [SerializeField]
        protected Toggle mainToggle;

        [SerializeField]
        protected TMP_Text mainText;

        protected readonly ISubject<bool> onClicked = new Subject<bool>();

        public ToggleGroup ToggleGroup => mainToggle.group;
        public IObservable<bool> OnClicked => onClicked;

        protected virtual void OnEnable()
        {
            mainToggle.onValueChanged.AddListener(HandleClick);
        }

        protected virtual void OnDisable()
        {
            mainToggle.onValueChanged.RemoveListener(HandleClick);
        }

        public void SetMessage(string message)
        {
            if (mainText == null)
            {
                return;
            }

            mainText.text = message;
        }

        public virtual void SetValue(bool value, bool notify = true)
        {
            if (notify)
            {
                mainToggle.isOn = value;
            }
            else
            {
                mainToggle.SetIsOnWithoutNotify(value);
            }
        }

        public void SetGroup(ToggleGroup group)
        {
            mainToggle.group = group;
        }

        protected virtual void HandleClick(bool value)
        {
            onClicked.OnNext(value);
        }
    }
}
