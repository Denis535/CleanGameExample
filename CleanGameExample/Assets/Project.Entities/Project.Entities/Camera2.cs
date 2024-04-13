#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class Camera2 : EntityBase {

        // Target
        public Transform? Target { get; set; }
        // Transform
        public Vector3 Rotation { get; private set; } = new Vector3( 30, 0, 0 );
        public float Distance { get; private set; } = 2.5f;
        // Input
        public Vector2 RotationDeltaInput { get; set; }
        public float DistanceDeltaInput { get; set; }

        // Awake
        public void Awake() {
            this.GetDependencyContainer().RequireDependency<Camera>( null ).gameObject.SetActive( false );
        }
        public void OnDestroy() {
            this.GetDependencyContainer().RequireDependency<Camera>( null ).gameObject.SetActive( true );
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }
        public void LateUpdate() {
            Rotation = GetRotation( Rotation, RotationDeltaInput );
            Distance = GetDistance( Distance, DistanceDeltaInput );
            RotationDeltaInput = Vector2.zero;
            DistanceDeltaInput = 0;
            if (Target != null) {
                Apply( transform, Target.position, Rotation, Distance );
            } else {
                Apply( transform, Vector3.up * 100, Rotation, Distance );
                Target = null;
            }
        }

        // Helpers
        private static Vector3 GetRotation(Vector3 rotation, Vector2 delta) {
            rotation.x -= delta.y * 0.15f;
            rotation.x = Math.Clamp( rotation.x, -90, 90 );
            rotation.y += delta.x * 0.15f;
            return rotation;
        }
        private static float GetDistance(float distance, float delta) {
            distance = Math.Clamp( distance + delta * 0.2f, 2, 4 );
            return distance;
        }
        private static void Apply(Transform transform, Vector3 target, Vector3 rotation, float distance) {
            transform.localEulerAngles = rotation;
            transform.position = target +
                (Vector3.up * 1.7f) + (Vector3.up * 0.5f * Mathf.InverseLerp( 2, 4, distance )) +
                (Vector3.right * 0.3f) + (Vector3.right * 0.5f * Mathf.InverseLerp( 2, 4, distance ));
            transform.Translate( 0, 0, -distance, Space.Self );
        }

    }
}
