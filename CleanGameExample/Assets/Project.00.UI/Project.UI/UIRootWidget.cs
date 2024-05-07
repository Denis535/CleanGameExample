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

        // ShowWidget
        public override void ShowWidget(UIWidgetBase widget) {
            if (widget.IsViewable) {
                if (widget.IsModal()) {
                    {
                        View.WidgetSlot.Children.LastOrDefault()?.View!.SaveFocus();
                        View.WidgetSlot.SetEnabled( false );
                    }
                    Push( View.ModalWidgetSlot, widget, i => i is not MainWidget or GameWidget );
                } else {
                    Push( View.WidgetSlot, widget, i => i is not MainWidget or GameWidget );
                }
            }
        }
        public override void HideWidget(UIWidgetBase widget) {
            if (widget.IsViewable) {
                if (widget.IsModal()) {
                    Pop( View.ModalWidgetSlot, widget, i => i is not MainWidget or GameWidget );
                    if (!View.ModalWidgetSlot.Children.Any()) {
                        View.WidgetSlot.SetEnabled( true );
                        View.WidgetSlot.Children.LastOrDefault()?.View!.LoadFocus();
                    }
                } else {
                    Pop( View.WidgetSlot, widget, i => i is not MainWidget or GameWidget );
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
