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
    using UnityEngine.UIElements;

    public class UIRootWidget : UnityEngine.Framework.UI.UIRootWidget {

        // Constructor
        public UIRootWidget() {
            VisualElementFactory.OnWidgetAttach += visualElement => {
                var view = (UIViewBase) visualElement.userData;
                if (!view.LoadFocus()) view.Focus();
            };
            VisualElementFactory.OnViewAttach += visualElement => {
                var view = (UIViewBase) visualElement.userData;
                if (!view.LoadFocus()) view.Focus();
            };
            VisualElementFactory.OnWidgetDetach += visualElement => {
                var view = (UIViewBase) visualElement.userData;
                view.SaveFocus();
            };
            VisualElementFactory.OnViewDetach += visualElement => {
                var view = (UIViewBase) visualElement.userData;
                view.SaveFocus();
            };
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
            if (view.IsModal()) {
                {
                    View.ViewSlot.Children.LastOrDefault().SaveFocus();
                    View.ViewSlot.SetEnabled( false );
                }
                Push( View.ModalViewSlot, view, i => i is not MainWidgetView or GameWidgetView );
            } else {
                Push( View.ViewSlot, view, i => i is not MainWidgetView or GameWidgetView );
            }
        }
        public override void HideView(UIViewBase view) {
            if (view.IsModal()) {
                Pop( View.ModalViewSlot, view, i => i is not MainWidgetView or GameWidgetView );
                if (!View.ModalViewSlot.Children.Any()) {
                    View.ViewSlot.SetEnabled( true );
                    View.ViewSlot.Children.LastOrDefault().LoadFocus();
                }
            } else {
                Pop( View.ViewSlot, view, i => i is not MainWidgetView or GameWidgetView );
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
