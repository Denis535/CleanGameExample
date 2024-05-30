#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using Project.UI;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class Program : ProgramBase, IDependencyContainer {

        // UI
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        // App
        private Application2 Application { get; set; } = default!;
        // Entities
        private Game? Game => Application.Game;

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
                UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Project/Assets.Project.00/Scenes/Startup.unity" );
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
            Application = new Application2( this );
            Router = new UIRouter( this );
            Screen = new UIScreen( this );
            Theme = new UITheme( this );
        }
        public override void OnDestroy() {
        }

        // Start
        public void Start() {
            Router.LoadMainSceneAsync().Throw();
        }
        public void Update() {
            Theme.Update();
            Screen.Update();
            Game?.Update();
        }
        public void LateUpdate() {
            Theme.LateUpdate();
            Screen.LateUpdate();
            Game?.LateUpdate();
        }

        // GetObject
        Option<object?> IDependencyContainer.GetValue(Type type, object? argument) {
            this.Validate();
            // UI
            if (type == typeof( UITheme )) {
                return new Option<object?>( Theme ?? throw Exceptions.Internal.NullReference( $"Reference 'Theme' is null" ) );
            }
            if (type == typeof( UIScreen )) {
                return new Option<object?>( Screen ?? throw Exceptions.Internal.NullReference( $"Reference 'Screen' is null" ) );
            }
            if (type == typeof( UIRouter )) {
                return new Option<object?>( Router ?? throw Exceptions.Internal.NullReference( $"Reference 'Router' is null" ) );
            }
            // App
            if (type == typeof( Application2 )) {
                return new Option<object?>( Application ?? throw Exceptions.Internal.NullReference( $"Reference 'Application' is null" ) );
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
            if (type.IsDescendentOf( typeof( UnityEngine.Object ) )) {
                var result = FindAnyObjectByType( type, FindObjectsInactive.Exclude );
                if (result is not null) {
                    return new Option<object?>( result );
                }
                return default;
            }
            if (type.IsArray && type.GetElementType().IsDescendentOf( typeof( UnityEngine.Object ) )) {
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
