#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI.Common;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class UIScreen : UIScreenBase {

        // Globals
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        // Widget
        private new RootWidget2? Widget => (RootWidget2?) base.Widget;
        // State
        public UIScreenState State => GetState( Router.State );
        private ValueTracker2<UIScreenState, UIScreen> StateTracker { get; } = new ValueTracker2<UIScreenState, UIScreen>( i => i.State );
        public bool IsMainScreen => State == UIScreenState.MainScreen;
        public bool IsGameScreen => State == UIScreenState.GameScreen;

        // Awake
        public new void Awake() {
            base.Awake();
            Router = this.GetDependencyContainer().Resolve<UIRouter>( null );
            Application = this.GetDependencyContainer().Resolve<Application2>( null );
            this.AttachWidget( new RootWidget2() );
        }
        public new void OnDestroy() {
            Widget?.DetachSelf();
            base.OnDestroy();
        }

        // Start
        public void Start() {
        }
        public void Update() {
#if UNITY_EDITOR
            //AddVisualElementIfNeeded( Document, Widget!.GetVisualElement()! );
#endif
            if (StateTracker.IsChanged( this )) {
                Widget!.DetachChildren();
                if (IsMainScreen) {
                    Widget!.AttachChild( new MainWidget() );
                } else if (IsGameScreen) {
                    Widget!.AttachChild( new GameWidget() );
                }
            }
            Widget!.Update();
        }

        // AttachWidget
        protected override void __AttachWidget__(UIWidgetBase widget, object? argument) {
            base.__AttachWidget__( widget, argument );
            AddVisualElement( Document, widget.__GetView__()!.__GetVisualElement__()! );
        }
        protected override void __DetachWidget__(UIWidgetBase widget, object? argument) {
            if (Document && Document.rootVisualElement != null) {
                RemoveVisualElement( Document, widget.__GetView__()!.__GetVisualElement__()! );
            } else {
                if (!Document) Debug.LogWarning( $"You are trying to detach '{widget}' widget but UIDocument is destroyed" );
                if (Document.rootVisualElement == null) Debug.LogWarning( $"You are trying to detach '{widget}' widget but UIDocument's rootVisualElement is null" );
            }
            base.__DetachWidget__( widget, argument );
        }

        // Helpers
        private static UIScreenState GetState(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return UIScreenState.MainScreen;
            }
            if (state is UIRouterState.GameSceneLoaded) {
                return UIScreenState.GameScreen;
            }
            return UIScreenState.None;
        }

    }
    // UIScreenState
    public enum UIScreenState {
        None,
        MainScreen,
        GameScreen,
    }
}
