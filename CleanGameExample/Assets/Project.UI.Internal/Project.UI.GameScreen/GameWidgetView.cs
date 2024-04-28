#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {
        public class TargetWrapper : ElementWrapper {
            public enum Mode {
                Normal,
                Loot,
                Enemy,
            }
            public TargetWrapper(VisualElement visualElement) : base( visualElement ) {
            }
            public void SetMode(Mode value) {
                switch (value) {
                    case Mode.Normal:
                        VisualElement.style.color = Color.white;
                        break;
                    case Mode.Loot:
                        VisualElement.style.color = Color.yellow;
                        break;
                    case Mode.Enemy:
                        VisualElement.style.color = Color.red;
                        break;
                    default:
                        Exceptions.Internal.NotSupported( $"Value {value} is supported" );
                        break;
                }
            }
        }

        // Root
        public ElementWrapper Root { get; }
        public TargetWrapper Target { get; }

        // Constructor
        public GameWidgetView() {
            VisualElement = GameViewFactory.GameWidget( out var root, out var target );
            Root = root.Wrap();
            Target = target.Wrap<TargetWrapper>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
