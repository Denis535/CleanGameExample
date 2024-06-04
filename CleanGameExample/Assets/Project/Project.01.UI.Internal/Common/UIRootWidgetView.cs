#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.Common;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class UIRootWidgetView : UIRootWidgetViewBase {

        // Constructor
        public UIRootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // AddView
        public override void AddView(UIViewBase view) {
            base.AddView( view );
            Recalculate( Children.ToArray() );
        }
        public override void RemoveView(UIViewBase view) {
            base.RemoveView( view );
            Recalculate( Children.ToArray() );
        }

        // Helpers
        private static void Recalculate(UIViewBase[] views) {
            for (var i = 0; i < views.Length; i++) {
                var view = views[ i ];
                var next = views.ElementAtOrDefault( i + 1 );
                if (next == null) {
                    Show( view );
                    Focus( view );
                } else {
                    if (view.IsEnabledSelf() && view.IsDisplayedSelf()) view.SaveFocus();
                    Hide( view, next );
                }
            }
        }
        private static void Show(UIViewBase view) {
            if (!IsAlwaysVisible( view )) {
                view.SetEnabled( true );
                view.SetDisplayed( true );
            }
        }
        private static void Hide(UIViewBase view, UIViewBase next) {
            if (!IsAlwaysVisible( view )) {
                if (!IsModal( view ) && IsModal( next )) {
                    view.SetEnabled( false );
                } else {
                    view.SetDisplayed( false );
                }
            }
        }
        // Helpers
        private static void Focus(UIViewBase view) {
            if (view.LoadFocus()) return;
            try {
                view.Focus();
            } catch (Exception ex) {
                Debug.LogError( ex );
            }
        }
        private static bool IsModal(UIViewBase view) {
            return view is DialogWidgetViewBase;
        }
        private static bool IsAlwaysVisible(UIViewBase view) {
            return view is MainWidgetView or GameWidgetView;
        }

    }
}
