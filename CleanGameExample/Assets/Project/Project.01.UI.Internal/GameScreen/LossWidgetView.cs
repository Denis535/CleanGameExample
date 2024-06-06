#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class LossWidgetView : UIViewBase {

        private Widget widget;
        private Label title;
        private Label message;
        private Button tryAgain;
        private Button back;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;

        // Constructor
        public LossWidgetView() {
            VisualElement = VisualElementFactory_Game.LossWidget( out widget, out title, out message, out tryAgain, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
