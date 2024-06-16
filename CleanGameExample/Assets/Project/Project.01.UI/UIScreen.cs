#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using Project.Entities;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UIScreen : UIScreenBase2<UIScreenState> {

        // Container
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Game? Game => Application.Game;

        // Constructor
        public UIScreen(IDependencyContainer container) : base( container, container.RequireDependency<UIDocument>(), container.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            AddWidget( new UIRootWidget() );
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
            Router.OnStateChangeEvent += state => {
                if (IsMainScreen( state )) {
                    if (!Widget.Children.OfType<MainWidget>().Any()) {
                        Widget.RemoveChild( i => i is GameWidget );
                        Widget.AddChild( new MainWidget( container ) );
                    }
                } else if (IsGameScreen( state )) {
                    if (!Widget.Children.OfType<GameWidget>().Any()) {
                        Widget.RemoveChild( i => i is MainWidget );
                        Widget.AddChild( new GameWidget( container ) );
                    }
                } else {
                    Widget.RemoveChildren( i => i is MainWidget or GameWidget );
                }
            };
        }
        public override void Dispose() {
            Widget.RemoveSelf();
            base.Dispose();
        }

        // Update
        public void Update() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.Update();
                (child as GameWidget)?.Update();
            }
        }
        public void LateUpdate() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.LateUpdate();
                (child as GameWidget)?.LateUpdate();
            }
        }

        // OnStateChange
        protected override void OnStateChange(UIScreenState state) {
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
    public enum UIScreenState {
        None
    }
}
