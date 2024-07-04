#if DEBUG
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using Project.UI;
    using UnityEngine;

    [DefaultExecutionOrder( 1001 )]
    public class DebugScreen : MonoBehaviour {

        // System
        private IDependencyContainer Contairner { get; set; } = default!;
        // Framework
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        private Game? Game => Application.Game;

        // Awake
        public void Awake() {
            Contairner = gameObject.RequireComponent<IDependencyContainer>();
            Theme = Contairner.RequireDependency<UITheme>();
            Screen = Contairner.RequireDependency<UIScreen>();
            Router = Contairner.RequireDependency<UIRouter>();
            Application = Contairner.RequireDependency<Application2>();
        }
        public void OnDestroy() {
        }

        // OnGUI
        public void OnGUI() {
            using (new GUILayout.VerticalScope( GUI.skin.box )) {
                GUILayout.Label( "Fps: " + (1f / Time.smoothDeltaTime).ToString( "000." ) );
                GUILayout.Label( "Main Scene: " + Router.IsMainSceneLoaded );
                GUILayout.Label( "Game Scene: " + Router.IsGameSceneLoaded );
                if (Game != null) {
                    GUILayout.Label( "Game State: " + Game.State );
                    GUILayout.Label( "Game Pause: " + Game.IsPaused );
                    GUILayout.Label( "Player State: " + Game.Player.State );
                }
            }
        }

    }
}
#endif
