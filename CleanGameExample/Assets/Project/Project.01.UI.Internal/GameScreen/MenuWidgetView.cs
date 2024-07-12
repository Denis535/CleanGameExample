#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public Label Title { get; }
        public Button Resume { get; }
        public Button Settings { get; }
        public Button Back { get; }

        public MenuWidgetView() {
            Widget = VisualElementFactory.LeftWidget( "menu-widget" ).UserData( this ).Children(
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
