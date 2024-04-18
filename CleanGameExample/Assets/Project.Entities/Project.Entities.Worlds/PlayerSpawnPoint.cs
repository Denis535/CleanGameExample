#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class PlayerSpawnPoint : SpawnPoint {

#if UNITY_EDITOR
        // OnValidate
        public new void OnValidate() {
            base.OnValidate();
        }
#endif

        // Awake
        public new void Awake() {
            base.Awake();
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // OnDrawGizmos
        private void OnDrawGizmos() {
            var size = HandleUtility.GetHandleSize( transform.position ).Convert( i => Mathf.Clamp( i, 1f, 20f ) );
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawSphere( Vector3.zero, size * 0.1f );
            Gizmos.DrawFrustum( Vector3.zero, 30f, size * 0.5f, 0f, 2f );
        }

    }
}
