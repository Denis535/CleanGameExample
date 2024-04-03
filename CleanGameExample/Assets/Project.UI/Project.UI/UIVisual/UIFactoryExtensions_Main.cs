#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class UIFactoryExtensions_Main {

        // MainWidget
        public static Widget MainWidget(this UIFactory factory, UIViewBase view, out Widget widget) {
            using (factory.Widget( view ).Name( "main-widget" ).AsScope( out widget )) {
            }
            return widget;
        }

        // MainMenuWidget
        public static Widget MainMenuWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Label title, out VisualElement contentSlot) {
            using (factory.LeftWidget( view ).AsScope( out widget )) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        factory.Label( "Main Menu" ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope( out contentSlot )) {
                    }
                }
            }
            return widget;
        }
        public static VisualElement MainMenuWidget_MainMenuView(this UIFactory factory, UIViewBase view, out VisualElement root, out Button startGame, out Button settings, out Button quit) {
            using (factory.View( view ).AsScope( out root )) {
                factory.Select( "Start Game" ).AddToScope( out startGame );
                factory.Select( "Settings" ).AddToScope( out settings );
                factory.Select( "Quit" ).AddToScope( out quit );
            }
            return root;
        }
        public static VisualElement MainMenuWidget_StartGameView(this UIFactory factory, UIViewBase view, out VisualElement root, out Button newGame, out Button @continue, out Button back) {
            using (factory.View( view ).AsScope( out root )) {
                factory.Select( "New Game" ).AddToScope( out newGame );
                factory.Select( "Continue" ).AddToScope( out @continue );
                factory.Select( "Back" ).AddToScope( out back );
            }
            return root;
        }
        public static VisualElement MainMenuWidget_SelectLevelView(this UIFactory factory, UIViewBase view, out VisualElement root, out Button level1, out Button level2, out Button level3, out Button back) {
            using (factory.View( view ).AsScope( out root )) {
                using (factory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    factory.Select( "Level 1" ).AddToScope( out level1 );
                    factory.Select( "Level 2" ).AddToScope( out level2 );
                    factory.Select( "Level 3" ).AddToScope( out level3 );
                }
                factory.Select( "Back" ).AddToScope( out back );
            }
            return root;
        }
        public static VisualElement MainMenuWidgetView_SelectYourCharacterView(this UIFactory factory, UIViewBase view, out VisualElement root, out Button gray, out Button red, out Button green, out Button blue, out Button back) {
            using (factory.View( view ).AsScope( out root )) {
                using (factory.ColumnScope().Classes( "margin-bottom-4px" ).AsScope()) {
                    factory.Select( "Gray" ).AddToScope( out gray );
                    factory.Select( "Red" ).AddToScope( out red );
                    factory.Select( "Green" ).AddToScope( out green );
                    factory.Select( "Blue" ).AddToScope( out blue );
                }
                factory.Select( "Back" ).AddToScope( out back );
            }
            return root;
        }

        // LoadingWidget
        public static Widget LoadingWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Label loading) {
            using (factory.Widget( view ).AsScope( out widget )) {
                using (factory.ColumnScope().Classes( "margin-2pc", "grow-1", "justify-content-end", "align-items-center" ).AsScope()) {
                    factory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold" ).AddToScope( out loading );
                }
            }
            return widget;
        }

    }
}
