#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UIRootWidgetView : UnityEngine.Framework.UI.UIRootWidgetView {

        // Views
        public override IEnumerable<UIViewBase> Views => base.Views;
        public override IEnumerable<UIViewBase> ModalViews => base.ModalViews;

        // Constructor
        public UIRootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // CreateVisualElement
        protected override VisualElement CreateVisualElement(out VisualElement widget, out VisualElement views, out VisualElement modalViews) {
            return base.CreateVisualElement( out widget, out views, out modalViews );
        }

        // AddView
        public override void AddView(UIViewBase view, Func<UIViewBase, bool> isAlwaysVisible) {
            views.Add( view );
            Recalculate( Views.Concat( ModalViews ).ToArray(), isAlwaysVisible );
        }
        public override void RemoveView(UIViewBase view, Func<UIViewBase, bool> isAlwaysVisible) {
            views.Remove( view );
            Recalculate( Views.Concat( ModalViews ).ToArray(), isAlwaysVisible );
        }

        // AddModalView
        public override void AddModalView(UIViewBase view, Func<UIViewBase, bool> isAlwaysVisible) {
            modalViews.Add( view );
            Recalculate( Views.Concat( ModalViews ).ToArray(), isAlwaysVisible );
        }
        public override void RemoveModalView(UIViewBase view, Func<UIViewBase, bool> isAlwaysVisible) {
            modalViews.Remove( view );
            Recalculate( Views.Concat( ModalViews ).ToArray(), isAlwaysVisible );
        }

        // Helpers
        private static void Recalculate(UIViewBase[] views, Func<UIViewBase, bool> isAlwaysVisible) {
            for (var i = 0; i < views.Length; i++) {
                var view = views[ i ];
                var next = views.ElementAtOrDefault( i + 1 );
                Recalculate( view, next, isAlwaysVisible );
            }
        }
        private static void Recalculate(UIViewBase view, UIViewBase? next, Func<UIViewBase, bool> isAlwaysVisible) {
            if (next != null) {
                if (!isAlwaysVisible( view )) {
                    if (!next.IsModal()) {
                        if (view.IsDisplayedSelf()) view.SaveFocus();
                        view.SetDisplayed( false );
                    } else {
                        if (view.IsEnabledSelf()) view.SaveFocus();
                        view.SetEnabled( false );
                    }
                }
            } else {
                if (!isAlwaysVisible( view )) {
                    view.SetDisplayed( true );
                    view.SetEnabled( true );
                    if (!view.LoadFocus()) view.Focus();
                }
            }
        }

    }
}
