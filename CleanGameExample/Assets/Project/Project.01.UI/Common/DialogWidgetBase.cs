#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public abstract class DialogWidgetBase<TView> : UIWidgetBase<TView> where TView : DialogWidgetViewBase {

        // View
        public override TView View { get; }

        // Constructor
        public DialogWidgetBase(TView view) {
            View = view;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        public override void OnActivate(object? argument) {
            ShowSelf();
        }
        public override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // OnSubmit
        public DialogWidgetBase<TView> OnSubmit(string text, Action? callback) {
            View.OnSubmit( text, () => {
                callback?.Invoke();
                if (State is UIWidgetState.Active) RemoveSelf();
            } );
            return this;
        }
        public DialogWidgetBase<TView> OnCancel(string text, Action? callback) {
            View.OnCancel( text, () => {
                callback?.Invoke();
                if (State is UIWidgetState.Active) RemoveSelf();
            } );
            return this;
        }

        //// OnSubmit
        //public DialogWidgetBase<TView> OnSubmit(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    var tcs = new TaskCompletionSource<object?>();
        //    View.OnSubmit( text, () => {
        //        callback?.Invoke();
        //        tcs.TrySetResult( null );
        //        this.DetachSelf();
        //    } );
        //    {
        //        var cancellationTokenRegistration = default( CancellationTokenRegistration );
        //        cancellationTokenRegistration = cancellationToken.Register( () => {
        //            tcs.TrySetCanceled( cancellationToken );
        //            cancellationTokenRegistration.Dispose();
        //        } );
        //    }
        //    task = tcs.Task;
        //    return this;
        //}
        //public DialogWidgetBase<TView> OnCancel(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    var tcs = new TaskCompletionSource<object?>();
        //    View.OnCancel( text, () => {
        //        callback?.Invoke();
        //        tcs.TrySetResult( null );
        //        this.DetachSelf();
        //    } );
        //    {
        //        var cancellationTokenRegistration = default( CancellationTokenRegistration );
        //        cancellationTokenRegistration = cancellationToken.Register( () => {
        //            tcs.TrySetCanceled( cancellationToken );
        //            cancellationTokenRegistration.Dispose();
        //        } );
        //    }
        //    task = tcs.Task;
        //    return this;
        //}

    }
    // Dialog
    public class DialogWidget : DialogWidgetBase<DialogWidgetView> {

        // Constructor
        public DialogWidget(string? title, string? message) : base( new DialogWidgetView() ) {
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

        //// OnSubmit
        //public new DialogWidget OnSubmit(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (DialogWidget) base.OnSubmit( text, callback, cancellationToken, out task );
        //}
        //public new DialogWidget OnCancel(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (DialogWidget) base.OnCancel( text, callback, cancellationToken, out task );
        //}

    }
    // InfoDialog
    public class InfoDialogWidget : DialogWidgetBase<InfoDialogWidgetView> {

        // Constructor
        public InfoDialogWidget(string? title, string? message) : base( new InfoDialogWidgetView() ) {
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

        //// OnSubmit
        //public new InfoDialogWidget OnSubmit(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (InfoDialogWidget) base.OnSubmit( text, callback, cancellationToken, out task );
        //}
        //public new InfoDialogWidget OnCancel(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (InfoDialogWidget) base.OnCancel( text, callback, cancellationToken, out task );
        //}

    }
    // WarningDialog
    public class WarningDialogWidget : DialogWidgetBase<WarningDialogWidgetView> {

        // Constructor
        public WarningDialogWidget(string? title, string? message) : base( new WarningDialogWidgetView() ) {
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

        //// OnSubmit
        //public new WarningDialogWidget OnSubmit(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (WarningDialogWidget) base.OnSubmit( text, callback, cancellationToken, out task );
        //}
        //public new WarningDialogWidget OnCancel(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (WarningDialogWidget) base.OnCancel( text, callback, cancellationToken, out task );
        //}

    }
    // ErrorDialog
    public class ErrorDialogWidget : DialogWidgetBase<ErrorDialogWidgetView> {

        // Constructor
        public ErrorDialogWidget(string? title, string? message) : base( new ErrorDialogWidgetView() ) {
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

        //// OnSubmit
        //public new ErrorDialogWidget OnSubmit(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (ErrorDialogWidget) base.OnSubmit( text, callback, cancellationToken, out task );
        //}
        //public new ErrorDialogWidget OnCancel(string text, Action? callback, CancellationToken cancellationToken, out Task task) {
        //    return (ErrorDialogWidget) base.OnCancel( text, callback, cancellationToken, out task );
        //}

    }
}
