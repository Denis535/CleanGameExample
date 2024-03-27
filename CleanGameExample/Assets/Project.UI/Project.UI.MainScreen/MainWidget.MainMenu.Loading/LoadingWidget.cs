#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class LoadingWidget : UIWidgetBase<LoadingWidgetView> {

        // Globals
        private UIFactory Factory { get; }

        // Constructor
        public LoadingWidget() {
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            View = CreateView( this, Factory );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
        }

        // Helpers
        private static LoadingWidgetView CreateView(LoadingWidget widget, UIFactory factory) {
            var view = new LoadingWidgetView( factory );
            return view;
        }

    }
}
