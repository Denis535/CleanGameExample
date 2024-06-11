#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class TotalsGameWidget : UIWidgetBase2<TotalsGameWidgetView> {

        // Container
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Game Game => Application.Game!;
        // View
        public override TotalsGameWidgetView View { get; }

        // Constructor
        public TotalsGameWidget(IDependencyContainer container) : base( container ) {
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
        private static TotalsGameWidgetView CreateView(TotalsGameWidget widget) {
            if (widget.Game.Player.State is PlayerState.Winner) {
                var view = new WinTotalsGameWidgetView();
                return view;
            }
            if (widget.Game.Player.State is PlayerState.Loser) {
                var view = new LossTotalsGameWidgetView();
                return view;
            }
            throw Exceptions.Internal.NotSupported( $"PlayerState {widget.Game.Player.State} is not supported" );
        }

    }
}
