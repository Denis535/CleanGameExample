#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public abstract class DialogWidgetViewBase : UIViewBase {

        protected Widget widget = default!;
        protected Card card = default!;
        protected Header header = default!;
        protected Content content = default!;
        protected Footer footer = default!;
        protected Label title = default!;
        protected Label message = default!;

        // Props
        public string? Title {
            get => title.text;
            set {
                title.text = value;
                header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => message.text;
            set {
                message.text = value;
                content.SetDisplayed( value != null );
            }
        }

        // Constructor
        public DialogWidgetViewBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public void OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                }
            } );
            footer.Add( button );
            footer.SetDisplayed( true );
        }
        public void OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                }
            } );
            footer.Add( button );
            footer.SetDisplayed( true );
        }

        // Helpers
        protected static void PlayAnimation(AttachToPanelEvent evt) {
            var target = (VisualElement) evt.target;
            var animation = ValueAnimation<float>.Create( target, Mathf.LerpUnclamped );
            animation.valueUpdated = (view, t) => {
                var tx = Easing.OutBack( Easing.InPower( t, 2 ), 4 );
                var ty = Easing.OutBack( Easing.OutPower( t, 2 ), 4 );
                var x = Mathf.LerpUnclamped( 0.8f, 1f, tx );
                var y = Mathf.LerpUnclamped( 0.8f, 1f, ty );
                view.transform.scale = new Vector3( x, y, 1 );
            };
            animation.from = 0;
            animation.to = 1;
            animation.durationMs = 500;
            animation.Start();
        }

    }
    // Dialog
    public class DialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public DialogWidgetView() {
            VisualElement = CreateVisualElement( out widget, out card, out header, out content, out footer, out title, out message );
            widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static VisualElement CreateVisualElement(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            return VisualElementFactory_Common.Dialog( out widget, out card, out header, out content, out footer, out title, out message );
        }

    }
    // InfoDialog
    public class InfoDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public InfoDialogWidgetView() {
            VisualElement = CreateVisualElement( out widget, out card, out header, out content, out footer, out title, out message );
            widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static VisualElement CreateVisualElement(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            return VisualElementFactory_Common.InfoDialog( out widget, out card, out header, out content, out footer, out title, out message );
        }

    }
    // WarningDialog
    public class WarningDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public WarningDialogWidgetView() {
            VisualElement = CreateVisualElement( out widget, out card, out header, out content, out footer, out title, out message );
            widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static VisualElement CreateVisualElement(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            return VisualElementFactory_Common.WarningDialog( out widget, out card, out header, out content, out footer, out title, out message );
        }

    }
    // ErrorDialog
    public class ErrorDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public ErrorDialogWidgetView() {
            VisualElement = CreateVisualElement( out widget, out card, out header, out content, out footer, out title, out message );
            widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static VisualElement CreateVisualElement(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            return VisualElementFactory_Common.ErrorDialog( out widget, out card, out header, out content, out footer, out title, out message );
        }

    }
}
