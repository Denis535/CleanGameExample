#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Main {

        // MainWidget
        public static Widget MainWidget(out Widget widget) {
            using (VisualElementFactory.Widget().Name( "main-widget" ).AsScope( out widget )) {
                return widget;
            }
        }

        // MainMenuWidget
        public static Widget MainMenuWidget(out Widget widget, out Label title, out VisualElement content) {
            using (VisualElementFactory.LeftWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        VisualElementFactory.Label( "Main Menu" ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                    }
                }
                return widget;
            }
        }
        public static VisualElement MainMenuWidget_MainMenuView(out VisualElement view, out Button startGame, out Button settings, out Button quit) {
            using (VisualElementFactory.View().AsScope( out view )) {
                VisualElementFactory.Select( "Start Game" ).AddToScope( out startGame );
                VisualElementFactory.Select( "Settings" ).AddToScope( out settings );
                VisualElementFactory.QuitSelect( "Quit" ).AddToScope( out quit );
                return view;
            }
        }
        public static VisualElement MainMenuWidget_StartGameView(out VisualElement view, out Button newGame, out Button @continue, out Button back) {
            using (VisualElementFactory.View().AsScope( out view )) {
                VisualElementFactory.Select( "New Game" ).AddToScope( out newGame );
                VisualElementFactory.Select( "Continue" ).AddToScope( out @continue );
                VisualElementFactory.BackSelect( "Back" ).AddToScope( out back );
                return view;
            }
        }
        public static VisualElement MainMenuWidget_SelectLevelView(out VisualElement view, out Button level1, out Button level2, out Button level3, out Button back) {
            using (VisualElementFactory.View().AsScope( out view )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    VisualElementFactory.Select( "Level 1" ).AddToScope( out level1 );
                    VisualElementFactory.Select( "Level 2" ).AddToScope( out level2 );
                    VisualElementFactory.Select( "Level 3" ).AddToScope( out level3 );
                }
                VisualElementFactory.BackSelect( "Back" ).AddToScope( out back );
                return view;
            }
        }
        public static VisualElement MainMenuWidget_SelectCharacterView(out VisualElement view, out Button gray, out Button red, out Button green, out Button blue, out Button back) {
            using (VisualElementFactory.View().AsScope( out view )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    VisualElementFactory.Select( "Gray" ).AddToScope( out gray );
                    VisualElementFactory.Select( "Red" ).AddToScope( out red );
                    VisualElementFactory.Select( "Green" ).AddToScope( out green );
                    VisualElementFactory.Select( "Blue" ).AddToScope( out blue );
                }
                VisualElementFactory.BackSelect( "Back" ).AddToScope( out back );
                return view;
            }
        }

        // LoadingWidget
        public static Widget LoadingWidget(out Widget widget, out Label loading) {
            using (VisualElementFactory.Widget().AsScope( out widget )) {
                using (VisualElementFactory.ColumnScope().Classes( "margin-2pc", "grow-1", "justify-content-end", "align-items-center" ).AsScope()) {
                    VisualElementFactory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold" ).AddToScope( out loading );
                }
                return widget;
            }
        }

    }
}
