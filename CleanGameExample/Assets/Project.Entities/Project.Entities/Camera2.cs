#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    [DefaultExecutionOrder( ScriptExecutionOrders.Entity )]
    public class Camera2 : EntityBase {

        // Transform
        public Vector3 Target { get; private set; }
        public Vector2 Angles { get; private set; } = new Vector2( 0, 30 );
        public float Distance { get; private set; } = 2.5f;
        // Hit
        public Vector3? HitPoint { get; private set; }
        public GameObject? HitObejct { get; private set; }

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

        // OnDrawGizmos
        public void OnDrawGizmos() {
            if (HitPoint != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere( HitPoint.Value, 0.1f );
            }
        }

        // Input
        public void SetTarget(Transform target, Vector3 offset) {
            Target = target.position + offset;
        }
        public void Rotate(Vector2 delta) {
            Angles = GetAngles( Angles, delta );
        }
        public void Zoom(float delta) {
            Distance = GetDistance( Distance, delta );
        }

        // Apply
        public void Apply() {
            Apply( transform, Target, Angles, Distance );
            HitPoint = Raycast( transform, out var hitObejct );
            HitObejct = hitObejct;
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
        private static void Apply(Transform transform, Vector3 target, Vector2 angles, float distance) {
            transform.position = target;
            transform.localEulerAngles = new Vector3( angles.y, angles.x, 0 );
            transform.Translate( 0.2f + 0.3f * Mathf.InverseLerp( 2, 4, distance ), 0, -distance, Space.Self );
            transform.Translate( 0, 0.2f * Mathf.InverseLerp( 2, 4, distance ), 0, Space.World );
        }
        // Heleprs
        private static Vector3? Raycast(Transform camera, out GameObject? @object) {
            //var mask = ~0;
            //var hits = Physics.RaycastAll( camera.position, camera.forward, 128, mask, QueryTriggerInteraction.Ignore );
            //var hit = hits.FirstOrDefault();
            //if (hit.collider) {
            //    @object = hit.transform.gameObject;
            //    return hit.point;
            //}
            //@object = null;
            //return camera.TransformPoint( Vector3.forward * 128 );
            var mask = ~0;
            if (Physics.Raycast( camera.position, camera.forward, out var hit, 128, mask, QueryTriggerInteraction.Ignore )) {
                @object = hit.transform.gameObject;
                return hit.point;
            } else {
                @object = null;
                return camera.TransformPoint( Vector3.forward * 128 );
            }
        }

    }
}
