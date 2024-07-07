#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

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

        private static readonly Vector2 DefaultAngles = new Vector2( 30, 0 );
        private static readonly float DefaultDistance = 1.5f;
        private static readonly float MinAngleX = -88;
        private static readonly float MaxAngleX = +88;
        private static readonly float MinDistance = 1;
        private static readonly float MaxDistance = 3;
        private static readonly float AnglesInputSensitivity = 0.15f;
        private static readonly float DistanceInputSensitivity = 0.20f;

        private Character? target;

        public ICameraInput? Input { get; set; }
        public Character? Target {
            get => target;
            set {
                if (value != target) IsTargetChanged = true;
                target = value;
            }
        }
        public bool IsTargetChanged { get; private set; }
        public Vector2 Angles { get; private set; }
        public float Distance { get; private set; }
        public Ray Ray => new Ray( transform.position, transform.forward );

        protected void Awake() {
        }
        protected void OnDestroy() {
        }

        protected void Update() {
        }
        protected void LateUpdate() {
            if (Target != null) {
                if (IsTargetChanged) {
                    Angles = new Vector2( DefaultAngles.x, Target.transform.eulerAngles.y );
                    Distance = DefaultDistance;
                    IsTargetChanged = false;
                }
                if (Input != null) {
                    {
                        var delta = Input.GetLookDelta() * AnglesInputSensitivity;
                        var angles = Angles + new Vector2( -delta.y, delta.x );
                        angles.x = Math.Clamp( angles.x, MinAngleX, MaxAngleX );
                        Angles = angles;
                    }
                    {
                        var delta = Input.GetZoomDelta() * DistanceInputSensitivity;
                        var distance = Distance + delta;
                        distance = Math.Clamp( distance, MinDistance, MaxDistance );
                        Distance = distance;
                    }
                }
                Apply( transform, Target, Angles, Distance );
                Apply( Camera.main, transform );
            }
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
