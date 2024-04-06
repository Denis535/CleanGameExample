#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static partial class UIFactory {
        public static class Game {

            // GameWidget
            public static Widget GameWidget(UIViewBase view, out Widget widget) {
                using (VisualElementFactory.Widget( view ).Name( "game-widget" ).AsScope( out widget )) {
                }
                return widget;
            }

            // GameMenuWidget
            public static Widget GameMenuWidget(UIViewBase view, out Widget widget, out Label title, out Button resume, out Button settings, out Button back) {
                using (VisualElementFactory.LeftWidget( view ).AsScope( out widget )) {
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
