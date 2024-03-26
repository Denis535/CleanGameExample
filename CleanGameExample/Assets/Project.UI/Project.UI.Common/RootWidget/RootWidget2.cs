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
                    View.ModalWidgetSlot.Add( widget );
                } else {
                    View.WidgetSlot.Add( widget );
                }
                {
                    var prev = (UIWidgetBase?) View.WidgetSlot.Widgets.Concat( View.ModalWidgetSlot.Widgets ).SkipLast( 1 ).LastOrDefault();
                    if (prev != null) prev.__GetView__()!.__GetVisualElement__().SaveFocus();
                    RecalcVisibility( View );
                    var last = (UIWidgetBase?) View.WidgetSlot.Widgets.Concat( View.ModalWidgetSlot.Widgets ).LastOrDefault();
                    if (last != null) last.__GetView__()!.__GetVisualElement__().Focus2();
                }
            }
        }
        protected override void HideDescendantWidget(UIWidgetBase widget) {
            if (widget.IsViewable) {
                if (widget.IsModal()) {
                    Assert.Operation.Message( $"Widget {widget} must be last" ).Valid( widget == View.ModalWidgetSlot.Widgets.LastOrDefault() );
                    View.ModalWidgetSlot.Remove( widget );
                } else {
                    Assert.Operation.Message( $"Widget {widget} must be last" ).Valid( widget == View.WidgetSlot.Widgets.LastOrDefault() );
                    View.WidgetSlot.Remove( widget );
                }
                {
                    RecalcVisibility( View );
                    var last = (UIWidgetBase?) View.WidgetSlot.Widgets.Concat( View.ModalWidgetSlot.Widgets ).LastOrDefault();
                    if (last != null) last.__GetView__()!.__GetVisualElement__().LoadFocus();
                }
            }
        }

        // Helpers
        protected static new void RecalcVisibility(RootWidgetViewBase view) {
            foreach (var widget in view.WidgetSlot.Widgets) {
                if (widget is not MainWidget and not GameWidget) {
                    RecalcWidgetVisibility( widget, widget == view.WidgetSlot.Widgets.Last(), view.ModalWidgetSlot.Widgets.Any() );
                }
            }
            foreach (var widget in view.ModalWidgetSlot.Widgets) {
                RecalcModalWidgetVisibility( widget, widget == view.ModalWidgetSlot.Widgets.Last() );
            }
        }

    }
}
