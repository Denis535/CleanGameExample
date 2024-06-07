#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class LoadingWidget : UIWidgetBase<LoadingWidgetView> {

        // View
        public override LoadingWidgetView View { get; }

        // Constructor
        public LoadingWidget() {
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

        // Helpers
        private static LoadingWidgetView CreateView(LoadingWidget widget) {
            var view = new LoadingWidgetView();
            return view;
        }

    }
}
