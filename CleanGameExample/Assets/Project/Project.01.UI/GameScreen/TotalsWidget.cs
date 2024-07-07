#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.Framework.UI;

    public class TotalsWidget : UIWidgetBase2<TotalsWidgetView> {

        // Framework
        private UIRouter Router { get; }
        private Game Game { get; }

        // Constructor
        public TotalsWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Game = container.RequireDependency<Game>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // OnDescendantActivate
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
                if (!widget.Game.Level.IsLast()) {
                    var view = new TotalsWidgetView_LevelCompleted();
                    view.OnContinue += evt => {
                        widget.Router.ReloadGameScene( widget.Game.Name, widget.Game.Mode, widget.Game.Level.GetNext(), widget.Game.Player.Name, widget.Game.Player.Kind );
                    };
                    view.OnBack += evt => {
                        widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
                    };
                    return view;
                } else {
                    var view = new TotalsWidgetView_GameCompleted();
                    view.OnOkey += evt => {
                        widget.Router.UnloadGameScene();
                    };
                    return view;
                }
            }
            if (widget.Game.Player.State is PlayerState.Loser) {
                var view = new TotalsWidgetView_LevelFailed();
                view.OnRetry += evt => {
                    widget.Router.ReloadGameScene( widget.Game.Name, widget.Game.Mode, widget.Game.Level, widget.Game.Player.Name, widget.Game.Player.Kind );
                };
                view.OnBack += evt => {
                    widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
                };
                return view;
            }
            throw Exceptions.Internal.NotSupported( $"PlayerState {widget.Game.Player.State} is not supported" );
        }

    }
}
