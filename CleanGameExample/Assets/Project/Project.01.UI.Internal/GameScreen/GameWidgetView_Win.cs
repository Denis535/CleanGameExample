#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView_Win : UIViewBase {

        private Widget widget;
        private Label title;
        private Label message;
        private Button nextLevel;
        private Button back;

        // Props
        public override int Layer => -1000;

        // Constructor
        public GameWidgetView_Win() {
            VisualElement = VisualElementFactory_Game.WinWidget( out widget, out title, out message, out nextLevel, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
