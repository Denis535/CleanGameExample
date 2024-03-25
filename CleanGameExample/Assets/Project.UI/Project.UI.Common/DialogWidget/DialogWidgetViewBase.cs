#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class DialogWidgetViewBase : UIViewBase, IModalWidgetView {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Widget { get; }
        public ElementWrapper Card { get; }
        public SlotWrapper Header { get; }
        public SlotWrapper Content { get; }
        public SlotWrapper Footer { get; }
        public LabelWrapper Title { get; }
        public LabelWrapper Message { get; }

        // Constructor
        public DialogWidgetViewBase(UIFactory factory) {
            if (this is DialogWidgetView) {
                VisualElement = factory.DialogWidget( out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.AsSlot();
                Content = content.AsSlot();
                Footer = footer.AsSlot();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is InfoDialogWidgetView) {
                VisualElement = factory.InfoDialogWidget( out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.AsSlot();
                Content = content.AsSlot();
                Footer = footer.AsSlot();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is WarningDialogWidgetView) {
                VisualElement = factory.WarningDialogWidget( out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.AsSlot();
                Content = content.AsSlot();
                Footer = footer.AsSlot();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is ErrorDialogWidgetView) {
                VisualElement = factory.ErrorDialogWidget( out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.AsSlot();
                Content = content.AsSlot();
                Footer = footer.AsSlot();
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
            Footer.Add( button );
        }
        public void OnCancel(UIFactory factory, string text, Action? callback) {
            var button = factory.Cancel( text );
            button.OnClick( evt => {
                if (button.IsValid()) {
                    callback?.Invoke();
                }
            } );
            Footer.Add( button );
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
