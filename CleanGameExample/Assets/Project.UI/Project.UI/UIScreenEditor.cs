#if UNITY_EDITOR
#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    [CustomEditor( typeof( UIScreen ) )]
    public class UIScreenEditor : UnityEngine.Framework.UI.UIScreenEditor {

        // Target
        private UIScreen Target => (UIScreen) target;
        private UIDocument Document => Target.GetComponentInChildren<UIDocument>();

        // Awake
        public new void Awake() {
        }
        public new void OnDestroy() {
        }

        // OnInspectorGUI
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (!EditorApplication.isPlaying) {
                foreach (var method in typeof( MainViewFactory ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                    if (Button( "Show Main / " + method.Name )) {
                        var element = (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                        Document.rootVisualElement.Clear();
                        Document.rootVisualElement.Add( element );
                    }
                }
                foreach (var method in typeof( GameViewFactory ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                    if (Button( "Show Game / " + method.Name )) {
                        var element = (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                        Document.rootVisualElement.Clear();
                        Document.rootVisualElement.Add( element );
                    }
                }
                foreach (var method in typeof( CommonViewFactory ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                    if (Button( "Show Common / " + method.Name )) {
                        var element = (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                        Document.rootVisualElement.Clear();
                        Document.rootVisualElement.Add( element );
                    }
                }
                if (GUILayout.Button( "Clear" )) {
                    Document.rootVisualElement.Clear();
                }
            }
        }

        // Helpers
        private static bool Button(string text) {
            var style = new GUIStyle( GUI.skin.button ) {
                alignment = TextAnchor.MiddleLeft
            };
            return GUILayout.Button( text, style );
        }

    }
}
#endif
