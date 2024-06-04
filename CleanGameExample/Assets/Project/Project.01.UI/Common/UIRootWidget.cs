#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class UIRootWidget : UIRootWidgetBase<UIRootWidgetView> {

        // View
        public override UIRootWidgetView View { get; }

        // Constructor
        public UIRootWidget() {
            View = CreateView<UIRootWidgetView>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
        }

        // ShowView
        public override void ShowView(UIViewBase view) {
            View.AddView( view );
        }
        public override void HideView(UIViewBase view) {
            View.RemoveView( view );
        }

        // Update
        public void Update() {
            foreach (var child in Children) {
                (child as MainWidget)?.Update();
                (child as GameWidget)?.Update();
            }
        }
        public void LateUpdate() {
            foreach (var child in Children) {
                (child as MainWidget)?.LateUpdate();
                (child as GameWidget)?.LateUpdate();
            }
        }

    }
}
