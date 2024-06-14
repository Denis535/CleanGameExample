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
            using (VisualElementFactory.Widget().Name( "main-widget" ).AsScope( out widget )) {
                return widget;
            }
        }

        // Menu
        public static Widget Menu(out Widget widget, out Label title, out VisualElement content) {
            using (VisualElementFactory.LeftWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Menu" );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                    }
                }
                return widget;
            }
        }
        public static VisualElement Menu_Menu(out VisualElement view, out Button startGame, out Button settings, out Button quit) {
            using (VisualElementFactory.View().AsScope( out view )) {
                startGame = VisualElementFactory.Select( "Start Game" );
                settings = VisualElementFactory.Select( "Settings" );
                quit = VisualElementFactory.Quit( "Quit" );
                return view;
            }
        }
        public static VisualElement Menu_StartGame(out VisualElement view, out Button newGame, out Button @continue, out Button back) {
            using (VisualElementFactory.View().AsScope( out view )) {
                newGame = VisualElementFactory.Select( "New Game" );
                @continue = VisualElementFactory.Select( "Continue" );
                back = VisualElementFactory.Back( "Back" );
                return view;
            }
        }
        public static VisualElement Menu_SelectLevel(out VisualElement view, out Button level1, out Button level2, out Button level3, out Button back) {
            using (VisualElementFactory.View().AsScope( out view )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    level1 = VisualElementFactory.Select( "Level 1" );
                    level2 = VisualElementFactory.Select( "Level 2" );
                    level3 = VisualElementFactory.Select( "Level 3" );
                }
                back = VisualElementFactory.Back( "Back" );
                return view;
            }
        }
        public static VisualElement Menu_SelectCharacter(out VisualElement view, out Button gray, out Button red, out Button green, out Button blue, out Button back) {
            using (VisualElementFactory.View().AsScope( out view )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    gray = VisualElementFactory.Select( "Gray" );
                    red = VisualElementFactory.Select( "Red" );
                    green = VisualElementFactory.Select( "Green" );
                    blue = VisualElementFactory.Select( "Blue" );
                }
                back = VisualElementFactory.Back( "Back" );
                return view;
            }
        }

        // Loading
        public static Widget Loading(out Widget widget, out Label loading) {
            using (VisualElementFactory.Widget().AsScope( out widget )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-2pc", "grow-1", "justify-content-end", "align-items-center" ).AsScope()) {
                    loading = VisualElementFactory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold" );
                }
                return widget;
            }
        }

    }
}
