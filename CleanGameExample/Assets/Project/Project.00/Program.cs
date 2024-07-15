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

    [DefaultExecutionOrder( 50 )]
    public class Program : ProgramBase2<UITheme, UIScreen, UIRouter, Application2, Game> {

        protected override UITheme Theme { get; set; } = default!;
        protected override UIScreen Screen { get; set; } = default!;
        protected override UIRouter Router { get; set; } = default!;
        protected override Application2 Application { get; set; } = default!;
        protected override Game? Game => Application.Game;

        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSplashScreen )]
        private static void OnLoad() {
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnLoad_Editor() {
            if (!EditorApplication.isPlaying) {
                UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Project/Assets.Project.00/Scenes/Startup.unity" );
                //EditorSceneManager.playModeStartScene = null;
            }
        }
#endif

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

        protected override void Start() {
            Router.LoadMainScene();
        }
        protected override void FixedUpdate() {
            Application?.OnFixedUpdate();
            Screen.OnFixedUpdate();
            Theme.OnFixedUpdate();
        }
        protected override void Update() {
            Application?.OnUpdate();
            Screen.OnUpdate();
            Theme.OnUpdate();
        }
        protected override void LateUpdate() {
            Application?.OnLateUpdate();
        }

        protected override bool OnQuit() {
            if (Router.IsMainSceneLoaded || Router.IsGameSceneLoaded) {
                Router.Quit();
                return false;
            }
            return true;
        }

        protected override Option<object?> GetValue(Type type, object? argument) {
            this.ThrowIfInvalid();
            // UI
            if (type.IsAssignableTo( typeof( UIThemeBase ) )) {
                if (Theme != null) return new Option<object?>( Theme );
                return default;
            }
            if (type.IsAssignableTo( typeof( UIScreenBase ) )) {
                if (Screen != null) return new Option<object?>( Screen );
                return default;
            }
            if (type.IsAssignableTo( typeof( UIRouterBase ) )) {
                if (Router != null) return new Option<object?>( Router );
                return default;
            }
            // App
            if (type.IsAssignableTo( typeof( ApplicationBase ) )) {
                if (Application != null) return new Option<object?>( Application );
                return default;
            }
            // Entities
            if (type.IsAssignableTo( typeof( GameBase ) )) {
                if (Game != null) return new Option<object?>( Game );
                return default;
            }
            // Misc
            if (type == typeof( AudioSource ) && (string?) argument == "MusicAudioSource") {
                var result = transform.Find( "MusicAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type == typeof( AudioSource ) && (string?) argument == "SfxAudioSource") {
                var result = transform.Find( "SfxAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type == typeof( UIDocument )) {
                var result = gameObject.GetComponentInChildren<UIDocument>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return new Option<object?>( result );
                }
                return default;
            }
            // Misc
            if (type.IsAssignableTo( typeof( UnityEngine.Object ) )) {
                var result = FindAnyObjectByType( type, FindObjectsInactive.Exclude );
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type.IsArray && type.GetElementType().IsAssignableTo( typeof( UnityEngine.Object ) )) {
                var result = FindObjectsByType( type.GetElementType(), FindObjectsInactive.Exclude, FindObjectsSortMode.None ).NullIfEmpty();
                if (result is not null) {
                    result.ForEach( i => i.ThrowIfInvalid() );
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
