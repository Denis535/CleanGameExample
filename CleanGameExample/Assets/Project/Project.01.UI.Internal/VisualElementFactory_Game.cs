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
            using (VisualElementFactory.Widget().Name( "game-widget" ).AsScope( out widget )) {
                target = VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) );
                return widget;
            }
        }

        // Menu
        public static Widget Menu(out Widget widget, out Label title, out Button resume, out Button settings, out Button back) {
            using (VisualElementFactory.LeftWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Menu" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        resume = VisualElementFactory.Resume( "Resume" );
                        settings = VisualElementFactory.Select( "Settings" );
                        back = VisualElementFactory.Back( "Back To Main Menu" );
                    }
                }
                return widget;
            }
        }

        // WinTotals
        public static Widget WinTotals(out Widget widget, out Label title, out Label message, out Button nextLevel, out Button back) {
            using (VisualElementFactory.SmallWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Congratulations" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( "Congratulations!\nYou win!!!" ).Classes( "text-align-middle-center" );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        nextLevel = VisualElementFactory.Submit( "Next Level" );
                        back = VisualElementFactory.Cancel( "Back to Main Menu" );
                    }
                }
                return widget;
            }
        }

        // LossTotals
        public static Widget LossTotals(out Widget widget, out Label title, out Label message, out Button tryAgain, out Button back) {
            using (VisualElementFactory.SmallWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "We're sorry" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( "We're sorry.\nYou lose." ).Classes( "text-align-middle-center" );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        tryAgain = VisualElementFactory.Submit( "Try Again" );
                        back = VisualElementFactory.Cancel( "Back to Main Menu" );
                    }
                }
                return widget;
            }
        }

    }
}
