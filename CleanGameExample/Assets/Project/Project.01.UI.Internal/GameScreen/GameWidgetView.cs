#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly VisualElement target;

        // Layer
        public override int Layer => -1000;
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
        // Input
        public InputActions_UI Input { get; }
        public bool IsSubmitPressed => Input.UI.Submit.WasPerformedThisFrame();
        public bool IsCancelPressed => Input.UI.Cancel.WasPerformedThisFrame();

        // Constructor
        public GameWidgetView() {
            VisualElement = VisualElementFactory_Game.Game( out widget, out target );
            Input = new InputActions_UI();
            Input.Enable();
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
