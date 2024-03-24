#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class UIFactoryExtensions_Main {

        // MainWidget
        public static Widget MainWidget(this UIFactory factory, out Widget widget) {
            using (factory.Widget( "main-widget" ).AsScope( out widget )) {
            }
            return widget;
        }

        // MainMenuWidget
        public static Widget MainMenuWidget(this UIFactory factory, out Widget widget, out Label title, out ColumnScope mainPageSlot, out ColumnScope startGamePageSlot) {
            using (factory.LeftWidget( "main-menu-widget" ).AsScope( out widget )) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        factory.Label( "Main Menu" ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope()) {
                        factory.ColumnScope().AddToScope( out mainPageSlot );
                        factory.ColumnScope().AddToScope( out startGamePageSlot );
                    }
                }
            }
            return widget;
        }
        public static ColumnScope MainMenuWidget_MainPage(this UIFactory factory, out ColumnScope scope, out Button startGame, out Button settings, out Button quit) {
            using (factory.ColumnScope().AsScope( out scope )) {
                factory.Select( "Start Game" ).AddToScope( out startGame );
                factory.Select( "Settings" ).AddToScope( out settings );
                factory.Select( "Quit" ).AddToScope( out quit );
            }
            return scope;
        }
        public static ColumnScope MainMenuWidget_StartGamePage(this UIFactory factory, out ColumnScope scope, out Button newGame, out Button @continue, out Button back) {
            using (factory.ColumnScope().AsScope( out scope )) {
                factory.Select( "New Game" ).AddToScope( out newGame );
                factory.Select( "Continue" ).AddToScope( out @continue );
                factory.Select( "Back" ).AddToScope( out back );
            }
            return scope;
        }

        // LoadingWidget
        public static Widget LoadingWidget(this UIFactory factory, out Widget widget, out Label loading) {
            using (factory.Widget( "loading-widget" ).AsScope( out widget )) {
                using (factory.ColumnScope().Classes( "padding-2pc", "grow-1", "justify-content-end", "align-items-center" ).AsScope()) {
                    factory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold" ).AddToScope( out loading );
                }
            }
            return widget;
        }

    }
}
