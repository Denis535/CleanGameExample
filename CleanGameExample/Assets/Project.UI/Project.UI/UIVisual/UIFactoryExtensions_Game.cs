#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class UIFactoryExtensions_Game {

        // GameWidget
        public static Widget GameWidget(this UIFactory factory, out Widget widget) {
            using (factory.Widget( "game-widget" ).AsScope( out widget )) {
            }
            return widget;
        }

        // GameMenuWidget
        public static Widget GameMenuWidget(this UIFactory factory, out Widget widget, out Label title, out Button resume, out Button settings, out Button back) {
            using (factory.LeftWidget( "game-menu-widget" ).AsScope( out widget )) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        factory.Label( "Game Menu" ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope()) {
                        factory.Select( "Resume" ).AddToScope( out resume );
                        factory.Select( "Settings" ).AddToScope( out settings );
                        factory.Select( "Back To Main Menu" ).AddToScope( out back );
                    }
                }
            }
            return widget;
        }

    }
}
