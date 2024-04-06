#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class GameWidgetView : UIViewBase {

        // View
        public ElementWrapper Widget { get; }

        // Constructor
        public GameWidgetView() {
            VisualElement = UIFactory.Game.GameWidget( out var view );
            Widget = view.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
