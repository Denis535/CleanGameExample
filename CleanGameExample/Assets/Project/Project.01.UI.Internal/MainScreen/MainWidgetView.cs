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
        // Props
        public (Color Color, Vector2 Translate, float Rotate, float Scale) Effect {
            set {
                widget.style.unityBackgroundImageTintColor = value.Color;
                widget.style.translate = new Translate( value.Translate.x, value.Translate.y );
                widget.style.rotate = new Rotate( Angle.Degrees( value.Rotate ) );
                widget.style.scale = new Scale( new Vector3( value.Scale, value.Scale, 1 ) );
            }
        }

        // Constructor
        public MainWidgetView() {
            VisualElement = VisualElementFactory_Main.Main( out widget );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
