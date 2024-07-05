#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class PlayerPoint : Point {

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
            gameObject.isStatic = true;
            transform.localPosition = Snapping.Snap( transform.localPosition, Vector3.one * 0.5f );
            transform.localEulerAngles = Snapping.Snap( transform.localEulerAngles, Vector3.one * 45f );
            transform.localEulerAngles = new Vector3( 0, transform.localEulerAngles.y, 0 );
            if (transform.parent == null) transform.parent = GameObject.Find( "World" )?.transform;
        }
#endif

#if UNITY_EDITOR
        protected override void OnDrawGizmos() {
            var size = HandleUtility.GetHandleSize( transform.position ).Chain( i => Mathf.Clamp( i, 1f, 20f ) );
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawSphere( Vector3.zero, size * 0.1f );
            Gizmos.DrawFrustum( Vector3.zero, 30f, size * 0.5f, 0f, 2f );
        }
#endif

    }
}
