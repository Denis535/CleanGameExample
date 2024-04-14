#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class UIScreen : UIScreenBase {

        // Deps
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        // Widget
        private new RootWidget? Widget => (RootWidget?) base.Widget;

        // Awake
        public new void Awake() {
            base.Awake();
            Router = this.GetDependencyContainer().RequireDependency<UIRouter>( null );
            Application = this.GetDependencyContainer().RequireDependency<Application2>( null );
            VisualElementFactory.OnPlayClick += evt => { };
            VisualElementFactory.OnPlaySelect += evt => { };
            VisualElementFactory.OnPlaySubmit += evt => { };
            VisualElementFactory.OnPlayCancel += evt => { };
            VisualElementFactory.OnPlayChange += evt => { };
            VisualElementFactory.OnPlayFocus += evt => { };
            VisualElementFactory.OnPlayDialog += evt => { };
            VisualElementFactory.OnPlayInfoDialog += evt => { };
            VisualElementFactory.OnPlayWarningDialog += evt => { };
            VisualElementFactory.OnPlayErrorDialog += evt => { };
            this.AttachWidget( new RootWidget() );
        }
        public new void OnDestroy() {
            Widget!.DetachSelf();
            base.OnDestroy();
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            Widget!.FixedUpdate();
        }
        public void Update() {
            if (IsMainScreen( Router.State )) {
                if (Widget!.Children.FirstOrDefault() is not MainWidget) {
                    Widget.DetachChildren();
                    Widget.AttachChild( new MainWidget() );
                }
            } else if (IsGameScreen( Router.State )) {
                if (Widget!.Children.FirstOrDefault() is not GameWidget) {
                    Widget.DetachChildren();
                    Widget.AttachChild( new GameWidget() );
                }
            } else {
                Widget!.DetachChildren();
            }
            Widget!.Update();
        }
        public void LateUpdate() {
            Widget!.LateUpdate();
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
        private static bool IsMainScreen(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return true;
            }
            return false;
        }
        private static bool IsGameScreen(UIRouterState state) {
            if (state is UIRouterState.GameSceneLoaded) {
                return true;
            }
            return false;
        }

    }
}