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
            VisualElement = ViewFactory.GameWidget( out var root, out var target );
            Root = root.Wrap();
            Target = target.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
