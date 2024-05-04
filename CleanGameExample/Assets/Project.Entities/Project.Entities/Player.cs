#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Project.Entities.Characters;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase {

        // IsPaused
        public bool IsPaused { get; private set; }
        // Entities
        private Game Game { get; set; } = default!;
        private Camera2 Camera { get; set; } = default!;
        public Character? Character { get; private set; }
        private World World { get; set; } = default!;
        // Actions
        private InputActions Actions { get; set; } = default!;
        // Hit
        internal (Vector3 Point, float Distance, GameObject Object)? Hit { get; set; }
        public GameObject? Enemy {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 16f && Hit.Value.Object.IsEnemy()) {
                    return Hit?.Object;
                }
                return null;
            }
        }
        public GameObject? Loot {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 2.5f && Hit.Value.Object.IsLoot()) {
                    return Hit?.Object;
                }
                return null;
            }
        }

        // Awake
        public void Awake() {
            Game = Utils.Container.RequireDependency<Game>( null );
            Camera = Utils.Container.RequireDependency<Camera2>( null );
            World = Utils.Container.RequireDependency<World>( null );
            Actions = new InputActions();
        }
        public void OnDestroy() {
            Actions.Dispose();
        }

        // SetPaused
        public void SetPaused(bool isPaused) {
            IsPaused = isPaused;
            if (IsPaused) {
                if (Character != null) Actions.Disable();
            } else {
                if (Character != null) Actions.Enable();
            }
        }

        // SetCharacter
        public void SetCharacter(Character? character) {
            if (Character != null) {
                Character.SetActions( null );
                if (!IsPaused) Actions.Disable();
                Hit = null;
            }
            Character = character;
            if (Character != null) {
                Character.SetActions( new CharacterInputActions( this, Actions ) );
                if (!IsPaused) Actions.Enable();
            }
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (Actions.asset.enabled) {
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
            }
            if (Character != null) {
                Camera.Apply( Character );
                if (Raycast( Camera.transform, Character.transform, out var point, out var distance, out var @object )) {
                    Hit = new( point, distance, @object );
                } else {
                    Hit = null;
                }
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
        private static bool Raycast(Transform camera, Transform character, out Vector3 point, out float distance, [NotNullWhen( true )] out GameObject? @object) {
            var mask = ~0;
            var hits = Physics.RaycastAll( camera.position, camera.forward, 128, mask, QueryTriggerInteraction.Ignore );
            var hit = hits.Where( i => !(i.transform == character || i.transform.IsChildOf( character )) ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                point = hit.point;
                distance = hit.distance;
                @object = hit.transform.gameObject;
                return true;
            } else {
                point = default;
                distance = default;
                @object = null;
                return false;
            }
        }

    }
    // CharacterInputActions
    internal class CharacterInputActions : CharacterInputActionsBase {

        private readonly Player player;
        private readonly InputActions actions;

        public override bool IsEnabled => actions.asset.enabled;
        public override Vector3 Target => player.Hit?.Point ?? Camera.main.transform.TransformPoint( Vector3.forward * 128f );

        public CharacterInputActions(Player player, InputActions actions) {
            this.player = player;
            this.actions = actions;
        }

        protected override bool IsMovePressedInternal(out Vector3 moveVector) {
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
        protected override bool IsJumpPressedInternal() {
            Assert.Operation.Message( $"Method 'IsJumpPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Jump.IsPressed();
        }
        protected override bool IsCrouchPressedInternal() {
            Assert.Operation.Message( $"Method 'IsCrouchPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Crouch.IsPressed();
        }
        protected override bool IsAcceleratePressedInternal() {
            Assert.Operation.Message( $"Method 'IsAcceleratePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Accelerate.IsPressed();
        }
        protected override bool IsFirePressedInternal() {
            Assert.Operation.Message( $"Method 'IsFirePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Fire.IsPressed();
        }
        protected override bool IsAimPressedInternal() {
            Assert.Operation.Message( $"Method 'IsAimPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return actions.Game.Aim.IsPressed();
        }
        protected override bool IsInteractPressedInternal(out GameObject? interactable) {
            Assert.Operation.Message( $"Method 'IsInteractPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            interactable = player.Enemy ?? player.Loot;
            return actions.Game.Interact.WasPressedThisFrame();
        }

    }
}
