#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    [DefaultExecutionOrder( ScriptExecutionOrders.Entity + 1000 )]
    public class Camera2 : EntityBase {

        // Target
        public Transform? Target { get; set; }
        // Transform
        public Vector3 Rotation { get; private set; } = new Vector3( 30, 0, 0 );
        public float Distance { get; private set; } = 2.5f;
        // Input
        public Vector2 RotationDeltaInput { get; set; }
        public float DistanceDeltaInput { get; set; }
        // Hit
        public Vector3? HitPoint { get; private set; }
        public GameObject? HitObject { get; private set; }

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
            if (Target != null) {
                Apply( transform, Target, Rotation, Distance );
                (HitPoint, HitObject) = Raycast( transform );
            } else {
                Apply( transform, null, Rotation, Distance );
                (HitPoint, HitObject) = (null, null);
            }
        }

        // Helpers
        private static Vector3 GetRotation(Vector3 rotation, Vector2 delta) {
            rotation.x -= delta.y * 0.15f;
            rotation.x = Math.Clamp( rotation.x, -85, 85 );
            rotation.y += delta.x * 0.15f;
            return rotation;
        }
        private static float GetDistance(float distance, float delta) {
            distance = Math.Clamp( distance + delta * 0.2f, 2, 4 );
            return distance;
        }
        private static void Apply(Transform transform, Transform? target, Vector3 rotation, float distance) {
            if (target != null) {
                var target2 = target.position + Vector3.up * 2f;
                Apply( transform, target2, rotation, distance );
            } else {
                Apply( transform, new Vector3( 0, 1024, 0 ), rotation, distance );
            }
        }
        private static void Apply(Transform transform, Vector3 target, Vector3 rotation, float distance) {
            transform.position = target;
            transform.localEulerAngles = rotation;
            transform.Translate( 0.2f + 0.3f * Mathf.InverseLerp( 2, 4, distance ), 0, -distance, Space.Self );
            transform.Translate( 0, 0.2f * Mathf.InverseLerp( 2, 4, distance ), 0, Space.World );
        }
        // Helpers
        private static (Vector3?, GameObject?) Raycast(Transform transform) {
            var hit = default( RaycastHit );
            var mask = ~0;
            if (Physics.Raycast( transform.position, transform.forward, out hit, 256, mask, QueryTriggerInteraction.Ignore )) {
                return (hit.point, hit.transform.gameObject);
            } else {
                return (null, null);
            }
        }

    }
}
