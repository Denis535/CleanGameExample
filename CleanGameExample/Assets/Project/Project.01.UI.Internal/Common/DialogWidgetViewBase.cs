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

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; protected init; } = default!;
        public Card Card { get; protected init; } = default!;
        public Header Header { get; protected init; } = default!;
        public Content Content { get; protected init; } = default!;
        public Footer Footer { get; protected init; } = default!;
        public Label Title { get; protected init; } = default!;
        public Label Message { get; protected init; } = default!;

        public DialogWidgetViewBase() {
        }
        public override void Dispose() {
            base.Dispose();
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
                    Header = VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
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
                    Header = VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
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
                    Header = VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
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
                    Header = VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) )
                )
            );
            Widget.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
