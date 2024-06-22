#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase2 {

        private readonly Widget widget;

        // Constructor
        public MainWidgetView() {
            VisualElement = VisualElementFactory_Main.Main( out widget );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
