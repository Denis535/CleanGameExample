#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase, Character.IInputActions {

        // Deps
        private Game Game { get; set; } = default!;
        private Camera2 Camera { get; set; } = default!;
        private World World { get; set; } = default!;
        // IsPaused
        public bool IsPaused { get; private set; }
        // Character
        public Character? Character { get; private set; }
        // Actions
        private InputActions Actions { get; set; } = default!;
        // Hit
        public (Vector3 Point, float Distance, GameObject Object)? Hit { get; private set; }
        // Interactable
        public GameObject? Interactable {
            get {
                if (Character != null && Hit != null && Vector3.Distance( Character.transform.position, Hit.Value.Point ) <= 2 && Hit.Value.Object.IsInteractable()) {
                    return Hit?.Object;
                }
                return null;
            }
        }

        // Awake
        public void Awake() {
            Game = UnityUtils.Container.RequireDependency<Game>( null );
            Camera = UnityUtils.Container.RequireDependency<Camera2>( null );
            World = UnityUtils.Container.RequireDependency<World>( null );
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
                Character.Actions = null;
                if (!IsPaused) Actions.Disable();
            }
            Character = character;
            if (Character != null) {
                Camera.SetUp( Character.transform );
                Character.Actions = this;
                if (!IsPaused) Actions.Enable();
            }
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (Character != null) {
                Camera.SetTarget( Character.transform.position );
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
                if (Raycast( Camera.transform, Character.transform, out var point, out var distance, out var @object )) {
                    Hit = new( point, distance, @object );
                } else {
                    Hit = null;
                }
            } else {
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
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
        bool Character.IInputActions.IsInteractPressed() {
            return Actions.Game.Interact.WasPressedThisFrame();
        }
        // Character.IInputActions
        Vector3? Character.IInputActions.GetMoveVector() {
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            var vector3 = UnityEngine.Camera.main.transform.TransformDirection( vector2.x, 0, vector2.y );
            return new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
        }
        Vector3? Character.IInputActions.GetLookTarget() {
            if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128 + Vector3.up * 1.75f );
            }
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            if (vector2 != default) {
                var vector3 = UnityEngine.Camera.main.transform.TransformDirection( vector2.x, 0, vector2.y );
                vector3 = new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
                return Character!.transform.position + vector3 * 128f;
            }
            return null;
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
