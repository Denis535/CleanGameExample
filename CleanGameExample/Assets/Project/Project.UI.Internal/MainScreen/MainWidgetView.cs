#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MainWidgetView : UIViewBase {

        // Constructor
        public MainWidgetView() {
            VisualElement = ViewFactory.MainWidget( out _ );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
