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

    public class Player : PlayerBase, Character.IContext, CharacterBody.IContext {
        public record Arguments();

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        private Game Game { get; set; } = default!;
        private Camera2 Camera { get; set; } = default!;
        private World World { get; set; } = default!;
        // Actions
        private InputActions Actions { get; set; } = default!;
        // Character
        public Character? Character { get; private set; }
        // Hit
        public (Vector3 Point, float Distance, GameObject Object)? Hit { get; private set; }

        // Awake
        public void Awake() {
            Args = Context.Get<Player, Arguments>();
            Game = IDependencyContainer.Instance.RequireDependency<Game>( null );
            Camera = IDependencyContainer.Instance.RequireDependency<Camera2>( null );
            World = IDependencyContainer.Instance.RequireDependency<World>( null );
            Actions = new InputActions();
            Actions.Enable();
        }
        public void OnDestroy() {
            Actions.Disable();
            Actions.Dispose();
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

        // SetPlaying
        public void SetPlaying(bool value) {
            if (value) {
                Actions.Enable();
            } else {
                Actions.Disable();
            }
        }

        // SetCharacter
        public void SetCharacter(Character? character) {
            Character = character;
            if (Character != null) {
                Camera.SetUp( Character.transform );
            }
        }

        // Character.IContext
        bool Character.IContext.IsFirePressed() {
            return Actions.Game.Fire.IsPressed();
        }
        bool Character.IContext.IsAimPressed() {
            return Actions.Game.Aim.IsPressed();
        }
        bool Character.IContext.IsInteractPressed() {
            return Actions.Game.Interact.WasPressedThisFrame();
        }
        // CharacterBody.IContext
        Vector3? CharacterBody.IContext.GetMoveVector(CharacterBody character) {
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            var vector3 = UnityEngine.Camera.main.transform.TransformDirection( vector2.x, 0, vector2.y );
            return new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
        }
        Vector3? CharacterBody.IContext.GetLookTarget(CharacterBody character) {
            if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128 + Vector3.up * 1.75f );
            }
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            if (vector2 != default) {
                var vector3 = UnityEngine.Camera.main.transform.TransformDirection( vector2.x, 0, vector2.y );
                vector3 = new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
                return character.transform.position + vector3 * 128f;
            }
            return null;
        }
        bool CharacterBody.IContext.IsJumpPressed(CharacterBody character, out float duration) {
            duration = 0;
            return Actions.Game.Jump.IsPressed();
        }
        bool CharacterBody.IContext.IsCrouchPressed(CharacterBody character) {
            return Actions.Game.Crouch.IsPressed();
        }
        bool CharacterBody.IContext.IsAcceleratePressed(CharacterBody character) {
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
