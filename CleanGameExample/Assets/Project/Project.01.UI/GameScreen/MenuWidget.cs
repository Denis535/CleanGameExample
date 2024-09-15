#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidget : UIWidgetBase2<MenuWidgetView> {

        private UIRouter Router { get; }

        public MenuWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
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
            Dispose();
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
        private static MenuWidgetView CreateView(MenuWidget widget) {
            var view = new MenuWidgetView();
            view.Resume.RegisterCallback<ClickEvent>( evt => {
                widget.RemoveSelf();
            } );
            view.Settings.RegisterCallback<ClickEvent>( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
            } );
            return view;
        }

    }
}
