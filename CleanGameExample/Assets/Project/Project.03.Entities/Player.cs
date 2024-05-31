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

    public class Player : PlayerBase2, IPlayer {

        private Camera2? camera;
        private PlayerCharacter? character;
        private bool isInputEnabled;

        // CharacterEnum
        public PlayerCharacterEnum CharacterEnum { get; }
        // Entities
        public Camera2? Camera {
            get => camera;
            set {
                camera = value;
                Input.SetEnabled( IsInputEnabled && Camera != null && Character != null );
            }
        }
        public PlayerCharacter? Character {
            get => character;
            set {
                character = value;
                Input.SetEnabled( IsInputEnabled && Camera != null && Character != null );
            }
        }
        // Input
        private InputActions Input { get; }
        public bool IsInputEnabled {
            get => isInputEnabled;
            set {
                isInputEnabled = value;
                Input.SetEnabled( IsInputEnabled && Camera != null && Character != null );
            }
        }
        // Hit
        private (Vector3 Point, float Distance, GameObject Object)? Hit { get; set; }
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

        // Constructor
        public Player(string name, PlayerCharacterEnum character) : base( name ) {
            CharacterEnum = character;
            Input = new InputActions();
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

        // Update
        public override void Update() {
            if (Camera != null && Character != null) {
                Camera.Rotate( Input.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Input.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply( Character );
                Hit = Raycast( Camera.transform, Character.transform );
            }
        }
        public override void LateUpdate() {
        }

        // OnWin
        public void OnWin() {
            SetWin();
        }
        public void OnLose() {
            SetLose();
        }

        // IPlayer
        Vector3 IPlayer.GetLookTarget() {
            return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
        }
        bool IPlayer.IsMovePressed(out Vector3 moveVector) {
            Assert.Operation.Message( $"Method 'IsMovePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (Input.Game.Move.IsPressed()) {
                var vector = (Vector3) Input.Game.Move.ReadValue<Vector2>();
                moveVector = UnityEngine.Camera.main.transform.TransformDirection( vector.x, 0, vector.y ).Chain( i => new Vector3( i.x, 0, i.z ).normalized * vector.magnitude );
                return true;
            } else {
                moveVector = default;
                return false;
            }
        }
        bool IPlayer.IsJumpPressed() {
            Assert.Operation.Message( $"Method 'IsJumpPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Game.Jump.IsPressed();
        }
        bool IPlayer.IsCrouchPressed() {
            Assert.Operation.Message( $"Method 'IsCrouchPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Game.Crouch.IsPressed();
        }
        bool IPlayer.IsAcceleratePressed() {
            Assert.Operation.Message( $"Method 'IsAcceleratePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Game.Accelerate.IsPressed();
        }
        bool IPlayer.IsFirePressed() {
            Assert.Operation.Message( $"Method 'IsFirePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Game.Fire.IsPressed();
        }
        bool IPlayer.IsAimPressed() {
            Assert.Operation.Message( $"Method 'IsAimPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Game.Aim.IsPressed();
        }
        bool IPlayer.IsInteractPressed(out MonoBehaviour? interactable) {
            Assert.Operation.Message( $"Method 'IsInteractPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            interactable = (MonoBehaviour?) Enemy ?? Thing;
            return Input.Game.Interact.WasPressedThisFrame();
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
    public enum PlayerState {
        None,
        Win,
        Lose
    }
}
