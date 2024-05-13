#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class UIRootWidget : UnityEngine.Framework.UI.UIRootWidget {

        // Constructor
        public UIRootWidget() {
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
            if (!view.IsModal()) {
                View.Views.LastOrDefault()?.SaveFocus();
                View.AddView( view, i => i is MainWidgetView or GameWidgetView );
                view.Focus();
            } else {
                if (View.ModalViews.Any()) {
                    View.ModalViews.Last().SaveFocus();
                } else {
                    View.Views.LastOrDefault()?.SaveFocus();
                }
                View.AddModalView( view, i => false );
                view.Focus();
            }
        }
        public override void HideView(UIViewBase view) {
            if (!view.IsModal()) {
                View.RemoveView( view, i => i is MainWidgetView or GameWidgetView );
                View.Views.LastOrDefault()?.LoadFocus();
            } else {
                View.RemoveModalView( view, i => false );
                if (View.ModalViews.Any()) {
                    View.ModalViews.Last().LoadFocus();
                } else {
                    View.Views.LastOrDefault()?.LoadFocus();
                }
            }
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
