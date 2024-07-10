#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public abstract class DialogWidgetViewBase : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        protected Widget Widget { get; init; } = default!;
        protected Card Card { get; init; } = default!;
        protected Header Header { get; init; } = default!;
        protected Content Content { get; init; } = default!;
        protected Footer Footer { get; init; } = default!;
        protected Label Title_ { get; init; } = default!;
        protected Label Message_ { get; init; } = default!;

        public string? Title {
            get => Title_.text;
            set {
                Title_.text = value;
                Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => Message_.text;
            set {
                Message_.text = value;
                Content.SetDisplayed( value != null );
            }
        }

        public DialogWidgetViewBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        public void OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                }
            } );
            Footer.Add( button );
            Footer.SetDisplayed( true );
        }
        public void OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                }
            } );
            Footer.Add( button );
            Footer.SetDisplayed( true );
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
    public class DialogWidgetView : DialogWidgetViewBase {

        public DialogWidgetView() {
            Widget = VisualElementFactory.DialogWidget().UserData( this ).Children(
                VisualElementFactory.DialogCard().Children(
                    VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title_ = VisualElementFactory.Label( null )
                    ),
                    VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message_ = VisualElementFactory.Label( null )
                        )
                    ),
                    VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
                )
            );
            Widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class InfoDialogWidgetView : DialogWidgetViewBase {

        public InfoDialogWidgetView() {
            Widget = VisualElementFactory.InfoDialogWidget().UserData( this ).Children(
                VisualElementFactory.InfoDialogCard().Children(
                    VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title_ = VisualElementFactory.Label( null )
                    ),
                    VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message_ = VisualElementFactory.Label( null )
                        )
                    ),
                    VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
                )
            );
            Widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class WarningDialogWidgetView : DialogWidgetViewBase {

        public WarningDialogWidgetView() {
            Widget = VisualElementFactory.WarningDialogWidget().UserData( this ).Children(
                VisualElementFactory.WarningDialogCard().Children(
                    VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title_ = VisualElementFactory.Label( null )
                    ),
                    VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message_ = VisualElementFactory.Label( null )
                        )
                    ),
                    VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
                )
            );
            Widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class ErrorDialogWidgetView : DialogWidgetViewBase {

        public ErrorDialogWidgetView() {
            Widget = VisualElementFactory.ErrorDialogWidget().UserData( this ).Children(
                VisualElementFactory.ErrorDialogCard().Children(
                    VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title_ = VisualElementFactory.Label( null )
                    ),
                    VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message_ = VisualElementFactory.Label( null )
                        )
                    ),
                    VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
                )
            );
            Widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
