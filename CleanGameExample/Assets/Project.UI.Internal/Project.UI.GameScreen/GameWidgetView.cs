#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {

        // Root
        public ElementWrapper Root { get; }
        public ElementWrapper Target { get; }

        // Constructor
        public GameWidgetView() {
            VisualElement = GameViewFactory.GameWidget( out var root, out var target );
            Root = root.Wrap();
            Target = target.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public static class VisualElementWrapperExtensions {

        public static void SetTargetMode(this ElementWrapper element, TargetMode value) {
            switch (value) {
                case TargetMode.Normal:
                    element.__GetVisualElement__().style.color = Color.white;
                    break;
                case TargetMode.Loot:
                    element.__GetVisualElement__().style.color = Color.yellow;
                    break;
                case TargetMode.Enemy:
                    element.__GetVisualElement__().style.color = Color.red;
                    break;
                default:
                    Exceptions.Internal.NotSupported( $"Value {value} is supported" );
                    break;
            }
        }

    }
    public enum TargetMode {
        Normal,
        Loot,
        Enemy,
    }
}
