#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Game {

        // GameWidget
        public static Widget GameWidget(out Widget widget, out VisualElement target) {
            using (VisualElementFactory.Widget().Name( "game-widget" ).AsScope( out widget )) {
                VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
                    .AddToScope( out target );
                return widget;
            }
        }

        // GameMenuWidget
        public static Widget GameMenuWidget(out Widget widget, out Label title, out Button resume, out Button settings, out Button back) {
            using (VisualElementFactory.LeftWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        VisualElementFactory.Label( "Game Menu" ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        VisualElementFactory.Select_Resume( "Resume" ).AddToScope( out resume );
                        VisualElementFactory.Select( "Settings" ).AddToScope( out settings );
                        VisualElementFactory.Select_Back( "Back To Main Menu" ).AddToScope( out back );
                    }
                }
                return widget;
            }
        }

        // WinWidget
        public static Widget WinWidget(out Widget widget, out Label title, out Label message, out Button nextLevel, out Button back) {
            using (VisualElementFactory.SmallWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        VisualElementFactory.Label( "Congratulations" ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            VisualElementFactory.Label( "Congratulations!\nYou win!!!" ).Classes( "text-align-middle-center" ).AddToScope( out message );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        VisualElementFactory.Submit( "Next Level" ).AddToScope( out nextLevel );
                        VisualElementFactory.Cancel( "Back to Main Menu" ).AddToScope( out back );
                    }
                }
                return widget;
            }
        }

        // LossWidget
        public static Widget LossWidget(out Widget widget, out Label title, out Label message, out Button tryAgain, out Button back) {
            using (VisualElementFactory.SmallWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        VisualElementFactory.Label( "We're sorry" ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            VisualElementFactory.Label( "We're sorry.\nYou lose." ).Classes( "text-align-middle-center" ).AddToScope( out message );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        VisualElementFactory.Submit( "Try Again" ).AddToScope( out tryAgain );
                        VisualElementFactory.Cancel( "Back to Main Menu" ).AddToScope( out back );
                    }
                }
                return widget;
            }
        }

    }
}
