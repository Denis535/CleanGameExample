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
                if (visualElement.focusController.focusedElement == null) {
                    var view = (UIViewBase) visualElement.userData;
                    if (!view.LoadFocus()) visualElement.Focus();
                }
            };
            VisualElementFactory.OnViewAttach += visualElement => {
                if (visualElement.focusController.focusedElement == null) {
                    var view = (UIViewBase) visualElement.userData;
                    if (!view.LoadFocus()) visualElement.Focus();
                }
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
            base.OnAttach( argument );
        }
        public override void OnDetach(object? argument) {
            base.OnDetach( argument );
        }

        // ShowDescendantWidget
        protected override void ShowDescendantWidget(UIWidgetBase widget) {
            if (widget.IsViewable) {
                if (widget.IsModal()) {
                    {
                        View.WidgetSlot.Children.LastOrDefault()?.View!.SaveFocus();
                        View.WidgetSlot.SetEnabled( false );
                    }
                    ShowDescendantWidget( View.ModalWidgetSlot, widget );
                } else {
                    ShowDescendantWidget( View.WidgetSlot, widget );
                }
            }
        }
        protected override void HideDescendantWidget(UIWidgetBase widget) {
            if (widget.IsViewable) {
                if (widget.IsModal()) {
                    HideDescendantWidget( View.ModalWidgetSlot, widget );
                    if (!View.ModalWidgetSlot.Children.Any()) {
                        View.WidgetSlot.SetEnabled( true );
                        View.WidgetSlot.Children.LastOrDefault()?.View!.LoadFocus();
                    }
                } else {
                    HideDescendantWidget( View.WidgetSlot, widget );
                }
            }
        }

        // ShowDescendantWidget
        protected override void ShowDescendantWidget(WidgetListSlotWrapper<UIWidgetBase> slot, UIWidgetBase widget) {
            slot.Push( widget, i => i is MainWidget or GameWidget );
        }
        protected override void HideDescendantWidget(WidgetListSlotWrapper<UIWidgetBase> slot, UIWidgetBase widget) {
            slot.Pop( widget, i => i is MainWidget or GameWidget );
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
