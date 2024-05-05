#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Camera2 : EntityBase {

        public static readonly Vector2 DefaultAngles = new Vector2( 30, 0 );
        public static readonly float DefaultDistance = 1.5f;
        public static readonly float MinAngleX = -88;
        public static readonly float MaxAngleX = +88;
        public static readonly float MinDistance = 1;
        public static readonly float MaxDistance = 3;
        public static readonly Vector3 TargetOffset1 = Vector3.up * 1.8f;
        public static readonly Vector3 TargetOffset2 = Vector3.up * 2.2f;
        public static readonly Vector3 CameraOffset1 = Vector3.right * 0.2f;
        public static readonly Vector3 CameraOffset2 = Vector3.right * 0.6f;
        public static readonly float AnglesInputSensitivity = 0.15f;
        public static readonly float DistanceInputSensitivity = 0.20f;

        private Character? prevTarget;

        // Angles
        public Vector2 Angles { get; private set; }
        public float Distance { get; private set; }

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // Rotate
        public void Rotate(Vector2 delta) {
            Assert.Operation.Message( $"Method 'Rotate' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Angles = ClampAngles( GetAngles( Angles, delta ) );
        }
        public void Zoom(float delta) {
            Assert.Operation.Message( $"Method 'Zoom' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Distance = ClampDistance( GetDistance( Distance, delta ) );
        }

        // Apply
        public void Apply(Character target) {
            Assert.Operation.Message( $"Method 'Apply' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (target != prevTarget) {
                Angles = new Vector2( DefaultAngles.x, target.transform.eulerAngles.y );
                Distance = DefaultDistance;
                prevTarget = target;
            }
            Apply( transform, target.transform.position, Angles, Distance, Mathf.InverseLerp( MinDistance, MaxDistance, Distance ) );
            Apply( Camera.main, transform );
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
        private static void Apply(Camera camera, Transform transform) {
            camera.transform.localPosition = transform.localPosition;
            camera.transform.localRotation = transform.localRotation;
        }

    }
}
