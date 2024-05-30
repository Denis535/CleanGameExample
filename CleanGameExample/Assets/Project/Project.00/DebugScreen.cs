#if DEBUG
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using UnityEngine;

    [DefaultExecutionOrder( 1 )]
    public class DebugScreen : MonoBehaviour {

        // Container
        private IDependencyContainer Container { get; set; } = default!;
        // UI
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        // App
        private Application2 Application { get; set; } = default!;

        // Awake
        public void Awake() {
            Container = gameObject.RequireComponent<IDependencyContainer>();
            Theme = Container.RequireDependency<UITheme>();
            Screen = Container.RequireDependency<UIScreen>();
            Router = Container.RequireDependency<UIRouter>();
            Application = Container.RequireDependency<Application2>();
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
                }
            }
        }

    }
}
#endif
