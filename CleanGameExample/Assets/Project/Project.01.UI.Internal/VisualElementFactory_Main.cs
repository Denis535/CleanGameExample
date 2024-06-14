#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Main {

        // Main
        public static Widget Main(out Widget widget) {
            using (VisualElementFactory.Widget().Name( "main-widget" ).Out( out widget ).AsScope()) {
                return widget;
            }
        }

        // Menu
        public static Widget Menu(out Widget widget, out Label title, out VisualElement views) {
            using (VisualElementFactory.LeftWidget().Out( out widget ).AsScope()) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Menu" );
                    }
                    using (VisualElementFactory.Content().Out( out views ).AsScope()) {
                    }
                }
                return widget;
            }
        }
        public static ColumnScope Menu_Menu(out ColumnScope scope, out Button startGame, out Button settings, out Button quit) {
            using (VisualElementFactory.ColumnScope().Out( out scope ).AsScope()) {
                startGame = VisualElementFactory.Select( "Start Game" );
                settings = VisualElementFactory.Select( "Settings" );
                quit = VisualElementFactory.Quit( "Quit" );
                return scope;
            }
        }
        public static ColumnScope Menu_StartGame(out ColumnScope scope, out Button newGame, out Button @continue, out Button back) {
            using (VisualElementFactory.ColumnScope().Out( out scope ).AsScope()) {
                newGame = VisualElementFactory.Select( "New Game" );
                @continue = VisualElementFactory.Select( "Continue" );
                back = VisualElementFactory.Back( "Back" );
                return scope;
            }
        }
        public static ColumnScope Menu_SelectLevel(out ColumnScope scope, out Button level1, out Button level2, out Button level3, out Button back) {
            using (VisualElementFactory.ColumnScope().Out( out scope ).AsScope()) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    level1 = VisualElementFactory.Select( "Level 1" );
                    level2 = VisualElementFactory.Select( "Level 2" );
                    level3 = VisualElementFactory.Select( "Level 3" );
                }
                back = VisualElementFactory.Back( "Back" );
                return scope;
            }
        }
        public static ColumnScope Menu_SelectCharacter(out ColumnScope scope, out Button gray, out Button red, out Button green, out Button blue, out Button back) {
            using (VisualElementFactory.ColumnScope().Out( out scope ).AsScope()) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    gray = VisualElementFactory.Select( "Gray" );
                    red = VisualElementFactory.Select( "Red" );
                    green = VisualElementFactory.Select( "Green" );
                    blue = VisualElementFactory.Select( "Blue" );
                }
                back = VisualElementFactory.Back( "Back" );
                return scope;
            }
        }

        // Loading
        public static Widget Loading(out Widget widget, out Label loading) {
            using (VisualElementFactory.Widget().Out( out widget ).AsScope()) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-2pc", "grow-1", "justify-content-end", "align-items-center" ).AsScope()) {
                    loading = VisualElementFactory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold" );
                }
                return widget;
            }
        }

    }
}
