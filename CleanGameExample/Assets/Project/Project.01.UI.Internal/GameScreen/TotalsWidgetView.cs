#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class TotalsWidgetView : UIViewBase2 {

        public TotalsWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_LevelCompleted : TotalsWidgetView {

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private Label Title { get; }
        private Label Message { get; }
        private Button Continue { get; }
        private Button Back { get; }

        public event EventCallback<ClickEvent> OnContinueEvent {
            add => Continue.RegisterCallback( value );
            remove => Continue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => Back.RegisterCallback( value );
            remove => Back.UnregisterCallback( value );
        }

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
        private Widget Widget { get; }
        private Label Title { get; }
        private Label Message { get; }
        private Button Okey { get; }

        public event EventCallback<ClickEvent> OnOkeyEvent {
            add => Okey.RegisterCallback( value );
            remove => Okey.UnregisterCallback( value );
        }

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
        private Widget Widget { get; }
        private Label Title { get; }
        private Label Message { get; }
        private Button Retry { get; }
        private Button Back { get; }

        public event EventCallback<ClickEvent> OnRetryEvent {
            add => Retry.RegisterCallback( value );
            remove => Retry.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => Back.RegisterCallback( value );
            remove => Back.UnregisterCallback( value );
        }

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
