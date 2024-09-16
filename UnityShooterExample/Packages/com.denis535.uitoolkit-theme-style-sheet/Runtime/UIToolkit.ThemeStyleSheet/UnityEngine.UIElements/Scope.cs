#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Scripting;

    public class ColumnScope : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ColumnScope, UxmlTraits> { }

        public ColumnScope() {
            AddToClassList( "scope" );
            AddToClassList( "column" );
        }

    }
    public class RowScope : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<RowScope, UxmlTraits> { }

        public RowScope() {
            AddToClassList( "scope" );
            AddToClassList( "row" );
        }

    }
}
