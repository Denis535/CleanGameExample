#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public class LoadingWidgetView : WidgetView {

        public VisualElement Background { get; }
        public Label Loading { get; }

        public LoadingWidgetView() : base( "loading-widget-view" ) {
            this.Add(
                Background = VisualElementFactory.VisualElement().Classes( "loading-widget-view-background", "width-100pc", "height-100pc" ),
                Loading = VisualElementFactory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold", "position-absolute", "left-50pc", "bottom-2pc", "translate-x-n50pc" )
            );
            Background.RegisterCallbackOnce<AttachToPanelEvent>( async evt => {
                await Awaitable.NextFrameAsync( DisposeCancellationToken );
                Background.style.unityBackgroundImageTintColor = Color.black;
                Background.style.translate = new Translate( 0, 0 );
                Background.style.rotate = new Rotate( Angle.Degrees( 45 ) );
                Background.style.scale = new Scale( new Vector3( 5, 5, 1 ) );
            } );
            Loading.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static void PlayAnimation(AttachToPanelEvent @event) {
            var label = (Label) @event.target;
            var animation = ValueAnimation<float>.Create( label, Mathf.Lerp );
            animation.easingCurve = Easing.Linear;
            animation.valueUpdated = (label, t) => ((Label) label).text = GetLoadingText( t );
            animation.from = 0;
            animation.to = 60;
            animation.durationMs = 60 * 1000; // 60 seconds
            animation.Start();
        }
        private static string GetLoadingText(float t) {
            var builder = new StringBuilder();
            var text = "Loading...";
            for (var i = 0; i < text.Length; i++) {
                var i01 = Mathf.InverseLerp( 0, text.Length - 1, i );
                var a = Mathf.LerpUnclamped( 0f, 0.75f, i01 - t * 1.5f );
                a = Mathf.PingPong( a, 1 );
                a = Mathf.Pow( a, 3 );
                a = Mathf.LerpUnclamped( 0.05f, 1f, a );
                var color = ColorUtility.ToHtmlStringRGBA( new Color( 0.9f, 0.9f, 0.9f, a ) );
                var @char = text[ i ];
                builder.Append( $"<color=#{color}>{@char}</color>" );
            }
            return builder.ToString();
        }

    }
}
