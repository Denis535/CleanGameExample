#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using Project.UI;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.App;
    using UnityEngine.Framework.Entities;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    [DefaultExecutionOrder( 1000 )]
    public class Program : ProgramBase2 {

        // Framework
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        private Game? Game => Application.Game;

        // OnLoad
        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSplashScreen )]
        private static void OnLoad() {
        }

#if UNITY_EDITOR
        // OnLoad
        [InitializeOnLoadMethod]
        private static void OnLoad_Editor() {
            if (!EditorApplication.isPlaying) {
                UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Project/Assets.Project.00/Scenes/Startup.unity" );
                //EditorSceneManager.playModeStartScene = null;
            }
        }
#endif

        // Awake
        protected override void Awake() {
            base.Awake();
            VisualElementFactory.StringSelector = GetDisplayString;
            Application = new Application2( this );
            Router = new UIRouter( this );
            Screen = new UIScreen( this );
            Theme = new UITheme( this );
        }
        protected override void OnDestroy() {
            Theme.Dispose();
            Screen.Dispose();
            Router.Dispose();
            Application.Dispose();
            base.OnDestroy();
        }

        // Start
        protected override void Start() {
            Router.LoadMainScene();
        }
        protected override void FixedUpdate() {
            Game?.FixedUpdate();
        }
        protected override void Update() {
            Theme.Update();
            Screen.Update();
            Game?.Update();
        }
        protected override void LateUpdate() {
            Theme.LateUpdate();
            Screen.LateUpdate();
            Game?.LateUpdate();
        }

        // OnQuit
        protected override bool OnQuit() {
            if (Router.IsMainSceneLoaded || Router.IsGameSceneLoaded) {
                Router.Quit();
                return false;
            }
            return true;
        }

#if UNITY_EDITOR
        protected override void OnInspectorGUI() {
            OnInspectorGUI( Theme );
            OnInspectorGUI( Screen );
            OnInspectorGUI( Router );
            OnInspectorGUI( Application );
            OnInspectorGUI( Game );
        }
#endif

        // IDependencyContainer
        protected override Option<object?> GetValue(Type type, object? argument) {
            this.ThrowIfInvalid();
            // UI
            if (type.IsAssignableTo( typeof( UIThemeBase ) )) {
                return new Option<object?>( Theme ?? throw Exceptions.Internal.NullReference( $"Reference 'Theme' is null" ) );
            }
            if (type.IsAssignableTo( typeof( UIScreenBase ) )) {
                return new Option<object?>( Screen ?? throw Exceptions.Internal.NullReference( $"Reference 'Screen' is null" ) );
            }
            if (type.IsAssignableTo( typeof( UIRouterBase ) )) {
                return new Option<object?>( Router ?? throw Exceptions.Internal.NullReference( $"Reference 'Router' is null" ) );
            }
            // App
            if (type.IsAssignableTo( typeof( ApplicationBase ) )) {
                return new Option<object?>( Application ?? throw Exceptions.Internal.NullReference( $"Reference 'Application' is null" ) );
            }
            // Entities
            if (type.IsAssignableTo( typeof( GameBase ) )) {
                return new Option<object?>( Game );
            }
            // Misc
            if (type == typeof( AudioSource ) && (string?) argument == "MusicAudioSource") {
                var result = transform.Find( "MusicAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type == typeof( AudioSource ) && (string?) argument == "SfxAudioSource") {
                var result = transform.Find( "SfxAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type == typeof( UIDocument )) {
                var result = gameObject.GetComponentInChildren<UIDocument>();
                if (result is not null) {
                    return new Option<object?>( result );
                }
                return default;
            }
            // Misc
            if (type.IsAssignableTo( typeof( UnityEngine.Object ) )) {
                var result = FindAnyObjectByType( type, FindObjectsInactive.Exclude );
                if (result is not null) {
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type.IsArray && type.GetElementType().IsAssignableTo( typeof( UnityEngine.Object ) )) {
                var result = FindObjectsByType( type.GetElementType(), FindObjectsInactive.Exclude, FindObjectsSortMode.None ).NullIfEmpty();
                if (result is not null) {
                    var result2 = Array.CreateInstance( type.GetElementType(), result.Length );
                    result.CopyTo( result2, 0 );
                    return new Option<object?>( result );
                }
                return default;
            }
            return default;
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
