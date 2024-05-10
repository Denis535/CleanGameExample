#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class GameViewFactory {

        // GameWidget
        public static Widget GameWidget(out Widget root, out VisualElement target) {
            using (VisualElementFactory.Widget().Name( "game-widget" ).AsScope( out root )) {
                VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
                    .AddToScope( out target );
            }
            return root;
        }

        // GameMenuWidget
        public static Widget GameMenuWidget(out Widget root, out Label title, out Button resume, out Button settings, out Button back) {
            using (VisualElementFactory.LeftWidget().AsScope( out root )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        VisualElementFactory.Label( "Game Menu" ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        VisualElementFactory.Select( "Resume" ).AddToScope( out resume );
                        VisualElementFactory.Select( "Settings" ).AddToScope( out settings );
                        VisualElementFactory.Select( "Back To Main Menu" ).AddToScope( out back );
                    }
                }
            }
            return root;
        }

    }
}