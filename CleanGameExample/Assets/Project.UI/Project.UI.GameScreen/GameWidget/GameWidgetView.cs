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
            public bool IsActive {
                get => VisualElement.style.color == Color.red;
                set => VisualElement.style.color = value ? Color.red : Color.white;
            }
            public TargetWrapper(VisualElement visualElement) : base( visualElement ) {
            }
        }

        // View
        public ElementWrapper Widget { get; }
        public TargetWrapper Target { get; }

        // Constructor
        public GameWidgetView() {
            VisualElement = UIFactory.Game.GameWidget( out var widget, out var target );
            Widget = widget.Wrap();
            Target = target.Wrap<TargetWrapper>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
