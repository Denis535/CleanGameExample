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
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase, Character.IInputActions {

        // Deps
        private Game Game { get; set; } = default!;
        private Camera2 Camera { get; set; } = default!;
        private World World { get; set; } = default!;
        // IsPlaying
        public bool IsPlaying { get; private set; } = true;
        // Character
        public Character? Character { get; private set; }
        // Actions
        private InputActions Actions { get; set; } = default!;
        // Hit
        public (Vector3 Point, float Distance, GameObject Object)? Hit { get; private set; }

        // Awake
        public void Awake() {
            Game = IDependencyContainer.Instance.RequireDependency<Game>( null );
            Camera = IDependencyContainer.Instance.RequireDependency<Camera2>( null );
            World = IDependencyContainer.Instance.RequireDependency<World>( null );
            Actions = new InputActions();
        }
        public void OnDestroy() {
            Actions.Dispose();
        }

        // SetPlaying
        public void SetPlaying(bool value) {
            IsPlaying = value;
            if (value) {
                if (Character != null) Actions.Enable();
            } else {
                if (Character != null) Actions.Disable();
            }
        }

        // SetCharacter
        public void SetCharacter(Character? character) {
            if (Character != null) {
                if (IsPlaying) Actions.Disable();
                Character.Actions = null;
            }
            Character = character;
            if (Character != null) {
                Camera.SetUp( Character.transform );
                Character.Actions = this;
                if (IsPlaying) Actions.Enable();
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
                if (Raycast( Camera.transform, out var point, out var distance, out var @object )) {
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
        bool Character.IInputActions.IsFirePressed() {
            return Actions.Game.Fire.IsPressed();
        }
        bool Character.IInputActions.IsAimPressed() {
            return Actions.Game.Aim.IsPressed();
        }
        bool Character.IInputActions.IsInteractPressed() {
            return Actions.Game.Interact.WasPressedThisFrame();
        }
        // CharacterBody.IInputActions
        Vector3? CharacterBody.IInputActions.GetMoveVector() {
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            var vector3 = UnityEngine.Camera.main.transform.TransformDirection( vector2.x, 0, vector2.y );
            return new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
        }
        Vector3? CharacterBody.IInputActions.GetLookTarget() {
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
        bool CharacterBody.IInputActions.IsJumpPressed(out float duration) {
            duration = 0;
            return Actions.Game.Jump.IsPressed();
        }
        bool CharacterBody.IInputActions.IsCrouchPressed() {
            return Actions.Game.Crouch.IsPressed();
        }
        bool CharacterBody.IInputActions.IsAcceleratePressed() {
            return Actions.Game.Accelerate.IsPressed();
        }

        // Heleprs
        private static bool Raycast(Transform transform, out Vector3 point, out float distance, [NotNullWhen( true )] out GameObject? @object) {
            var mask = ~0;
            var hits = Physics.RaycastAll( transform.position, transform.forward, 128, mask, QueryTriggerInteraction.Ignore );
            var hit = hits.SkipWhile( i => i.transform == transform ).FirstOrDefault();
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
