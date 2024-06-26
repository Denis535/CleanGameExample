#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public abstract class DialogWidgetBase : UIWidgetBase<DialogWidgetViewBase> {

        // Constructor
        public DialogWidgetBase() {
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // OnSubmit
        public DialogWidgetBase OnSubmit(string text, Action? callback) {
            View.OnSubmit( text, () => {
                callback?.Invoke();
                if (State is UIWidgetState.Active) RemoveSelf();
            } );
            return this;
        }
        public DialogWidgetBase OnCancel(string text, Action? callback) {
            View.OnCancel( text, () => {
                callback?.Invoke();
                if (State is UIWidgetState.Active) RemoveSelf();
            } );
            return this;
        }

    }
    // Dialog
    public class DialogWidget : DialogWidgetBase {

        // Constructor
        public DialogWidget(string? title, string? message) {
            View = new DialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public new DialogWidget OnSubmit(string text, Action? callback) {
            return (DialogWidget) base.OnSubmit( text, callback );
        }
        public new DialogWidget OnCancel(string text, Action? callback) {
            return (DialogWidget) base.OnCancel( text, callback );
        }

    }
    // InfoDialog
    public class InfoDialogWidget : DialogWidgetBase {

        // Constructor
        public InfoDialogWidget(string? title, string? message) {
            View = new InfoDialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public new InfoDialogWidget OnSubmit(string text, Action? callback) {
            return (InfoDialogWidget) base.OnSubmit( text, callback );
        }
        public new InfoDialogWidget OnCancel(string text, Action? callback) {
            return (InfoDialogWidget) base.OnCancel( text, callback );
        }

    }
    // WarningDialog
    public class WarningDialogWidget : DialogWidgetBase {

        // Constructor
        public WarningDialogWidget(string? title, string? message) {
            View = new WarningDialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public new WarningDialogWidget OnSubmit(string text, Action? callback) {
            return (WarningDialogWidget) base.OnSubmit( text, callback );
        }
        public new WarningDialogWidget OnCancel(string text, Action? callback) {
            return (WarningDialogWidget) base.OnCancel( text, callback );
        }

    }
    // ErrorDialog
    public class ErrorDialogWidget : DialogWidgetBase {

        // Constructor
        public ErrorDialogWidget(string? title, string? message) {
            View = new ErrorDialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnSubmit
        public new ErrorDialogWidget OnSubmit(string text, Action? callback) {
            return (ErrorDialogWidget) base.OnSubmit( text, callback );
        }
        public new ErrorDialogWidget OnCancel(string text, Action? callback) {
            return (ErrorDialogWidget) base.OnCancel( text, callback );
        }

    }
}
