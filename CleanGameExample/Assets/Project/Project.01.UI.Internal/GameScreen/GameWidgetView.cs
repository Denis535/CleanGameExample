#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly VisualElement target;

        // Props
        public override int Layer => -1000;

        // Constructor
        public GameWidgetView() {
            VisualElement = VisualElementFactory_Game.GameWidget( out widget, out target );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetEffect
        public void SetEffect(TargetEffect value) {
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
    public enum TargetEffect {
        Normal,
        Enemy,
        Thing,
    }
}
