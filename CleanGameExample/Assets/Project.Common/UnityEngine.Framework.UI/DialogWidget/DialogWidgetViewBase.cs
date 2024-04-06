#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

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
        public DialogWidgetViewBase() {
            if (this is DialogWidgetView) {
                VisualElement = DialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                VisualElement.OnAttachToPanel( evt => PlayAppearance( VisualElement ) );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is InfoDialogWidgetView) {
                VisualElement = InfoDialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                VisualElement.OnAttachToPanel( evt => PlayAppearance( VisualElement ) );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is WarningDialogWidgetView) {
                VisualElement = WarningDialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                VisualElement.OnAttachToPanel( evt => PlayAppearance( VisualElement ) );
                Widget = widget.Wrap();
                Card = card.Wrap();
                Header = header.Wrap();
                Content = content.Wrap();
                Footer = footer.Wrap();
                Title = title.Wrap();
                Message = message.Wrap();
            } else if (this is ErrorDialogWidgetView) {
                VisualElement = ErrorDialogWidget( this, out var widget, out var card, out var header, out var content, out var footer, out var title, out var message );
                VisualElement.OnAttachToPanel( evt => PlayAppearance( VisualElement ) );
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
        public void OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.OnClick( evt => {
                if (button.IsValid()) {
                    callback?.Invoke();
                }
            } );
            Footer.__GetVisualElement__().Add( button );
        }
        public void OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.OnClick( evt => {
                if (button.IsValid()) {
                    callback?.Invoke();
                }
            } );
            Footer.__GetVisualElement__().Add( button );
        }

        // Helpers
        private static Widget DialogWidget(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.DialogWidget( view ).AsScope( out widget )) {
                using (VisualElementFactory.DialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        VisualElementFactory.Label( null ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            VisualElementFactory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        private static Widget InfoDialogWidget(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.InfoDialogWidget( view ).AsScope( out widget )) {
                using (VisualElementFactory.InfoDialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        VisualElementFactory.Label( null ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            VisualElementFactory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        private static Widget WarningDialogWidget(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.WarningDialogWidget( view ).AsScope( out widget )) {
                using (VisualElementFactory.WarningDialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        VisualElementFactory.Label( null ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            VisualElementFactory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        private static Widget ErrorDialogWidget(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.ErrorDialogWidget( view ).AsScope( out widget )) {
                using (VisualElementFactory.ErrorDialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        VisualElementFactory.Label( null ).AddToScope( out title );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            VisualElementFactory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        // Helpers
        private static void PlayAppearance(VisualElement element) {
            var animation = ValueAnimation<float>.Create( element, Mathf.LerpUnclamped );
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
