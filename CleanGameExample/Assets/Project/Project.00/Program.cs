#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.UI;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;

    public class Program : ProgramBase {

        // UI
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        // App
        private Application2 Application { get; set; } = default!;

#if UNITY_EDITOR
        // OnEnterPlaymode
        //[InitializeOnEnterPlayMode]
        //public static void OnEnterPlaymode() {
        //    var scene = SceneManager.GetActiveScene();
        //    if (scene.name == "Launcher") {
        //    } else
        //    if (scene.name == "Startup") {
        //    } else
        //    if (scene.name == "MainScene") {
        //    } else
        //    if (scene.name == "GameScene") {
        //    }
        //}
#endif

#if UNITY_EDITOR
        // OnLoad
        [InitializeOnLoadMethod]
        internal static void OnLoad() {
            if (!EditorApplication.isPlaying) {
                UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Project/Assets.Project.Scenes/Startup.unity" );
                //EditorSceneManager.playModeStartScene = null;
            }
        }
#endif

        // OnLoad
        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSplashScreen )]
        internal static void OnLoad2() {
            VisualElementFactory.StringSelector = GetDisplayString;
        }

        // Awake
        public override void Awake() {
            Theme = Utils.Container.RequireDependency<UITheme>();
            Screen = Utils.Container.RequireDependency<UIScreen>();
            Router = Utils.Container.RequireDependency<UIRouter>();
            Application = Utils.Container.RequireDependency<Application2>();
        }
        public override void OnDestroy() {
        }

        // Start
        public void Start() {
            Router.LoadMainSceneAsync().Throw();
        }
        public void Update() {
        }

        // Helpers
        private static string GetDisplayString<T>(T value) {
            if (value is Resolution resolution) return GetDisplayString( resolution );
            return value?.ToString() ?? "Null";
        }
        private static string GetDisplayString(Resolution value) {
            return $"{value.width} x {value.height}";
        }

    }
}
