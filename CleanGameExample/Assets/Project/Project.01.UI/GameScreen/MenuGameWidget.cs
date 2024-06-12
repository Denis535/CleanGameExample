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

    public class MenuGameWidget : UIWidgetBase2<MenuGameWidgetView> {

        // Container
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Game Game => Application.Game!;
        // View
        public override MenuGameWidgetView View { get; }

        // Constructor
        public MenuGameWidget(IDependencyContainer container) : base( container ) {
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
        private static MenuGameWidgetView CreateView(MenuGameWidget widget) {
            var view = new MenuGameWidgetView();
            view.OnResume.Register( evt => {
                widget.RemoveSelf();
            } );
            view.OnSettings.Register( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.OnBack.Register( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.LoadMainSceneAsync().Throw() ).OnCancel( "No", null );
                widget.AddChild( dialog );
            } );
            return view;
        }

    }
}
