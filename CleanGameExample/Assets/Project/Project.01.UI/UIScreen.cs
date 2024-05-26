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
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UIScreen : UIScreenBase {

        // Document
        private UIDocument Document { get; set; } = default!;
        // UI
        private UIRouter Router { get; set; } = default!;
        // App
        private Application2 Application { get; set; } = default!;
        // Widget
        private new UIRootWidget Widget => (UIRootWidget?) base.Widget!;

        // Awake
        public override void Awake() {
            Document = gameObject.RequireComponentInChildren<UIDocument>();
            Router = Utils.Container.RequireDependency<UIRouter>( null );
            Application = Utils.Container.RequireDependency<Application2>( null );
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
            AttachWidget( new UIRootWidget() );
        }
        public override void OnDestroy() {
            Widget.DetachSelf();
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (IsMainScreen( Router.State )) {
                if (Widget.Children.FirstOrDefault() is not MainWidget) {
                    Widget.DetachChildren();
                    Widget.AttachChild( new MainWidget() );
                }
            } else if (IsGameScreen( Router.State )) {
                if (Widget.Children.FirstOrDefault() is not GameWidget) {
                    Widget.DetachChildren();
                    Widget.AttachChild( new GameWidget() );
                }
            } else {
                Widget!.DetachChildren();
            }
            Widget.Update();
        }
        public void LateUpdate() {
            Widget.LateUpdate();
        }

        // AttachWidget
        public override void AttachWidget(UIWidgetBase widget, object? argument = null) {
            base.AttachWidget( widget, argument );
            Document.Add( widget.View! );
        }
        public override void DetachWidget(UIWidgetBase widget, object? argument = null) {
            if (Document && Document.rootVisualElement != null) {
                Document.Remove( widget.View! );
            } else {
                if (!Document) Debug.LogWarning( $"You are trying to detach '{widget}' widget but UIDocument is destroyed" );
                if (Document.rootVisualElement == null) Debug.LogWarning( $"You are trying to detach '{widget}' widget but UIDocument's rootVisualElement is null" );
            }
            base.DetachWidget( widget, argument );
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
