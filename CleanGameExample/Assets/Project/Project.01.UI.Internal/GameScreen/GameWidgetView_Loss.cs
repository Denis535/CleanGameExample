#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView_Loss : UIViewBase {

        private Widget widget;
        private Label title;
        private Label message;
        private Button tryAgain;
        private Button back;

        // Props
        public override int Layer => -1000;

        // Constructor
        public GameWidgetView_Loss() {
            VisualElement = VisualElementFactory_Game.LossWidget( out widget, out title, out message, out tryAgain, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
