#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;

    public class Program : ProgramBase {

        // Globals
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;

#if UNITY_EDITOR
        // OnEnterPlaymode
        //[InitializeOnEnterPlayMode]
        //public static void OnEnterPlaymode() {
        //    var scene = SceneManager.GetActiveScene();
        //    if (scene.name == "Launcher") {
        //    } else
        //    if (scene.name == "Program") {
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
                //UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Project/Assets.Project/Launcher.unity" );
                //EditorSceneManager.playModeStartScene = null;
            }
        }
#endif

        // OnLoad
        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSplashScreen )]
        internal static void OnLoad2() {
            UnityEngine.Application.logMessageReceived += OnLog;
            UIFactory.StringSelector = GetDisplayString;
        }

        // Awake
        public new void Awake() {
            base.Awake();
            Theme = this.GetDependencyContainer().RequireDependency<UITheme>( null );
            Screen = this.GetDependencyContainer().RequireDependency<UIScreen>( null );
            Router = this.GetDependencyContainer().RequireDependency<UIRouter>( null );
            Application = this.GetDependencyContainer().RequireDependency<Application2>( null );
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public async void Start() {
            await Router.LoadMainSceneAsync();
        }
        public void Update() {
        }

        // OnLog
        private static void OnLog(string message, string stackTrace, LogType type) {
#if RELEASE
            if (type is LogType.Error or LogType.Assert or LogType.Exception) {
                UnityEngine.Application.Quit( 1 );
            }
#endif
        }

        // Helpers/GetDisplayString
        private static string GetDisplayString<T>(T value) {
            if (value is Resolution resolution) return GetDisplayString( resolution );
            return value?.ToString() ?? "Null";
        }
        // Helpers/GetDisplayString
        private static string GetDisplayString(Resolution value) {
            return $"{value.width} x {value.height}";
        }

    }
}
