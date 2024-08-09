#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class TotalsWidgetView : UIViewBase {

        public TotalsWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_LevelCompleted : TotalsWidgetView {

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public Label Title { get; }
        public Label Message { get; }
        public Button Continue { get; }
        public Button Back { get; }

        public TotalsWidgetView_LevelCompleted() {
            Widget = VisualElementFactory.SmallWidget( "level-completed-widget" ).UserData( this ).Children(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Level Completed" )
                    ),
                    VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "Congratulations!\n" +
                                "You have completed the level!\n" +
                                "Do you want to continue or back to the menu?"
                                ).Classes( "text-align-middle-center" )
                        )
                    ),
                    VisualElementFactory.Footer().Children(
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

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public Label Title { get; }
        public Label Message { get; }
        public Button Okey { get; }

        public TotalsWidgetView_GameCompleted() {
            Widget = VisualElementFactory.SmallWidget( "game-completed-widget" ).UserData( this ).Children(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Game Completed" )
                    ),
                    VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "Congratulations!\n" +
                                "You have completed the game!"
                                ).Classes( "text-align-middle-center" )
                        )
                    ),
                    VisualElementFactory.Footer().Children(
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

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public Label Title { get; }
        public Label Message { get; }
        public Button Retry { get; }
        public Button Back { get; }

        public TotalsWidgetView_LevelFailed() {
            Widget = VisualElementFactory.SmallWidget( "level-failed-widget" ).UserData( this ).Children(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Level Failed" )
                    ),
                    VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "We're sorry.\n" +
                                "You have failed the level.\n" +
                                "Do you want to retry or back to the menu?"
                                ).Classes( "text-align-middle-center" )
                        )
                    ),
                    VisualElementFactory.Footer().Children(
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
