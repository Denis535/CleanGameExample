#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class GameMenuWidget : UIWidgetBase<GameMenuWidgetView> {

        // Container
        private IDependencyContainer Container { get; }
        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        // Entities
        private Game Game => Application.Game!;
        // View
        public override GameMenuWidgetView View { get; }

        // Constructor
        public GameMenuWidget(IDependencyContainer container) {
            Container = container;
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
        private static GameMenuWidgetView CreateView(GameMenuWidget widget) {
            var view = new GameMenuWidgetView();
            view.OnResume( evt => {
                widget.RemoveSelf();
            } );
            view.OnSettings( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.OnBack( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.LoadMainSceneAsync().Throw() ).OnCancel( "No", null );
                widget.AddChild( dialog );
            } );
            return view;
        }

    }
}
