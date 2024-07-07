#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public abstract class DialogWidgetBase : UIWidgetBase<DialogWidgetViewBase> {

        public DialogWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

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
    public class DialogWidget : DialogWidgetBase {

        public DialogWidget(string? title, string? message) {
            View = new DialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        public new DialogWidget OnSubmit(string text, Action? callback) {
            return (DialogWidget) base.OnSubmit( text, callback );
        }
        public new DialogWidget OnCancel(string text, Action? callback) {
            return (DialogWidget) base.OnCancel( text, callback );
        }

    }
    public class InfoDialogWidget : DialogWidgetBase {

        public InfoDialogWidget(string? title, string? message) {
            View = new InfoDialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        public new InfoDialogWidget OnSubmit(string text, Action? callback) {
            return (InfoDialogWidget) base.OnSubmit( text, callback );
        }
        public new InfoDialogWidget OnCancel(string text, Action? callback) {
            return (InfoDialogWidget) base.OnCancel( text, callback );
        }

    }
    public class WarningDialogWidget : DialogWidgetBase {

        public WarningDialogWidget(string? title, string? message) {
            View = new WarningDialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        public new WarningDialogWidget OnSubmit(string text, Action? callback) {
            return (WarningDialogWidget) base.OnSubmit( text, callback );
        }
        public new WarningDialogWidget OnCancel(string text, Action? callback) {
            return (WarningDialogWidget) base.OnCancel( text, callback );
        }

    }
    public class ErrorDialogWidget : DialogWidgetBase {

        public ErrorDialogWidget(string? title, string? message) {
            View = new ErrorDialogWidgetView();
            View.Title = title;
            View.Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        public new ErrorDialogWidget OnSubmit(string text, Action? callback) {
            return (ErrorDialogWidget) base.OnSubmit( text, callback );
        }
        public new ErrorDialogWidget OnCancel(string text, Action? callback) {
            return (ErrorDialogWidget) base.OnCancel( text, callback );
        }

    }
}
