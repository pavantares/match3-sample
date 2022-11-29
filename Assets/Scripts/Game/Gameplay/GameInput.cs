using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Gameplay
{
    public class GameInput : MonoBehaviour
    {
        private readonly ISubject<Vector2> onDownClicked = new Subject<Vector2>();
        private readonly ISubject<Vector2> onUpClicked = new Subject<Vector2>();
        private readonly ISubject<Vector2> onDragging = new Subject<Vector2>();

        private InputAction leftMouseClick;
        private bool isPressed;
        private bool isEnabled;

        public IObservable<Vector2> OnDownClicked => onDownClicked;
        public IObservable<Vector2> OnUpClicked => onUpClicked;
        public IObservable<Vector2> OnDragging => onDragging;

        private void Awake()
        {
            leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        }

        private void Update()
        {
            if (Application.isEditor && isEnabled && isPressed)
            {
                onDragging.OnNext(GetMousePoint());
            }
        }

        private void OnDestroy()
        {
            SetEnabled(false);
        }

        public void SetEnabled(bool isEnabled)
        {
            if (this.isEnabled == isEnabled)
            {
                return;
            }

            if (isEnabled)
            {
                if (Application.isEditor)
                {
                    leftMouseClick.Enable();
                    leftMouseClick.performed += HandleMouseDown;
                    leftMouseClick.canceled += HandleMouseUp;
                }
                else
                {
                    TouchSimulation.Enable();
                    EnhancedTouchSupport.Enable();
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += HandleTouchDown;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += HandleTouchUp;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += HandleTouchMove;
                }
            }
            else
            {
                if (Application.isEditor)
                {
                    leftMouseClick.performed -= HandleMouseDown;
                    leftMouseClick.canceled -= HandleMouseUp;
                    leftMouseClick.Disable();
                }
                else
                {
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= HandleTouchDown;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= HandleTouchUp;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= HandleTouchMove;
                    TouchSimulation.Disable();
                    EnhancedTouchSupport.Disable();
                }

                isPressed = false;
            }

            this.isEnabled = isEnabled;
        }

        private void HandleMouseDown(InputAction.CallbackContext context)
        {
            ClickDown(GetMousePoint());
        }

        private void HandleMouseUp(InputAction.CallbackContext context)
        {
            ClickUp(GetMousePoint());
        }

        private void HandleTouchDown(Finger finger)
        {
            ClickDown(finger.screenPosition);
        }

        private void HandleTouchUp(Finger finger)
        {
            ClickUp(finger.screenPosition);
        }

        private void HandleTouchMove(Finger finger)
        {
            if (!isPressed)
            {
                return;
            }

            onDragging.OnNext(finger.screenPosition);
        }

        private void ClickDown(Vector2 screenPoint)
        {
            if (IsClickOnUI(screenPoint))
            {
                return;
            }

            onDownClicked.OnNext(screenPoint);
            isPressed = true;
        }

        private void ClickUp(Vector2 screenPoint)
        {
            isPressed = false;

            if (IsClickOnUI(screenPoint))
            {
                return;
            }

            onUpClicked.OnNext(screenPoint);
        }

        private Vector2 GetMousePoint()
        {
            return Mouse.current.position.ReadValue();
        }

        private static bool IsClickOnUI(Vector2 screenPoint)
        {
            var pointerEventData = new PointerEventData(EventSystem.current) { position = screenPoint };
            var raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

            return raycastResultsList.Any(result => result.gameObject is GameObject);
        }
    }
}
