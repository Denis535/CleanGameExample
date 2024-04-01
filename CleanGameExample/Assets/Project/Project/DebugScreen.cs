#if DEBUG
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.UI;
    using UnityEngine;
    using UnityEngine.Framework;

    [DefaultExecutionOrder( ScriptExecutionOrders.Program )]
    public class DebugScreen : MonoBehaviour {

        // Globals
        private UITheme Theme { get; set; } = default!;
        private UIScreen Screen { get; set; } = default!;
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;

        // Awake
        public void Awake() {
            Theme = this.GetDependencyContainer().Resolve<UITheme>( null );
            Screen = this.GetDependencyContainer().Resolve<UIScreen>( null );
            Router = this.GetDependencyContainer().Resolve<UIRouter>( null );
            Application = this.GetDependencyContainer().Resolve<Application2>( null );
        }
        public void OnDestroy() {
        }

        // OnGUI
        public void OnGUI() {
            using (new GUILayout.VerticalScope( GUI.skin.box )) {
                {
                    // Fps
                    GUILayout.Label( "Fps: " + (1f / Time.smoothDeltaTime).ToString( "000." ) );
                    GUILayout.Space( 2 );
                }
                {
                    // State
                    GUILayout.Label( "Theme State: " + Theme.State );
                    GUILayout.Label( "Screen State: " + Screen.State );
                    GUILayout.Label( "Router State: " + Router.State );
                    GUILayout.Space( 2 );
                }
                if (Application.Game != null) {
                    // Game
                    GUILayout.Label( "Is Game Running: " + true );
                    GUILayout.Label( "Is Game Playing: " + Application.Game.IsPlaying );
                    GUILayout.Space( 2 );
                }
                {
                    // Misc
                    //GUILayout.Label( "Is Focused: " + UnityEngine.Application.isFocused );
                    //GUILayout.Label( "Focused Element: " + GetFocusedElement()?.Convert( GetDisplayString ) );
                }
            }
        }

        // Heleprs
        //private static Focusable? GetFocusedElement() {
        //    return EventSystem.current.currentSelectedGameObject?.GetComponent<PanelEventHandler>()?.panel.focusController.focusedElement;
        //}
        //private static string GetDisplayString(Focusable focusable) {
        //    var element = (VisualElement) focusable;
        //    return element.name.NullIfEmpty() ?? focusable.GetType().Name;
        //}

    }
}
#endif
