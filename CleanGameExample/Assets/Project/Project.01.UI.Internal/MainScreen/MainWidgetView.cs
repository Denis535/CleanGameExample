#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        private readonly Widget widget;

        // Layer
        public override int Layer => -1000;

        // Constructor
        public MainWidgetView() {
            VisualElement = VisualElementFactory_Main.Main( out widget );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetEffect
        public void SetEffect(Color color, Vector2 translate, float rotate, float scale) {
            widget.style.unityBackgroundImageTintColor = color;
            widget.style.translate = new Translate( translate.x, translate.y );
            widget.style.rotate = new Rotate( Angle.Degrees( rotate ) );
            widget.style.scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

    }
}
