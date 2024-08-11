#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    internal interface IDialogWidget<TThis> where TThis : UIWidgetBase {
        string? Title { get; set; }
        string? Message { get; set; }
        TThis OnSubmit(string text, Action? callback);
        TThis OnCancel(string text, Action? callback);
    }
    public class DialogWidget : UIWidgetBase<DialogWidgetView>, IDialogWidget<DialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public DialogWidget(string? title, string? message) {
            View = new DialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
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

        public DialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public DialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class InfoDialogWidget : UIWidgetBase<InfoDialogWidgetView>, IDialogWidget<InfoDialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public InfoDialogWidget(string? title, string? message) {
            View = new InfoDialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
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

        public InfoDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public InfoDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class WarningDialogWidget : UIWidgetBase<WarningDialogWidgetView>, IDialogWidget<WarningDialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public WarningDialogWidget(string? title, string? message) {
            View = new WarningDialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
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

        public WarningDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public WarningDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class ErrorDialogWidget : UIWidgetBase<ErrorDialogWidgetView>, IDialogWidget<ErrorDialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public ErrorDialogWidget(string? title, string? message) {
            View = new ErrorDialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
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

        public ErrorDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public ErrorDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (State is UIWidgetState.Active) RemoveSelf();
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
}
