﻿#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public class LoadingMainWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label loading;

        // Layer
        public override int Layer => 0;

        // Constructor
        public LoadingMainWidgetView() {
            VisualElement = VisualElementFactory_Main.Loading( out widget, out loading );
            loading.OnAttachToPanel( PlayAnimation );
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
                var i01 = (float) i / (text.Length - 1);
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
