#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        public MainWidgetView() : base( "main-widget-view", "widget-view", "main-widget-view-background" ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
