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

        // View
        public override UIRootWidgetViewBase View => base.View;

        // Constructor
        public UIRootWidget() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // CreateView
        protected override UIRootWidgetViewBase CreateView() {
            var view = new UIRootWidgetView();
            view.OnSubmit( evt => {
                var button = evt.target as Button;
                if (button != null) {
                    Click( button );
                    evt.StopPropagation();
                }
            } );
            view.OnCancel( evt => {
                var widget = ((VisualElement) evt.target).GetAncestorsAndSelf().Where( i => i.IsAttached() && i.enabledInHierarchy && i.IsDisplayedInHierarchy() ).FirstOrDefault( IsWidget );
                var button = widget?.Query<Button>().Where( i => i.IsAttached() && i.enabledInHierarchy && i.IsDisplayedInHierarchy() ).Where( IsCancel ).First();
                if (button != null) {
                    Click( button );
                    evt.StopPropagation();
                }
            } );
            return view;
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
        }

        // ShowView
        public override void ShowView(UIViewBase view) {
            if (!view.IsModal()) {
                View.AddView( view, i => i is MainWidgetView or GameWidgetView );
            } else {
                View.AddModalView( view, i => i is MainWidgetView or GameWidgetView );
            }
        }
        public override void HideView(UIViewBase view) {
            if (!view.IsModal()) {
                View.RemoveView( view, i => i is MainWidgetView or GameWidgetView );
            } else {
                View.RemoveModalView( view, i => i is MainWidgetView or GameWidgetView );
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
