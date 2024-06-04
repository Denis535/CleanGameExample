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
            AttachWidget( new UIRootWidget() );
            Router.OnStateChangeEvent += (state, prev) => {
                if (IsMainScreen( state )) {
                    if (Widget.Children.FirstOrDefault() is not MainWidget) {
                        Widget.DetachChildren();
                        Widget.AttachChild( new MainWidget( container ) );
                    }
                } else if (IsGameScreen( state )) {
                    if (Widget.Children.FirstOrDefault() is not GameWidget) {
                        Widget.DetachChildren();
                        Widget.AttachChild( new GameWidget( container ) );
                    }
                } else {
                    Widget!.DetachChildren();
                }
            };
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
        }
        public override void Dispose() {
            Widget.DetachSelf();
            base.Dispose();
        }

        // Update
        public override void Update() {
            Widget.Update();
        }
        public override void LateUpdate() {
            Widget.LateUpdate();
        }

    }
}
