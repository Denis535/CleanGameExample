#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class TotalsWidget : UIWidgetBase2<TotalsWidgetView> {

        private UIRouter Router { get; }
        private Game Game { get; }

        public TotalsWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Game = container.RequireDependency<Game>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static TotalsWidgetView CreateView(TotalsWidget widget) {
            if (widget.Game.Player.State is PlayerState.Winner) {
                if (!widget.Game.Info.Level.IsLast()) {
                    var view = new TotalsWidgetView_LevelCompleted();
                    view.Continue.RegisterCallback<ClickEvent>( evt => {
                        var gameInfo = widget.Game.Info;
                        gameInfo = gameInfo with { Level = gameInfo.Level.GetNext() };
                        var playerInfo = widget.Game.Player.Info;
                        widget.Router.ReloadGameScene( gameInfo, playerInfo );
                    } );
                    view.Back.RegisterCallback<ClickEvent>( evt => {
                        widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
                    } );
                    return view;
                } else {
                    var view = new TotalsWidgetView_GameCompleted();
                    view.Okey.RegisterCallback<ClickEvent>( evt => {
                        widget.Router.UnloadGameScene();
                    } );
                    return view;
                }
            }
            if (widget.Game.Player.State is PlayerState.Loser) {
                var view = new TotalsWidgetView_LevelFailed();
                view.Retry.RegisterCallback<ClickEvent>( evt => {
                    var gameInfo = widget.Game.Info;
                    var playerInfo = widget.Game.Player.Info;
                    widget.Router.ReloadGameScene( gameInfo, playerInfo );
                } );
                view.Back.RegisterCallback<ClickEvent>( evt => {
                    widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
                } );
                return view;
            }
            throw Exceptions.Internal.NotSupported( $"PlayerState {widget.Game.Player.State} is not supported" );
        }

    }
}
