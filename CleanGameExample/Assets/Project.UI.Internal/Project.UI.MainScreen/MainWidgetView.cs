#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        // View
        public ElementWrapper Widget { get; }

        // Constructor
        public MainWidgetView() {
            VisualElement = ViewFactory.MainWidget( out var widget );
            Widget = widget.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetEffect
        public void SetEffect(Color color, Vector2 position, float rotate, float scale) {
            VisualElement.style.unityBackgroundImageTintColor = color;
            VisualElement.style.translate = new Translate( position.x, position.y, 0 );
            VisualElement.style.rotate = new Rotate( Angle.Degrees( rotate ) );
            VisualElement.style.scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

    }
}
