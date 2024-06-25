#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MenuWidget : UIWidgetBase2<MenuWidgetView> {

        // Framework
        private UIRouter Router { get; }
        // View
        public override MenuWidgetView View { get; }

        // Constructor
        public MenuWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
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
        private static MenuWidgetView CreateView(MenuWidget widget) {
            var view = new MenuWidgetView();
            view.OnResume += evt => {
                widget.RemoveSelf();
            };
            view.OnSettings += evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            };
            view.OnBack += evt => {
                widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
            };
            return view;
        }

    }
}
