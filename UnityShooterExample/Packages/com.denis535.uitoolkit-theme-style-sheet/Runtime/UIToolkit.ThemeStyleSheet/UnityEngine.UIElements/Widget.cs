#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.Scripting;

    public class Widget : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Widget, UxmlTraits> { }

        public Widget() {
            AddToClassList( "widget" );
        }

    }
}
