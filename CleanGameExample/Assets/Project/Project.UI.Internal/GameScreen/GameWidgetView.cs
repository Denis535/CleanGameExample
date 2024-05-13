#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {

        private readonly VisualElement target;

        // Constructor
        public GameWidgetView() {
            VisualElement = ViewFactory.GameWidget( out _, out target );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
