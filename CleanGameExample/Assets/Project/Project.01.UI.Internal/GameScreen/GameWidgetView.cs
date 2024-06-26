﻿#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly VisualElement target;

        // Props
        public TargetEffect TargetEffect {
            set {
                switch (value) {
                    case TargetEffect.Normal:
                        target.style.color = Color.white;
                        break;
                    case TargetEffect.Enemy:
                        target.style.color = Color.red;
                        break;
                    case TargetEffect.Thing:
                        target.style.color = Color.yellow;
                        break;
                    default:
                        Exceptions.Internal.NotSupported( $"Value {value} is supported" );
                        break;
                }
            }
        }
        // Events
        public event EventCallback<NavigationCancelEvent> OnCancel {
            add => widget.RegisterCallback( value );
            remove => widget.UnregisterCallback( value );
        }
        // Input
        private InputActions_UI Input { get; }

        // Constructor
        public GameWidgetView() {
            VisualElement = VisualElementFactory_Game.Game( out widget, out target );
            widget.RegisterCallback<AttachToPanelEvent>( evt => {
                Input!.Enable();
            } );
            widget.UnregisterCallback<DetachFromPanelEvent>( evt => {
                Input!.Disable();
            } );
            Input = new InputActions_UI();
            Input.UI.Cancel.performed += ctx => {
                if (widget.focusController.focusedElement == null) {
                    widget.Focus();
                }
            };
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

    }
    public enum TargetEffect {
        Normal,
        Enemy,
        Thing,
    }
}
