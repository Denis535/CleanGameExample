#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        public Widget Widget { get; }

        public MainWidgetView() : base( "main-widget-view" ) {
            Add(
                Widget = VisualElementFactory.Widget( "main-widget" ).Classes( "main-widget-background" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
