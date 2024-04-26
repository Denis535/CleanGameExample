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

    public class Player : PlayerBase, Character.IInputActions {

        // IsPaused
        public bool IsPaused { get; private set; }
        // Deps
        private Game Game { get; set; } = default!;
        private Camera2 Camera { get; set; } = default!;
        private World World { get; set; } = default!;
        // Character
        public Character? Character { get; private set; }
        // Actions
        private InputActions Actions { get; set; } = default!;
        // Hit
        public (Vector3 Point, float Distance, GameObject Object)? Hit { get; private set; }
        // Interactable
        public GameObject? Interactable {
            get {
                if (Character != null && Hit != null && Vector3.Distance( Character.transform.position, Hit.Value.Point ) <= 2.5f && Hit.Value.Object.IsInteractable()) {
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
        public void SetPaused(bool value) {
            IsPaused = value;
            if (IsPaused) {
                if (Character != null) Actions.Disable();
            } else {
                if (Character != null) Actions.Enable();
            }
        }

        // SetCharacter
        public void SetCharacter(Character? character) {
            if (Character != null) {
                Camera.SetTarget( null );
                Character.Actions = null;
                if (!IsPaused) Actions.Disable();
            }
            Character = character;
            if (Character != null) {
                Camera.SetTarget( Character );
                Character.Actions = this;
                if (!IsPaused) Actions.Enable();
            }
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (Character != null) {
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
                if (Raycast( Camera.transform, Character.transform, out var point, out var distance, out var @object )) {
                    Hit = new( point, distance, @object );
                } else {
                    Hit = null;
                }
            } else {
                Hit = null;
            }
        }

        // OnDrawGizmos
        public void OnDrawGizmos() {
            if (Hit != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere( Hit.Value.Point, 0.1f );
            }
        }

        // Character.IInputActions
        bool Character.IInputActions.IsEnabled() {
            return Actions.asset.enabled;
        }
        // Character.IInputActions
        bool Character.IInputActions.IsFirePressed() {
            return Actions.Game.Fire.IsPressed();
        }
        bool Character.IInputActions.IsAimPressed() {
            return Actions.Game.Aim.IsPressed();
        }
        bool Character.IInputActions.IsInteractPressed(out GameObject? interactable) {
            interactable = Interactable;
            return Actions.Game.Interact.WasPressedThisFrame();
        }
        // Character.IInputActions
        bool Character.IInputActions.IsMovePressed(out Vector3 moveVector) {
            if (Actions.Game.Move.IsPressed()) {
                var vector = (Vector3) Actions.Game.Move.ReadValue<Vector2>();
                moveVector = UnityEngine.Camera.main.transform.TransformDirection( vector.x, 0, vector.y ).Convert( i => new Vector3( i.x, 0, i.z ).normalized * vector.magnitude );
                return true;
            } else {
                moveVector = Vector3.zero;
                return false;
            }
        }
        bool Character.IInputActions.IsLookPressed(out Vector3 lookTarget) {
            if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                lookTarget = Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( new Vector3( 0, 1, 128f ) );
                return true;
            }
            if (Actions.Game.Move.IsPressed()) {
                var vector = (Vector3) Actions.Game.Move.ReadValue<Vector2>();
                var moveVector = UnityEngine.Camera.main.transform.TransformDirection( vector.x, 0, vector.y ).Convert( i => new Vector3( i.x, 0, i.z ).normalized * vector.magnitude );
                lookTarget = Character!.transform.position + moveVector * 128f + Vector3.up * 1.75f;
                return true;
            }
            lookTarget = Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( new Vector3( 0, 1, 128f ) );
            return false;
        }
        bool Character.IInputActions.IsJumpPressed() {
            return Actions.Game.Jump.IsPressed();
        }
        bool Character.IInputActions.IsCrouchPressed() {
            return Actions.Game.Crouch.IsPressed();
        }
        bool Character.IInputActions.IsAcceleratePressed() {
            return Actions.Game.Accelerate.IsPressed();
        }

        // Heleprs
        private static bool Raycast(Transform camera, Transform character, out Vector3 point, out float distance, [NotNullWhen( true )] out GameObject? @object) {
            var mask = ~0;
            var hits = Physics.RaycastAll( camera.position, camera.forward, 128, mask, QueryTriggerInteraction.Ignore );
            var hit = hits.Where( i => i.transform != character ).OrderBy( i => i.distance ).FirstOrDefault();
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
}
