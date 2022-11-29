using System;
using System.Reactive;
using System.Reactive.Subjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens.Widgets
{
    public class ButtonWidget : Widget
    {
        [SerializeField]
        protected Button mainButton;

        [SerializeField]
        protected TMP_Text mainText;

        protected readonly ISubject<Unit> onClicked = new Subject<Unit>();

        public IObservable<Unit> OnClicked => onClicked;

        protected virtual void OnEnable()
        {
            mainButton.onClick.AddListener(HandleClick);
        }

        protected virtual void OnDisable()
        {
            mainButton.onClick.RemoveListener(HandleClick);
        }

        public void SetMessage(string message)
        {
            if (mainText == null)
            {
                return;
            }

            mainText.text = message;
        }

        protected virtual void HandleClick()
        {
            onClicked.OnNext(Unit.Default);
        }

        public void SetInteractableButton(bool isInteractable)
        {
            mainButton.interactable = isInteractable;
        }
    }
}
