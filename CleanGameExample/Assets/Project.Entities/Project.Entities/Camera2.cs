#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Camera2 : EntityBase {

        private static readonly Vector2 DefaultAngles = new Vector2( 0, 30 );
        private static readonly float DefaultDistance = 1.5f;
        private static readonly float MinAngleY = -89;
        private static readonly float MaxAngleY = +89;
        private static readonly float MinDistance = 1;
        private static readonly float MaxDistance = 3;
        private static readonly Vector3 TargetOffset1 = Vector3.up * 1.8f;
        private static readonly Vector3 TargetOffset2 = Vector3.up * 2.2f;
        private static readonly Vector3 CameraOffset1 = Vector3.right * 0.2f;
        private static readonly Vector3 CameraOffset2 = Vector3.right * 0.6f;

        // Transform
        public Vector3 Target { get; set; } = Vector3.zero;
        public Vector2 Angles { get; set; } = DefaultAngles;
        public float Distance { get; set; } = DefaultDistance;

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // OnDrawGizmosSelected
        public void OnDrawGizmosSelected() {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere( Target, 0.01f );
        }

        // SetUp
        public void SetUp(Transform target) {
            SetUp( target.position, DefaultAngles, DefaultDistance );
        }
        public void SetUp(Vector3 target) {
            SetUp( target, DefaultAngles, DefaultDistance );
        }

        // SetUp
        public void SetUp(Transform target, Vector2 angles, float distance) {
            SetUp( target.position, angles, distance );
        }
        public void SetUp(Vector3 target, Vector2 angles, float distance) {
            Target = target;
            Angles = GetAngles( angles, default );
            Distance = GetDistance( distance, default );
            Apply( transform, Target, Angles, Distance, Mathf.InverseLerp( MinDistance, MaxDistance, Distance ) );
            if (Camera.main != null && Camera.main.gameObject != gameObject) {
                Camera.main.transform.localPosition = transform.localPosition;
                Camera.main.transform.localRotation = transform.localRotation;
            }
        }

        // ManualUpdate
        public void ManualUpdate(Transform target, Vector2 rotate, float zoom) {
            ManualUpdate( target.position, rotate, zoom );
        }
        public void ManualUpdate(Vector3 target, Vector2 rotate, float zoom) {
            Target = target;
            Angles = GetAngles( Angles, rotate );
            Distance = GetDistance( Distance, zoom );
            Apply( transform, Target, Angles, Distance, Mathf.InverseLerp( MinDistance, MaxDistance, Distance ) );
            if (Camera.main != null && Camera.main.gameObject != gameObject) {
                Camera.main.transform.localPosition = transform.localPosition;
                Camera.main.transform.localRotation = transform.localRotation;
            }
        }

        // Helpers
        private static Vector2 GetAngles(Vector2 angles, Vector2 delta) {
            angles.x += delta.x * 0.15f;
            angles.y -= delta.y * 0.15f;
            angles.y = Math.Clamp( angles.y, MinAngleY, MaxAngleY );
            return angles;
        }
        private static float GetDistance(float distance, float delta) {
            distance += delta * 0.2f;
            distance = Math.Clamp( distance, MinDistance, MaxDistance );
            return distance;
        }
        private static void Apply(Transform transform, Vector3 target, Vector2 angles, float distance, float k) {
            transform.localPosition = target + Vector3.LerpUnclamped( TargetOffset1, TargetOffset2, k );
            transform.localEulerAngles = new Vector3( angles.y, angles.x, 0 );
            transform.Translate( 0, 0, -distance, Space.Self );
            transform.Translate( Vector3.LerpUnclamped( CameraOffset1, CameraOffset2, k ), Space.Self );
        }

    }
}
