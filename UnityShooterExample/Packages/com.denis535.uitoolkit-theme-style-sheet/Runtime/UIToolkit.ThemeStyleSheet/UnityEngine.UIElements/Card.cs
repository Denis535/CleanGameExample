#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Scripting;

    public class Card : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Card, UxmlTraits> { }

        public Header? Header => Children().OfType<Header>().FirstOrDefault();
        public Content? Content => Children().OfType<Content>().FirstOrDefault();
        public Footer? Footer => Children().OfType<Footer>().FirstOrDefault();

        public Card() {
            AddToClassList( "card" );
        }

    }
    // Header
    public class Header : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Header, UxmlTraits> { }

        public Header() {
            AddToClassList( "header" );
        }

    }
    // Content
    public class Content : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Content, UxmlTraits> { }

        public Content() {
            AddToClassList( "content" );
        }

    }
    // Footer
    public class Footer : VisualElement {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Footer, UxmlTraits> { }

        public Footer() {
            AddToClassList( "footer" );
        }

    }
}
