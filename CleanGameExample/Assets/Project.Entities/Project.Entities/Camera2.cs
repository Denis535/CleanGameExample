#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Camera2 : EntityBase {

        public static readonly Vector2 DefaultAngles = new Vector2( 30, 0 );
        public static readonly float DefaultDistance = 1.5f;
        public static readonly float MinAngleX = -89;
        public static readonly float MaxAngleX = +89;
        public static readonly float MinDistance = 1;
        public static readonly float MaxDistance = 3;
        public static readonly Vector3 TargetOffset1 = Vector3.up * 1.8f;
        public static readonly Vector3 TargetOffset2 = Vector3.up * 2.2f;
        public static readonly Vector3 CameraOffset1 = Vector3.right * 0.2f;
        public static readonly Vector3 CameraOffset2 = Vector3.right * 0.6f;
        public static readonly float AnglesInputSensitivity = 0.15f;
        public static readonly float DistanceInputSensitivity = 0.20f;

        // Target
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
            Target = target.position;
            Angles = new Vector2( DefaultAngles.x, target.eulerAngles.y );
            Distance = DefaultDistance;
        }

        // SetTarget
        public void SetTarget(Vector3 target) {
            Target = target;
        }
        public void SetAngles(Vector2 angles) {
            Angles = ClampAngles( angles );
        }
        public void SetDistance(float distance) {
            Distance = ClampDistance( distance );
        }

        // Rotate
        public void Rotate(Vector2 delta) {
            Angles = ClampAngles( GetAngles( Angles, delta ) );
        }
        public void Zoom(float delta) {
            Distance = ClampDistance( GetDistance( Distance, delta ) );
        }

        // Apply
        public void Apply() {
            Apply( transform, Target, Angles, Distance, Mathf.InverseLerp( MinDistance, MaxDistance, Distance ) );
            if (Camera.main != null && Camera.main.gameObject != gameObject) {
                Camera.main.transform.localPosition = transform.localPosition;
                Camera.main.transform.localRotation = transform.localRotation;
            }
        }

        // Helpers
        private static Vector2 GetAngles(Vector2 angles, Vector2 delta) {
            angles.x -= delta.y * AnglesInputSensitivity;
            angles.y += delta.x * AnglesInputSensitivity;
            return angles;
        }
        private static float GetDistance(float distance, float delta) {
            distance += delta * DistanceInputSensitivity;
            distance = Math.Clamp( distance, MinDistance, MaxDistance );
            return distance;
        }
        // Helpers
        private static Vector2 ClampAngles(Vector2 angles) {
            angles.x = Math.Clamp( angles.x, MinAngleX, MaxAngleX );
            return angles;
        }
        private static float ClampDistance(float distance) {
            distance = Math.Clamp( distance, MinDistance, MaxDistance );
            return distance;
        }
        // Helpers
        private static void Apply(Transform transform, Vector3 target, Vector2 angles, float distance, float k) {
            transform.localPosition = target + Vector3.LerpUnclamped( TargetOffset1, TargetOffset2, k );
            transform.localEulerAngles = angles;
            transform.Translate( 0, 0, -distance, Space.Self );
            transform.Translate( Vector3.LerpUnclamped( CameraOffset1, CameraOffset2, k ), Space.Self );
        }

    }
}
