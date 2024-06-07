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

    public class UIScreen : UIScreenBase2<UIRootWidget> {

        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        // Entities
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
            Router.OnStateChangeEvent += (state, prev) => {
                if (IsMainScreen( state )) {
                    if (Widget.Children.FirstOrDefault() is not MainWidget) {
                        Widget.RemoveChildren();
                        Widget.AddChild( new MainWidget( container ) );
                    }
                } else if (IsGameScreen( state )) {
                    if (Widget.Children.FirstOrDefault() is not GameWidget) {
                        Widget.RemoveChildren();
                        Widget.AddChild( new GameWidget( container ) );
                    }
                } else {
                    Widget.RemoveChildren();
                }
            };
        }
        public override void Dispose() {
            Widget.RemoveSelf();
            base.Dispose();
        }

        // Update
        public override void Update() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.Update();
                (child as GameWidget)?.Update();
            }
        }
        public override void LateUpdate() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.LateUpdate();
                (child as GameWidget)?.LateUpdate();
            }
        }

    }
}
