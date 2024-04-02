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
        public Vector3? Target { get; set; }
        // Rotation
        public Vector3 Rotation { get; private set; } = new Vector3( 30, 0, 0 );
        public Vector2 InputRotationDelta { get; set; }
        // Distance
        public float Distance { get; private set; } = 2.5f;
        public float InputDistanceDelta { get; set; }

        // Awake
        public void Awake() {
            this.GetDependencyContainer().Resolve<Camera>( null ).gameObject.SetActive( false );
        }
        public void OnDestroy() {
            this.GetDependencyContainer().Resolve<Camera>( null ).gameObject.SetActive( true );
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }
        public void LateUpdate() {
            Rotation = GetRotation( Rotation, InputRotationDelta );
            Distance = GetDistance( Distance, InputDistanceDelta );
            InputRotationDelta = Vector3.zero;
            InputDistanceDelta = 0;
            if (Target != null) {
                Apply( transform, Target.Value, Rotation, Distance );
            }
        }

        // Helpers
        private static Vector3 GetRotation(Vector3 rotation, Vector2 delta) {
            rotation.x += delta.y * 0.15f;
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
            transform.position = target;
            transform.Translate( 0, 0, -distance, Space.Self );
        }

    }
}
