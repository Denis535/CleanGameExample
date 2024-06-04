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

        private readonly Widget widget;
        private readonly Card card;
        private readonly Header header;
        private readonly Content content;
        private readonly Footer footer;
        private readonly Label title;
        private readonly Label message;

        // Priority
        public override int Priority => 1000;
        // IsModal
        public override bool IsModal => true;
        // Title
        public string? Title {
            get => title.text;
            set {
                title.text = value;
                header.SetDisplayed( value != null );
            }
        }
        // Message
        public string? Message {
            get => message.text;
            set {
                message.text = value;
                content.SetDisplayed( value != null );
            }
        }

        // Constructor
        public DialogWidgetViewBase() {
            VisualElement = CreateVisualElement( this, out widget, out card, out header, out content, out footer, out this.title, out this.message );
            widget.OnAttachToPanel( PlayAppearance );
            header.SetDisplayed( false );
            content.SetDisplayed( false );
            footer.SetDisplayed( false );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public void OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.OnClick( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                }
            } );
            footer.Add( button );
            footer.SetDisplayed( true );
        }
        public void OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.OnClick( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                }
            } );
            footer.Add( button );
            footer.SetDisplayed( true );
        }

        // Helpers
        private static VisualElement CreateVisualElement(DialogWidgetViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            if (view is DialogWidgetView) {
                return VisualElementFactory_Common.DialogWidget( out widget, out card, out header, out content, out footer, out title, out message );
            }
            if (view is InfoDialogWidgetView) {
                return VisualElementFactory_Common.InfoDialogWidget( out widget, out card, out header, out content, out footer, out title, out message );
            }
            if (view is WarningDialogWidgetView) {
                return VisualElementFactory_Common.WarningDialogWidget( out widget, out card, out header, out content, out footer, out title, out message );
            }
            if (view is ErrorDialogWidgetView) {
                return VisualElementFactory_Common.ErrorDialogWidget( out widget, out card, out header, out content, out footer, out title, out message );
            }
            throw Exceptions.Internal.NotSupported( $"DialogWidgetViewBase {view} is not supported" );
        }
        // Helpers
        private static void PlayAppearance(AttachToPanelEvent evt) {
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
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // InfoDialog
    public class InfoDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public InfoDialogWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // WarningDialog
    public class WarningDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public WarningDialogWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // ErrorDialog
    public class ErrorDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public ErrorDialogWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
