#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class Camera2 {
        public static class Factory {

            private static readonly PrefabHandle<Camera2> Prefab = new PrefabHandle<Camera2>( R.Project.Entities.Value_Camera );

            public static void Load() {
                Prefab.Load().Wait();
            }
            public static void Unload() {
                Prefab.Release();
            }

            public static Camera2 Create() {
                return GameObject.Instantiate( Prefab.GetValue() );
            }

        }
    }
    public partial class Camera2 : MonoBehaviour {

        public static readonly Vector2 DefaultAngles = new Vector2( 30, 0 );
        public static readonly float DefaultDistance = 1.5f;
        public static readonly float MinAngleX = -88;
        public static readonly float MaxAngleX = +88;
        public static readonly float MinDistance = 1;
        public static readonly float MaxDistance = 3;
        public static readonly float AnglesInputSensitivity = 0.15f;
        public static readonly float DistanceInputSensitivity = 0.20f;

        private Character? prevTarget;

        // Angles
        public Vector2 Angles { get; private set; }
        // Distance
        public float Distance { get; private set; }
        // Ray
        public Ray Ray => new Ray( transform.position, transform.forward );

        // Awake
        protected void Awake() {
        }
        protected void OnDestroy() {
        }

        // Rotate
        public void Rotate(Vector2 delta) {
            Assert.Operation.Message( $"Method 'Rotate' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Angles += new Vector2( -delta.y, delta.x ) * AnglesInputSensitivity;
            Angles = new Vector2( Math.Clamp( Angles.x, MinAngleX, MaxAngleX ), Angles.y );
        }

        // Zoom
        public void Zoom(float delta) {
            Assert.Operation.Message( $"Method 'Zoom' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Distance += delta * DistanceInputSensitivity;
            Distance = Math.Clamp( Distance, MinDistance, MaxDistance );
        }

        // Apply
        public void Apply(Character target) {
            Assert.Operation.Message( $"Method 'Apply' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (target != prevTarget) {
                Angles = new Vector2( DefaultAngles.x, target.transform.eulerAngles.y );
                Distance = DefaultDistance;
                prevTarget = target;
            }
            Apply( transform, target, Angles, Distance );
            Apply( Camera.main, transform );
        }

        // Helpers
        private static void Apply(Transform transform, Character target, Vector2 angles, float distance) {
            if (target.IsAlive) {
                var distance01 = Mathf.InverseLerp( MinDistance, MaxDistance, distance );
                transform.localPosition = target.transform.position;
                transform.localEulerAngles = angles;
                transform.Translate( 0, 0, -distance, Space.Self );
                transform.Translate( Vector3.LerpUnclamped( Vector3.right * 0.2f, Vector3.right * 0.6f, distance01 ), Space.Self );
                transform.Translate( Vector3.LerpUnclamped( target.transform.up * 1.8f, target.transform.up * 2.2f, distance01 ), Space.World );
            } else {
                transform.localPosition = target.transform.position;
                transform.localEulerAngles = angles;
                transform.Translate( 0, 0, -distance, Space.Self );
                transform.Translate( target.transform.up * 1.5f, Space.World );
            }
        }
        private static void Apply(Camera camera, Transform transform) {
            camera.transform.localPosition = transform.localPosition;
            camera.transform.localRotation = transform.localRotation;
        }

    }
}
