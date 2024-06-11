#if DEBUG
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using UnityEngine;

    [DefaultExecutionOrder( 1001 )]
    public class DebugScreen : MonoBehaviour {

        // Container
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;

        // Awake
        public void Awake() {
            var container = gameObject.RequireComponent<IDependencyContainer>();
            Theme = container.RequireDependency<UITheme>();
            Screen = container.RequireDependency<UIScreen>();
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
        }
        public void OnDestroy() {
        }

        // OnGUI
        public void OnGUI() {
            using (new GUILayout.VerticalScope( GUI.skin.box )) {
                GUILayout.Label( "Fps: " + (1f / Time.smoothDeltaTime).ToString( "000." ) );
                GUILayout.Label( "Router State: " + Router.State );
                if (Application.Game != null) {
                    GUILayout.Label( "Game State: " + Application.Game.State );
                    GUILayout.Label( "Game Paused: " + Application.Game.IsPaused );
                    GUILayout.Label( "Player State: " + Application.Game.Player.State );
                }
            }
        }

    }
}
#endif
