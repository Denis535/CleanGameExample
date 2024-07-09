#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Main {

        public static void Main(UIViewBase view, out Widget widget) {
            using (VisualElementFactory.Widget( "main-widget" ).Classes( "main-widget-background" ).UserData( view ).AsScope().Out( out widget )) {
            }
        }

        public static void Menu(UIViewBase view, out Widget widget, out Label title, out VisualElement views) {
            using (VisualElementFactory.LeftWidget( "menu-widget" ).UserData( view ).AsScope().Out( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Menu" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        views = VisualElementFactory.VisualElement();
                    }
                }
            }
        }
        public static void Menu_Menu(UIViewBase view, out ColumnScope scope, out Button startGame, out Button settings, out Button quit) {
            using (VisualElementFactory.ColumnScope().UserData( view ).AsScope().Out( out scope )) {
                startGame = VisualElementFactory.Select( "Start Game" );
                settings = VisualElementFactory.Select( "Settings" );
                quit = VisualElementFactory.Quit( "Quit" );
            }
        }
        public static void Menu_StartGame(UIViewBase view, out ColumnScope scope, out Button newGame, out Button @continue, out Button back) {
            using (VisualElementFactory.ColumnScope().UserData( view ).AsScope().Out( out scope )) {
                newGame = VisualElementFactory.Select( "New Game" );
                @continue = VisualElementFactory.Select( "Continue" );
                back = VisualElementFactory.Back( "Back" );
            }
        }
        public static void Menu_SelectLevel(UIViewBase view, out ColumnScope scope, out Button level1, out Button level2, out Button level3, out Button back) {
            using (VisualElementFactory.ColumnScope().UserData( view ).AsScope().Out( out scope )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    level1 = VisualElementFactory.Select( "Level 1" );
                    level2 = VisualElementFactory.Select( "Level 2" );
                    level3 = VisualElementFactory.Select( "Level 3" );
                }
                back = VisualElementFactory.Back( "Back" );
            }
        }
        public static void Menu_SelectCharacter(UIViewBase view, out ColumnScope scope, out Button gray, out Button red, out Button green, out Button blue, out Button back) {
            using (VisualElementFactory.ColumnScope().UserData( view ).AsScope().Out( out scope )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    gray = VisualElementFactory.Select( "Gray" );
                    red = VisualElementFactory.Select( "Red" );
                    green = VisualElementFactory.Select( "Green" );
                    blue = VisualElementFactory.Select( "Blue" );
                }
                back = VisualElementFactory.Back( "Back" );
            }
        }

    }
}
