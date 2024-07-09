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

        protected override VisualElement VisualElement => widget;

        public MainWidgetView() {
            VisualElementFactory_Main.Main( this, out widget );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
