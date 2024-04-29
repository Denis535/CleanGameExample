#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        // Root
        public ElementWrapper Root { get; }

        // Constructor
        public MainWidgetView() {
            VisualElement = MainViewFactory.MainWidget( out var root );
            Root = root.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public static class VisualElementWrapperExtensions {

        public static void SetBackgroundEffect(this ElementWrapper element, Color color, Vector2 translate, float rotate, float scale) {
            element.__GetVisualElement__().style.unityBackgroundImageTintColor = color;
            element.__GetVisualElement__().style.translate = new Translate( translate.x, translate.y );
            element.__GetVisualElement__().style.rotate = new Rotate( Angle.Degrees( rotate ) );
            element.__GetVisualElement__().style.scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

    }
}
