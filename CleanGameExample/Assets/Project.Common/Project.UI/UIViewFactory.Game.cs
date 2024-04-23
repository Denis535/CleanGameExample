#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static partial class UIViewFactory {
        public static class Game {

            // GameWidget
            public static Widget GameWidget(out Widget widget) {
                using (VisualElementFactory.Widget().Name( "game-widget" ).AsScope( out widget )) {
                    VisualElementFactory.Label( "+" )
                        .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                        .Style( i => i.translate = new StyleTranslate( new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) ) )
                        .AddToScope();
                }
                return widget;
            }

            // GameMenuWidget
            public static Widget GameMenuWidget(out Widget widget, out Label title, out Button resume, out Button settings, out Button back) {
                using (VisualElementFactory.LeftWidget().AsScope( out widget )) {
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
                return widget;
            }

        }
    }
}
