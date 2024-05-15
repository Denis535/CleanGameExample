#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase {

        // State
        [MemberNotNullWhen( true, "Camera", "Character" )]
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        // Entities
        private Camera2? Camera { get; set; }
        public PlayerCharacter? Character { get; private set; }
        // Actions
        private InputActions Actions { get; set; } = default!;
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

        // Awake
        public override void Awake() {
            Actions = new InputActions();
        }
        public override void OnDestroy() {
            Actions.Dispose();
        }

        // RunGame
        public void RunGame(Camera2 camera, PlayerCharacter character) {
            Assert.Operation.Message( $"Player must be non-running" ).Valid( !IsRunning );
            IsRunning = true;
            Camera = camera;
            Character = character;
            Character.Actions = new PlayerCharacterInputActions( Actions, this );
            Actions.Enable();
        }
        public void StopGame() {
            Assert.Operation.Message( $"Player must be running" ).Valid( IsRunning );
            IsRunning = false;
            Actions.Disable();
        }

        // Pause
        public void Pause() {
            Assert.Operation.Message( $"Player must be running" ).Valid( IsRunning );
            Assert.Operation.Message( $"Player must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            Actions.Disable();
        }
        public void UnPause() {
            Assert.Operation.Message( $"Player must be running" ).Valid( IsRunning );
            Assert.Operation.Message( $"Player must be paused" ).Valid( IsPaused );
            IsPaused = false;
            Actions.Enable();
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (IsRunning) {
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply( Character );
                Hit = Raycast( Character.transform, Camera.transform );
            }
        }

        // OnDrawGizmos
        public void OnDrawGizmos() {
            if (Hit != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere( Hit.Value.Point, 0.1f );
            }
        }

        // Heleprs
        private static (Vector3 Point, float Distance, GameObject Object)? Raycast(Transform character, Transform camera) {
            var mask = ~0 & ~LayerMask.GetMask( "Bullet" );
            var hits = Physics.RaycastAll( camera.position, camera.forward, 128, mask, QueryTriggerInteraction.Ignore );
            var hit = hits.Where( i => i.transform.root != character ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                return (hit.point, hit.distance, hit.transform.gameObject);
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
