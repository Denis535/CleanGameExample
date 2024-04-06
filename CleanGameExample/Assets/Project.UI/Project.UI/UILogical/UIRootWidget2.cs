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

    public class UIRootWidget2 : UIRootWidget {

        // Constructor
        public UIRootWidget2() {
            UIFactory.OnVisualElementCreate += (visualElement, view) => {
                if (view != null) {
                    visualElement.OnAttachToPanel( evt => {
                        if (visualElement.focusController.focusedElement != null) return;
                        if (visualElement.LoadFocus()) return;
                        visualElement.Focus2();
                    } );
                    visualElement.OnDetachFromPanel( evt => {
                        visualElement.SaveFocus();
                    } );
                }
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

        // Update
        public void Update() {
            foreach (var child in Children) {
                (child as MainWidget)?.Update();
                (child as GameWidget)?.Update();
            }
        }

    }
}
