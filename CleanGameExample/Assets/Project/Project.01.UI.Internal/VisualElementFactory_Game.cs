#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Game {

        // Game
        public static Widget Game(out Widget widget, out VisualElement target) {
            using (VisualElementFactory.Widget( "game-widget" ).Pipe( i => i.focusable = true ).AsScope().Out( out widget )) {
                target = VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) );
                return widget;
            }
        }

        // Menu
        public static Widget Menu(out Widget widget, out Label title, out Button resume, out Button settings, out Button back) {
            using (VisualElementFactory.LeftWidget( "menu-widget" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Menu" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        resume = VisualElementFactory.Resume( "Resume" );
                        settings = VisualElementFactory.Select( "Settings" );
                        back = VisualElementFactory.Back( "Back To Menu" );
                    }
                }
                return widget;
            }
        }

        // Totals
        public static VisualElement Totals_LevelCompleted(out Widget widget, out Label title, out Label message, out Button @continue, out Button back) {
            using (VisualElementFactory.SmallWidget( "level-completed-widget" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Level Completed" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label(
                                "Congratulations!\n" +
                                "You have completed the level!\n" +
                                "Do you want to continue or back to the menu?"
                                ).Classes( "text-align-middle-center" );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        @continue = VisualElementFactory.Submit( "Continue" );
                        back = VisualElementFactory.Cancel( "Back To Menu" );
                    }
                }
            }
            return widget;
        }
        public static VisualElement Totals_GameCompleted(out Widget widget, out Label title, out Label message, out Button okey) {
            using (VisualElementFactory.SmallWidget( "game-completed-widget" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Game Completed" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label(
                                "Congratulations!\n" +
                                "You have completed the game!"
                                ).Classes( "text-align-middle-center" );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        okey = VisualElementFactory.Submit( "Ok" );
                    }
                }
            }
            return widget;
        }
        public static VisualElement Totals_LevelFailed(out Widget widget, out Label title, out Label message, out Button retry, out Button back) {
            using (VisualElementFactory.SmallWidget( "level-failed-widget" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Level Failed" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label(
                                "We're sorry.\n" +
                                "You have failed the level.\n" +
                                "Do you want to retry or back to the menu?"
                                ).Classes( "text-align-middle-center" );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        retry = VisualElementFactory.Submit( "Retry" );
                        back = VisualElementFactory.Cancel( "Back To Menu" );
                    }
                }
            }
            return widget;
        }

    }
}
