#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor( typeof( Camera2 ) )]
    public class Camera2Editor : Editor {

        // Target
        private Camera2 Target => (Camera2) target;

        // OnInspectorGUI
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.Vector2Field( "Angles", Target.Angles );
            EditorGUILayout.FloatField( "Distance", Target.Distance );
        }

    }
}
