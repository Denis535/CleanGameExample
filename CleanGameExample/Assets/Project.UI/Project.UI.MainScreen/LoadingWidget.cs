#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class LoadingWidget : UIWidgetBase<LoadingWidgetView> {

        // Constructor
        public LoadingWidget() {
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            ShowWidget( this );
        }
        public override void OnDetach(object? argument) {
            HideWidget( this );
        }

        // Helpers
        private static LoadingWidgetView CreateView(LoadingWidget widget) {
            var view = new LoadingWidgetView();
            return view;
        }

    }
}
