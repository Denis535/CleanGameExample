#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UIScreen : UIScreenBase2 {

        // Constructor
        public UIScreen(IDependencyContainer container) : base( container, container.RequireDependency<UIDocument>(), container.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
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
        }
        public override void Dispose() {
            Widget.RemoveSelf();
            base.Dispose();
        }

        // ShowScreen
        public void ShowMainScreen() {
            Widget.RemoveChild( i => i is GameWidget );
            Widget.AddChild( new MainWidget( Container ) );
        }
        public void ShowGameScreen() {
            Widget.RemoveChild( i => i is MainWidget );
            Widget.AddChild( new GameWidget( Container ) );
        }
        public void Hide() {
            Widget.RemoveChildren( i => i is MainWidget or GameWidget );
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

    }
}
