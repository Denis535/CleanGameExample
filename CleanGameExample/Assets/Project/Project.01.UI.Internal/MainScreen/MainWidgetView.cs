#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }

        public MainWidgetView() {
            Widget = VisualElementFactory.Widget( "main-widget" ).Classes( "main-widget-background" ).UserData( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
