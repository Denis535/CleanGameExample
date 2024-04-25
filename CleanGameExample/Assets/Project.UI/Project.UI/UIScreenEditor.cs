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
        private new UIScreen Target => (UIScreen) target;
        // Document
        private UIDocument Document => Target.GetComponentInChildren<UIDocument>();

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // OnInspectorGUI
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (!EditorApplication.isPlaying) {
                foreach (var method in typeof( UIFactory.Main ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                    if (GUILayout.Button( "Main / " + method.Name )) {
                        var element = (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                        Document.rootVisualElement.Clear();
                        Document.rootVisualElement.Add( element );
                    }
                }
                foreach (var method in typeof( UIFactory.Game ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                    if (GUILayout.Button( "Game / " + method.Name )) {
                        var element = (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                        Document.rootVisualElement.Clear();
                        Document.rootVisualElement.Add( element );
                    }
                }
                foreach (var method in typeof( UIFactory.Common ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                    if (GUILayout.Button( "Common / " + method.Name )) {
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

    }
}
#endif
