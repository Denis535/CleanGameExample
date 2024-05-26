#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using UnityEngine;

    public class DependencyContainer : MonoBehaviour, IDependencyContainer {

        [SerializeField] private UITheme uiTheme = default!;
        [SerializeField] private UIScreen uiScreen = default!;
        [SerializeField] private UIRouter uiRouter = default!;
        [SerializeField] private Application2 application = default!;

        // UI
        private UITheme UITheme => uiTheme;
        private UIScreen UIScreen => uiScreen;
        private UIRouter UIRouter => uiRouter;
        // App
        private Application2 Application => application;

        // Awake
        public void Awake() {
            Utils.Container = this;
        }
        public void OnDestroy() {
        }

        // GetObject
        Option<object?> IDependencyContainer.GetValue(Type type, object? argument) {
            this.Validate();
            // UI
            if (type == typeof( UITheme )) {
                var result = UITheme ?? throw Exceptions.Internal.NullReference( $"Reference 'UITheme' is null" );
                return new Option<object?>( result );
            }
            if (type == typeof( UIScreen )) {
                var result = UIScreen ?? throw Exceptions.Internal.NullReference( $"Reference 'UIScreen' is null" );
                return new Option<object?>( result );
            }
            if (type == typeof( UIRouter )) {
                var result = UIRouter ?? throw Exceptions.Internal.NullReference( $"Reference 'UIRouter' is null" );
                return new Option<object?>( result );
            }
            // App
            if (type == typeof( Application2 )) {
                var result = Application ?? throw Exceptions.Internal.NullReference( $"Reference 'Application' is null" );
                return new Option<object?>( result );
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

    }
}
