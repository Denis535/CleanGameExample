#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase {

        // CharacterEnum
        public PlayerCharacterEnum CharacterEnum { get; }
        // Entities
        public Camera2? Camera { get; private set; }
        public PlayerCharacter? Character { get; private set; }
        // Actions
        private InputActions Actions { get; }
        // Hit
        internal (Vector3 Point, float Distance, GameObject Object)? Hit { get; set; }
        public GameObject? Enemy {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 16f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    if (@object.IsEnemy()) return @object;
                }
                return null;
            }
        }
        public GameObject? Loot {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 2.5f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    if (@object.IsWeapon()) return @object;
                }
                return null;
            }
        }

        // Constructor
        public Player(PlayerCharacterEnum characterType) {
            CharacterEnum = characterType;
            Actions = new InputActions();
            Actions.Enable();
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // SetCamera
        public void SetCamera(Camera2 camera) {
            Camera = camera;
        }

        // SetCharacter
        public void SetCharacter(PlayerCharacter character) {
            Character = character;
            Character.Actions = new PlayerCharacterInputActions( Actions, this );
        }

        // SetInputEnabled
        public void SetInputEnabled(bool value) {
            if (value) Actions.Enable(); else Actions.Disable();
        }

        // Update
        public void Update() {
            if (Camera != null && Character != null) {
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply( Character );
                Hit = Raycast( Camera.transform, Character.transform );
            }
        }
        public void LateUpdate() {
        }

        // Heleprs
        private static (Vector3 Point, float Distance, GameObject Object)? Raycast(Transform camera, Transform character) {
            var mask = ~0 & ~Layers.BulletMask;
            var hits = Physics.RaycastAll( camera.position, camera.forward, 128, mask, QueryTriggerInteraction.Ignore );
            var hit = hits.Where( i => i.transform.root != character ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                return (hit.point, hit.distance, hit.collider.gameObject);
            } else {
                return null;
            }
        }

    }
    // PlayerCharacterInputActions
    internal class PlayerCharacterInputActions : IPlayerCharacterInputActions {

        private readonly InputActions actions;
        private readonly Player player;

        public bool IsEnabled => actions.asset.enabled;
        public Vector3 LookTarget => player.Hit?.Point ?? Camera.main.transform.TransformPoint( Vector3.forward * 128f );

        public PlayerCharacterInputActions(InputActions actions, Player player) {
            this.actions = actions;
            this.player = player;
        }

        public bool IsMovePressed(out Vector3 moveVector) {
            Assert.Operation.Message( $"Method 'IsMovePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (actions.Game.Move.IsPressed()) {
                var vector = (Vector3) actions.Game.Move.ReadValue<Vector2>();
                moveVector = Camera.main.transform.TransformDirection( vector.x, 0, vector.y ).Chain( i => new Vector3( i.x, 0, i.z ).normalized * vector.magnitude );
                return true;
            } else {
                moveVector = default;
                return false;
            }
        }
        public bool IsJumpPressed() {
            Assert.Operation.Message( $"Method 'IsJumpPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            Assert.Operation.Message( $"Method 'IsCrouchPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Method 'IsAcceleratePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Accelerate.IsPressed();
        }
        public bool IsFirePressed() {
            Assert.Operation.Message( $"Method 'IsFirePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            Assert.Operation.Message( $"Method 'IsAimPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Aim.IsPressed();
        }
        public bool IsInteractPressed(out GameObject? interactable) {
            Assert.Operation.Message( $"Method 'IsInteractPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            interactable = player.Enemy ?? player.Loot;
            return actions.Game.Interact.WasPressedThisFrame();
        }

    }
}
