#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class RootWidget2 : RootWidget {

        // Constructor
        public RootWidget2() {
            UIViewBase.OnVisualElementAssignedEvent += (UIViewBase view, VisualElement element) => {
                element.OnAttachToPanel( evt => {
                    if (element.focusController.focusedElement != null) return;
                    if (element.LoadFocus()) return;
                    View.__GetVisualElement__().Focus2();
                } );
                element.OnDetachFromPanel( evt => {
                    element.SaveFocus();
                } );
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
                    View.WidgetSlot.Widgets.LastOrDefault()?.__GetView__()!.__GetVisualElement__().SaveFocus();
                    View.WidgetSlot.SetEnabled( false );
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
                    if (!View.ModalWidgetSlot.Widgets.Any()) {
                        View.WidgetSlot.SetEnabled( true );
                        View.WidgetSlot.Widgets.LastOrDefault()?.__GetView__()!.__GetVisualElement__().LoadFocus();
                    }
                } else {
                    HideDescendantWidget( View.WidgetSlot, widget );
                }
            }
        }

        // ShowDescendantWidget
        protected override void ShowDescendantWidget(WidgetListSlotWrapper<UIWidgetBase> slot, UIWidgetBase widget) {
            var covered = slot.Widgets.LastOrDefault();
            if (covered != null && covered is not MainWidget and not GameWidget) {
                slot.__GetVisualElement__().Remove( covered.__GetView__()!.__GetVisualElement__() );
            }
            slot.Add( widget );
        }
        protected override void HideDescendantWidget(WidgetListSlotWrapper<UIWidgetBase> slot, UIWidgetBase widget) {
            Assert.Operation.Message( $"Widget {widget} must be last" ).Valid( widget == slot.Widgets.LastOrDefault() );
            slot.Remove( widget );
            var uncovered = slot.Widgets.LastOrDefault();
            if (uncovered != null && uncovered is not MainWidget and not GameWidget) {
                slot.__GetVisualElement__().Add( uncovered.__GetView__()!.__GetVisualElement__() );
            }
        }

    }
}
