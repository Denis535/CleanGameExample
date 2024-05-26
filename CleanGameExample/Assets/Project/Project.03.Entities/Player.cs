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

    public class Player : PlayerBase {

        // Name
        public string Name { get; }
        // Character
        public PlayerCharacterEnum CharacterEnum { get; }
        // Input
        private InputActions Input { get; }
        // Entities
        public Camera2? Camera { get; private set; }
        public PlayerCharacter? Character { get; private set; }
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
        public Player(string name, PlayerCharacterEnum character) {
            Name = name;
            CharacterEnum = character;
            Input = new InputActions();
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

        // SetInputEnabled
        public void SetInputEnabled(bool value) {
            if (value) Input.Enable(); else Input.Disable();
        }

        // SetCamera
        public void SetCamera(Camera2 camera) {
            Camera = camera;
        }

        // SetCharacter
        public void SetCharacter(PlayerCharacter character) {
            Character = character;
            Character.Input = new PlayerCharacterInput( Input, this );
        }

        // Update
        public void Update() {
            if (Camera != null && Character != null) {
                Camera.Rotate( Input.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Input.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply( Character );
                Hit = Raycast( Camera.transform, Character.transform );
            }
        }
        public void LateUpdate() {
        }

        // Heleprs
        private static (Vector3 Point, float Distance, GameObject Object)? Raycast(Transform ray, Transform ignore) {
            var hit = Physics2.RaycastAll( ray.position, ray.forward, 128 ).Where( i => i.transform.root != ignore ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                return (hit.point, hit.distance, hit.collider.gameObject);
            } else {
                return null;
            }
        }

    }
    // PlayerCharacterInput
    internal class PlayerCharacterInput : IPlayerCharacterInput {

        private readonly InputActions input;
        private readonly Player player;

        public bool IsEnabled => input.asset.enabled;
        public Vector3 LookTarget => player.Hit?.Point ?? Camera.main.transform.TransformPoint( Vector3.forward * 128f );

        public PlayerCharacterInput(InputActions input, Player player) {
            this.input = input;
            this.player = player;
        }

        public bool IsMovePressed(out Vector3 moveVector) {
            Assert.Operation.Message( $"Method 'IsMovePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (input.Game.Move.IsPressed()) {
                var vector = (Vector3) input.Game.Move.ReadValue<Vector2>();
                moveVector = Camera.main.transform.TransformDirection( vector.x, 0, vector.y ).Chain( i => new Vector3( i.x, 0, i.z ).normalized * vector.magnitude );
                return true;
            } else {
                moveVector = default;
                return false;
            }
        }
        public bool IsJumpPressed() {
            Assert.Operation.Message( $"Method 'IsJumpPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return input.Game.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            Assert.Operation.Message( $"Method 'IsCrouchPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return input.Game.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Method 'IsAcceleratePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return input.Game.Accelerate.IsPressed();
        }
        public bool IsFirePressed() {
            Assert.Operation.Message( $"Method 'IsFirePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return input.Game.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            Assert.Operation.Message( $"Method 'IsAimPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return input.Game.Aim.IsPressed();
        }
        public bool IsInteractPressed(out GameObject? interactable) {
            Assert.Operation.Message( $"Method 'IsInteractPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            interactable = player.Enemy ?? player.Loot;
            return input.Game.Interact.WasPressedThisFrame();
        }

    }
}
