#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class LootSpawnPoint : MonoBehaviour {

#if UNITY_EDITOR
        // OnValidate
        public void OnValidate() {
            gameObject.name = GetType().Name;
            var content = EditorGUIUtility.IconContent( "sv_label_4" );
            EditorGUIUtility.SetIconForObject( gameObject, (Texture2D) content.image );
        }
#endif

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

    }
}
