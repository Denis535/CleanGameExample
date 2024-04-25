#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using Unity.Services.Authentication;
    using UnityEngine;
    using UnityEngine.Audio;

    public class DependencyContainer : MonoBehaviour, IDependencyContainer {

        [SerializeField] private UITheme uiTheme = default!;
        [SerializeField] private UIScreen uiScreen = default!;
        [SerializeField] private UIRouter uiRouter = default!;
        [SerializeField] private Application2 application = default!;
        [SerializeField] private AudioMixer audioMixer = default!;

        // Deps
        private UITheme UITheme => uiTheme;
        private UIScreen UIScreen => uiScreen;
        private UIRouter UIRouter => uiRouter;
        private Application2 Application => application;
        private AudioMixer AudioMixer => audioMixer;
        // Deps
        private Storage Storage { get; set; } = default!;
        private Storage.ProfileSettings ProfileSettings { get; set; } = default!;
        private Storage.VideoSettings VideoSettings { get; set; } = default!;
        private Storage.AudioSettings AudioSettings { get; set; } = default!;
        private Storage.Preferences Preferences { get; set; } = default!;
        private IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;

        // Awake
        public void Awake() {
            Storage = new Storage();
            ProfileSettings = new Storage.ProfileSettings();
            VideoSettings = new Storage.VideoSettings();
            AudioSettings = new Storage.AudioSettings( AudioMixer );
            Preferences = new Storage.Preferences();
            UnityUtils.Container = this;
        }
        public void OnDestroy() {
        }

        // GetObject
        Option<object?> IDependencyContainer.GetValue(Type type, object? argument) {
            // UI
            if (type == typeof( UITheme )) {
                var result = UITheme;
                return result;
            }
            if (type == typeof( UIScreen )) {
                var result = UIScreen;
                return result;
            }
            if (type == typeof( UIRouter )) {
                var result = UIRouter;
                return result;
            }
            // App
            if (type == typeof( Application2 )) {
                var result = Application;
                return result;
            }
            if (type == typeof( Storage )) {
                var result = Storage;
                return result;
            }
            if (type == typeof( Storage.ProfileSettings )) {
                var result = ProfileSettings;
                return result;
            }
            if (type == typeof( Storage.VideoSettings )) {
                var result = VideoSettings;
                return result;
            }
            if (type == typeof( Storage.AudioSettings )) {
                var result = AudioSettings;
                return result;
            }
            if (type == typeof( Storage.Preferences )) {
                var result = Preferences;
                return result;
            }
            if (type == typeof( IAuthenticationService )) {
                var result = AuthenticationService;
                return (object?) result;
            }
            // Misc
            if (type.IsDescendentOf( typeof( Component ) )) {
                var result = (Component) FindAnyObjectByType( type, FindObjectsInactive.Exclude );
                return result;
            }
            if (type.IsArray && type.GetElementType().IsDescendentOf( typeof( Component ) )) {
                var result = FindObjectsByType( type.GetElementType(), FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                var result2 = Array.CreateInstance( type.GetElementType(), result.Length );
                result.CopyTo( result2, 0 );
                return result2;
            }
            return null;
        }

    }
}
