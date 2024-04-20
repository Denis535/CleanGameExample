#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase, Character.IContext {
        public interface IContext {
        }
        public record Arguments(IContext Context);

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public Camera2 Camera { get; private set; } = default!;
        // Instances
        private List<InstanceHandle<Character>> Instances { get; } = new List<InstanceHandle<Character>>();
        // Actions
        private InputActions Actions { get; set; } = default!;
        // Character
        public Character? Character { get; set; }

        // Awake
        public void Awake() {
            Args = Context.Get<Player, Arguments>();
            Camera = this.GetDependencyContainer().RequireDependency<Camera2>( null );
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
                Camera.SetTarget( Character.transform, Vector3.up * 2 );
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
            } else {
                Camera.SetTarget( Vector3.up * 1024 );
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
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

        // SpawnAsync
        public async ValueTask SpawnAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            Character = await Instances.SpawnPlayerCharacterAsync( point, character, this, cancellationToken );
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
            var vector3 = Camera.transform.TransformDirection( vector2.x, 0, vector2.y );
            return new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
        }
        Vector3? CharacterBody.IContext.GetLookTarget(CharacterBody character) {
            if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                return Camera.Hit?.Point ?? Camera.transform.TransformPoint( Vector3.forward * 128 + Vector3.up * 1.75f );
            }
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            if (vector2 != default) {
                var vector3 = Camera.transform.TransformDirection( vector2.x, 0, vector2.y );
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

    }
}
