#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MainWidget : UIWidgetBase2<MainWidgetView> {

        // Deps
        private Application2 Application { get; }
        // View
        public override MainWidgetView View { get; }

        // Constructor
        public MainWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
            AddChild( new MenuWidget( Container ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override async void OnActivate(object? argument) {
            try {
                ShowSelf();
                Children.OfType<MenuWidget>().First().View.SetDisplayed( false );
                await Application.InitializeAsync( DisposeCancellationToken );
                Children.OfType<MenuWidget>().First().View.SetDisplayed( true );
            } catch (OperationCanceledException) {
            }
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

        // Update
        public void Update() {
        }
        public void LateUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

    }
}
