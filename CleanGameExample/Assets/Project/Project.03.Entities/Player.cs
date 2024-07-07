#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Characters;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase3 {

        private PlayerCharacter? character;
        private Camera2? camera;

        private InputActions_Player Input { get; }
        public PlayerCharacter? Character {
            get => character;
            internal set {
                if (character != null) {
                    character.Input = null;
                    Input.Disable();
                }
                character = value;
                if (character != null) {
                    character.Input = this;
                }
            }
        }
        public Camera2? Camera {
            get => camera;
            internal set {
                if (camera != null) {
                    camera.Input = null;
                    Input.Disable();
                }
                camera = value;
                if (camera != null) {
                    camera.Input = this;
                }
            }
        }

        public (Vector3 Point, float Distance, GameObject Object)? Hit { get; private set; }
        public EnemyCharacter? Enemy {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 16f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    return @object.GetComponent<EnemyCharacter>();
                }
                return null;
            }
        }
        public Thing? Thing {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 2.5f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    return @object.GetComponent<Thing>();
                }
                return null;
            }
        }

        public Player(IDependencyContainer container, string name, PlayerKind kind) : base( container, name, kind ) {
            Input = new InputActions_Player();
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

        public override void OnFixedUpdate() {
        }
        public override void OnUpdate() {
            {
                Input.SetEnabled( Time.timeScale != 0f && Cursor.lockState == CursorLockMode.Locked && Character != null && Camera != null );
            }
            if (Character != null) {

            }
            if (Camera != null) {
                Camera.Target = Character;
                Hit = Raycast( Camera.Ray, Character?.transform );
            }
        }

        public override Vector3 GetMoveVector() {
            Assert.Operation.Message( $"Method 'GetMoveVector' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (Input.Character.Move.IsPressed()) {
                var vector = Input.Character.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                vector = UnityEngine.Camera.main.transform.TransformDirection( vector );
                vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public override Vector3? GetBodyTarget() {
            Assert.Operation.Message( $"Method 'GetBodyTarget' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            if (Input.Character.Move.IsPressed()) {
                var vector = Input.Character.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                if (vector != Vector3.zero) {
                    vector = UnityEngine.Camera.main.transform.TransformDirection( vector );
                    vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                    return Character!.transform.position + vector;
                }
            }
            return null;
        }
        public override Vector3? GetHeadTarget() {
            Assert.Operation.Message( $"Method 'GetHeadTarget' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            if (Input.Character.Move.IsPressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
        }
        public override Vector3? GetWeaponTarget() {
            Assert.Operation.Message( $"Method 'GetWeaponTarget' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            if (Input.Character.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public override bool IsJumpPressed() {
            Assert.Operation.Message( $"Method 'IsJumpPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Character.Jump.IsPressed();
        }
        public override bool IsCrouchPressed() {
            Assert.Operation.Message( $"Method 'IsCrouchPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Character.Crouch.IsPressed();
        }
        public override bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Method 'IsAcceleratePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Character.Accelerate.IsPressed();
        }
        public override bool IsFirePressed() {
            Assert.Operation.Message( $"Method 'IsFirePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Character.Fire.IsPressed();
        }
        public override bool IsAimPressed() {
            Assert.Operation.Message( $"Method 'IsAimPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Character.Aim.IsPressed();
        }
        public override bool IsInteractPressed(out MonoBehaviour? interactable) {
            Assert.Operation.Message( $"Method 'IsInteractPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            interactable = (MonoBehaviour?) Enemy ?? (MonoBehaviour?) Thing;
            return Input.Character.Interact.WasPressedThisFrame();
        }

        public override Vector2 GetLookDelta() {
            Assert.Operation.Message( $"Method 'GetLookDelta' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Camera.Look.ReadValue<Vector2>();
        }
        public override float GetZoomDelta() {
            Assert.Operation.Message( $"Method 'GetZoomDelta' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Camera.Zoom.ReadValue<Vector2>().y;
        }

        private static (Vector3 Point, float Distance, GameObject Object)? Raycast(Ray ray, Transform? ignore) {
            var mask = ~(Masks.Entity_Approximate | Masks.Trivial);
            var hit = Utils.RaycastAll( ray, 128, mask, QueryTriggerInteraction.Ignore ).Where( i => i.transform.root != ignore ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                return (hit.point, hit.distance, hit.collider.gameObject);
            } else {
                return null;
            }
        }

    }
}
