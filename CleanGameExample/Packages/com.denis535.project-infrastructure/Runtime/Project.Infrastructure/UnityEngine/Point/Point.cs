#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public abstract class Point : MonoBehaviour {

#if UNITY_EDITOR
        protected virtual void OnValidate() {
            gameObject.name = GetType().Name;
        }
#endif

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos() {
            var size = HandleUtility.GetHandleSize( transform.position ).Chain( i => Math.Clamp( i, 1f, 20f ) );
            Gizmos.color = Color.white;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawSphere( Vector3.zero, size * 0.05f );
            Gizmos.DrawFrustum( Vector3.zero, 10f, 0.25f, 0f, 2f );
        }
#endif

    }
}
