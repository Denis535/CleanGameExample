#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.Framework.UI;

    public abstract class TotalsWidgetView : SmallWidgetView {

        public TotalsWidgetView(string name) : base( name ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_LevelCompleted : TotalsWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }
        public Button Continue { get; }
        public Button Back { get; }

        public TotalsWidgetView_LevelCompleted() : base( "level-completed-totals-widget-view" ) {
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Level Completed" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "Congratulations!\n" +
                                "You have completed the level!\n" +
                                "Do you want to continue or back to the menu?"
                                ).Classes( "text-align-middle-center" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Continue = VisualElementFactory.Submit( "Continue" ),
                        Back = VisualElementFactory.Cancel( "Back To Menu" )
                    )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_GameCompleted : TotalsWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Footer Footer { get; }
        public Label Message { get; }
        public Button Okey { get; }

        public TotalsWidgetView_GameCompleted() : base( "game-completed-totals-widget-view" ) {
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Game Completed" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "Congratulations!\n" +
                                "You have completed the game!"
                                ).Classes( "text-align-middle-center" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Okey = VisualElementFactory.Submit( "Ok" )
                    )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_LevelFailed : TotalsWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }
        public Button Retry { get; }
        public Button Back { get; }

        public TotalsWidgetView_LevelFailed() : base( "level-failed-totals-widget-view" ) {
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Level Failed" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "We're sorry.\n" +
                                "You have failed the level.\n" +
                                "Do you want to retry or back to the menu?"
                                ).Classes( "text-align-middle-center" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Retry = VisualElementFactory.Submit( "Retry" ),
                        Back = VisualElementFactory.Cancel( "Back To Menu" )
                    )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
