#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using Unity.Services.Authentication;
    using UnityEngine;
    using UnityEngine.Framework;

    public class DependencyContainer : MonoBehaviour, IDependencyContainer {

        [SerializeField] private UITheme uiTheme = default!;
        [SerializeField] private UIScreen uiScreen = default!;
        [SerializeField] private UIFactory uiFactory = default!;
        [SerializeField] private UIRouter uiRouter = default!;
        [SerializeField] private Application2 application = default!;
        [SerializeField] private new Camera camera = default!;

        // Globals
        private UITheme UITheme => uiTheme;
        private UIScreen UIScreen => uiScreen;
        private UIFactory UIFactory => uiFactory;
        private UIRouter UIRouter => uiRouter;
        private Application2 Application => application;
        private Camera Camera => camera;
        private Globals Globals { get; set; } = default!;
        private Globals.ProfileSettings ProfileSettings { get; set; } = default!;
        private Globals.VideoSettings VideoSettings { get; set; } = default!;
        private Globals.AudioSettings AudioSettings { get; set; } = default!;
        private Globals.Preferences Preferences { get; set; } = default!;
        private IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;

        // Awake
        public void Awake() {
            IDependencyContainer.Instance = this;
            Globals = new Globals();
            ProfileSettings = new Globals.ProfileSettings();
            VideoSettings = new Globals.VideoSettings();
            AudioSettings = new Globals.AudioSettings();
            Preferences = new Globals.Preferences();
        }
        public void OnDestroy() {
        }

        // GetObject
        object? IDependencyContainer.GetObject(Type type, object? argument) {
            // UI
            if (type == typeof( UITheme )) {
                var result = UITheme;
                return result;
            }
            if (type == typeof( UIScreen )) {
                var result = UIScreen;
                return result;
            }
            if (type == typeof( UIFactory )) {
                var result = UIFactory;
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
            if (type == typeof( Camera )) {
                var result = Camera;
                return result;
            }
            if (type == typeof( Globals )) {
                var result = Globals;
                return result;
            }
            if (type == typeof( Globals.ProfileSettings )) {
                var result = ProfileSettings;
                return result;
            }
            if (type == typeof( Globals.VideoSettings )) {
                var result = VideoSettings;
                return result;
            }
            if (type == typeof( Globals.AudioSettings )) {
                var result = AudioSettings;
                return result;
            }
            if (type == typeof( Globals.Preferences )) {
                var result = Preferences;
                return result;
            }
            if (type == typeof( IAuthenticationService )) {
                var result = AuthenticationService;
                return result;
            }
            // Misc
            if (type.IsDescendentOf( typeof( MonoBehaviour ) )) {
                var result = (MonoBehaviour) FindAnyObjectByType( type, FindObjectsInactive.Exclude );
                return result;
            }
            if (type.HasElementType && type.GetElementType().IsDescendentOf( typeof( MonoBehaviour ) )) {
                var result = FindObjectsByType( type.GetElementType(), FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                var result2 = Array.CreateInstance( type.GetElementType(), result.Length );
                result.CopyTo( result2, 0 );
                return result2;
            }
            return null;
        }

    }
}
