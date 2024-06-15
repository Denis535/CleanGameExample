#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class TotalsWidget : UIWidgetBase2<TotalsWidgetView> {

        // Container
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Game Game => Application.Game!;
        // View
        public override TotalsWidgetView View { get; }

        // Constructor
        public TotalsWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
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
                    var view = new LevelCompletedWidgetView();
                    return view;
                } else {
                    var view = new GameCompletedWidgetView();
                    return view;
                }
            }
            if (widget.Game.Player.State is PlayerState.Loser) {
                var view = new LevelFailedWidgetView();
                return view;
            }
            throw Exceptions.Internal.NotSupported( $"PlayerState {widget.Game.Player.State} is not supported" );
        }

    }
}
