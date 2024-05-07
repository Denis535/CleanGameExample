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
}
