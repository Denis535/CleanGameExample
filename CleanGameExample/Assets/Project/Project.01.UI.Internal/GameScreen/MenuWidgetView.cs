#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.Framework.UI;

    public class MenuWidgetView : LeftWidgetView {

        public Label Title { get; }
        public Button Resume { get; }
        public Button Settings { get; }
        public Button Back { get; }

        public MenuWidgetView() : base( "menu-widget-view" ) {
            Add(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Menu" )
                    ),
                    VisualElementFactory.Content().Children(
                        Resume = VisualElementFactory.Resume( "Resume" ),
                        Settings = VisualElementFactory.Select( "Settings" ),
                        Back = VisualElementFactory.Back( "Back To Menu" )
                    )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
