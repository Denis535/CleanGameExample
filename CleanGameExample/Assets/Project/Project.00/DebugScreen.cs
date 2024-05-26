#if DEBUG
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using UnityEngine;
    using UnityEngine.Framework;

    [DefaultExecutionOrder( ScriptExecutionOrders.Program )]
    public class DebugScreen : MonoBehaviour {

        // UI
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        // App
        private Application2 Application { get; set; } = default!;

        // Awake
        public void Awake() {
            Theme = Utils.Container.RequireDependency<UITheme>();
            Screen = Utils.Container.RequireDependency<UIScreen>();
            Router = Utils.Container.RequireDependency<UIRouter>();
            Application = Utils.Container.RequireDependency<Application2>();
        }
        public void OnDestroy() {
        }

        // OnGUI
        public void OnGUI() {
            using (new GUILayout.VerticalScope( GUI.skin.box )) {
                {
                    // Fps
                    GUILayout.Label( "Fps: " + (1f / Time.smoothDeltaTime).ToString( "000." ) );
                    GUILayout.Space( 2 );
                }
                {
                    // State
                    GUILayout.Label( "State: " + Router.State );
                    GUILayout.Space( 2 );
                }
                if (Application.Game != null) {
                    // Game
                    GUILayout.Label( "Is Game Running: " + true );
                    GUILayout.Label( "Is Game Paused: " + Application.Game.IsPaused );
                    GUILayout.Space( 2 );
                }
            }
        }

    }
}
#endif
