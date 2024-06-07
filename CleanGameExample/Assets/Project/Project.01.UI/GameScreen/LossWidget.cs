#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class LossWidget : UIWidgetBase<LossWidgetView> {

        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        // Entities
        private Game Game => Application.Game!;
        // View
        public override LossWidgetView View { get; }

        // Constructor
        public LossWidget(IDependencyContainer container) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        public override void OnActivate(object? argument) {
            ShowSelf();
        }
        public override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // OnDescendantActivate
        public override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static LossWidgetView CreateView(LossWidget widget) {
            var view = new LossWidgetView();
            return view;
        }

    }
}
