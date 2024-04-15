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
        public Vector2 Angles { get; private set; } = new Vector2( 0, 30 );
        public float Distance { get; private set; } = 2.5f;
        // Input
        public Vector2 AnglesDeltaInput { get; set; }
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
            Angles = GetAngles( Angles, AnglesDeltaInput );
            Distance = GetDistance( Distance, DistanceDeltaInput );
            if (Target != null) {
                Apply( transform, Target, Angles, Distance );
            } else {
                Apply( transform, null, Angles, Distance );
            }
        }

        // Helpers
        private static Vector2 GetAngles(Vector2 angles, Vector2 delta) {
            angles.x += delta.x * 0.15f;
            angles.y -= delta.y * 0.15f;
            angles.y = Math.Clamp( angles.y, -89, 89 );
            return angles;
        }
        private static float GetDistance(float distance, float delta) {
            distance = Math.Clamp( distance + delta * 0.2f, 2, 4 );
            return distance;
        }
        private static void Apply(Transform transform, Transform? target, Vector2 angles, float distance) {
            if (target != null) {
                var target2 = target.position + Vector3.up * 2f;
                Apply( transform, target2, angles, distance );
            } else {
                var target2 = new Vector3( 0, 1024, 0 );
                Apply( transform, target2, angles, distance );
            }
        }
        private static void Apply(Transform transform, Vector3 target, Vector2 angles, float distance) {
            transform.position = target;
            transform.localEulerAngles = new Vector3( angles.y, angles.x, 0 );
            transform.Translate( 0.2f + 0.3f * Mathf.InverseLerp( 2, 4, distance ), 0, -distance, Space.Self );
            transform.Translate( 0, 0.2f * Mathf.InverseLerp( 2, 4, distance ), 0, Space.World );
        }

    }
}
