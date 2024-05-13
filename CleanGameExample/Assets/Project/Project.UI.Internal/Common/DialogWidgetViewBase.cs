#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public abstract class DialogWidgetViewBase : UIViewBase, IModalWidgetView {

        private readonly Card card;
        private readonly Header header;
        private readonly Content content;
        private readonly Footer footer;
        private readonly Label title;
        private readonly Label message;

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
            if (this is DialogWidgetView) {
                VisualElement = ViewFactory.DialogWidget( out _, out card, out header, out content, out footer, out title, out message );
                VisualElement.OnAttachToPanel( PlayAppearance );
                footer.SetDisplayed( false );
            } else if (this is InfoDialogWidgetView) {
                VisualElement = ViewFactory.InfoDialogWidget( out _, out card, out header, out content, out footer, out title, out message );
                VisualElement.OnAttachToPanel( PlayAppearance );
                footer.SetDisplayed( false );
            } else if (this is WarningDialogWidgetView) {
                VisualElement = ViewFactory.WarningDialogWidget( out _, out card, out header, out content, out footer, out title, out message );
                VisualElement.OnAttachToPanel( PlayAppearance );
                footer.SetDisplayed( false );
            } else if (this is ErrorDialogWidgetView) {
                VisualElement = ViewFactory.ErrorDialogWidget( out _, out card, out header, out content, out footer, out title, out message );
                VisualElement.OnAttachToPanel( PlayAppearance );
                footer.SetDisplayed( false );
            } else {
                throw Exceptions.Internal.NotSupported( $"DialogWidgetViewBase {this} is not supported" );
            }
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
