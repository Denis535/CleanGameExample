#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor( typeof( Camera2 ) )]
    public class Camera2Editor : Editor {

        public Camera2 Target => (Camera2) target;

        // OnInspectorGUI
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            Target.Target = EditorGUILayout.Vector3Field( "Target", Target.Target );
            Target.Angles = EditorGUILayout.Vector2Field( "Angles", Target.Angles );
            Target.Distance = EditorGUILayout.FloatField( "Distance", Target.Distance );
        }

        // Helpers
        private static void LabelField(string label, string? text) {
            using (new EditorGUILayout.HorizontalScope()) {
                EditorGUILayout.PrefixLabel( label );
                EditorGUI.SelectableLabel( GUILayoutUtility.GetRect( new GUIContent( text ), GUI.skin.textField ), text, GUI.skin.textField );
            }
        }

    }
}
