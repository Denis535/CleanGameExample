#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Widget { get; }

        // Constructor
        public GameWidgetView(UIFactory factory) {
            VisualElement = factory.GameWidget( out var view );
            Widget = view.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
