#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class UIScreenBase2 : UIScreenBase {

        // Container
        protected IDependencyContainer Container { get; }
        // Document
        protected UIDocument Document { get; }
        // AudioSource
        protected AudioSource AudioSource { get; }

        // Constructor
        public UIScreenBase2(IDependencyContainer container, UIDocument document, AudioSource audioSource) {
            Container = container;
            Document = document;
            AudioSource = audioSource;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void Update();
        public abstract void LateUpdate();

        // AttachWidget
        public override void AttachWidget(UIWidgetBase widget, object? argument = null) {
            base.AttachWidget( widget, argument );
            Document.Add( widget.View! );
        }
        public override void DetachWidget(UIWidgetBase widget, object? argument = null) {
            if (!Document) {
                Debug.LogWarning( $"You are trying to detach '{widget}' widget but UIDocument is destroyed" );
                return;
            }
            if (Document.rootVisualElement == null) {
                Debug.LogWarning( $"You are trying to detach '{widget}' widget but UIDocument's rootVisualElement is null" );
                return;
            }
            Document.Remove( widget.View! );
            base.DetachWidget( widget, argument );
        }

        // Helpers
        protected static bool IsMainScreen(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return true;
            }
            return false;
        }
        protected static bool IsGameScreen(UIRouterState state) {
            if (state is UIRouterState.GameSceneLoaded) {
                return true;
            }
            return false;
        }

    }
    public abstract class UIScreenBase2<TWidget> : UIScreenBase2 where TWidget : UIWidgetBase {

        // Widget
        protected new TWidget Widget => (TWidget) base.Widget;

        // Constructor
        public UIScreenBase2(IDependencyContainer container, UIDocument document, AudioSource audioSource) : base( container, document, audioSource ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
