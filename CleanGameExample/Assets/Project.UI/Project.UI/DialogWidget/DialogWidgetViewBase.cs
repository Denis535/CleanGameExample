#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class DialogWidgetViewBase : UIViewBase, IModalWidgetView {

        // View
        public ElementWrapper Widget { get; }
        public ElementWrapper Card { get; }
        public ElementWrapper Header { get; }
        public ElementWrapper Content { get; }
        public ElementWrapper Footer { get; }
        public LabelWrapper Title { get; }
        public LabelWrapper Message { get; }

        // Constructor
        public DialogWidgetViewBase(UIFactory factory) {
            if (this is DialogWidgetView) {
                VisualElement = factory.DialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is InfoDialogWidgetView) {
                VisualElement = factory.InfoDialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is WarningDialogWidgetView) {
                VisualElement = factory.WarningDialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is ErrorDialogWidgetView) {
                VisualElement = factory.ErrorDialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else {
                throw Exceptions.Internal.NotImplemented( $"DialogWidgetViewBase {this} is not implemented" );
            }
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public void OnSubmit(UIFactory factory, string text, Action? callback) {
            var button = factory.Submit( text );
            button.OnClick( evt => {
                if (button.IsValid()) {
                    callback?.Invoke();
                }
            } );
            Footer.__GetVisualElement__().Add( button );
        }
        public void OnCancel(UIFactory factory, string text, Action? callback) {
            var button = factory.Cancel( text );
            button.OnClick( evt => {
                if (button.IsValid()) {
                    callback?.Invoke();
                }
            } );
            Footer.__GetVisualElement__().Add( button );
        }

    }
    // Dialog
    public class DialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public DialogWidgetView(UIFactory factory) : base( factory ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // InfoDialog
    public class InfoDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public InfoDialogWidgetView(UIFactory factory) : base( factory ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // WarningDialog
    public class WarningDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public WarningDialogWidgetView(UIFactory factory) : base( factory ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // ErrorDialog
    public class ErrorDialogWidgetView : DialogWidgetViewBase {

        // Constructor
        public ErrorDialogWidgetView(UIFactory factory) : base( factory ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
