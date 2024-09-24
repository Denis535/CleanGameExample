#if UNITY_EDITOR
#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Actors;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor( typeof( Camera2 ) )]
    public class Camera2Editor : Editor {

        private Camera2 Target => (Camera2) target;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.ObjectField( "Target", Target.Target, typeof( CharacterBase ), true );
            EditorGUILayout.Vector2Field( "Angles", Target.Angles );
            EditorGUILayout.FloatField( "Distance", Target.Distance );
        }

    }
}
#endif
