#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Game : GameBase, Character.IContext {
        public record Arguments(LevelEnum Level, CharacterEnum Character);
        private readonly Lock @lock = new Lock();
        private bool isPlaying = true;

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public Camera2 Camera { get; private set; } = default!;
        public World World { get; private set; } = default!;
        // Player
        public Player Player { get; private set; } = default!;
        public Character? Character => Player.Character;
        // IsPlaying
        public bool IsPlaying {
            get => isPlaying;
            set {
                isPlaying = value;
                if (isPlaying) {
                    Actions.Enable();
                } else {
                    Actions.Disable();
                }
            }
        }
        // Actions
        private InputActions Actions { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            Camera = this.GetDependencyContainer().RequireDependency<Camera2>( null );
            World = this.GetDependencyContainer().RequireDependency<World>( null );
            Player = gameObject.RequireComponent<Player>();
            Actions = new InputActions();
            Actions.Enable();
        }
        public void OnDestroy() {
            Actions.Disable();
            Actions.Dispose();
        }

        // Start
        public async void Start() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    await Player.SpawnAsync( World.PlayerSpawnPoints.First(), Args.Character, this );
                }
            }
        }
        public void Update() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
            if (Character != null) {
                Camera.SetTarget( Character.transform, Vector3.up * 2 );
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
            }
        }
        public void LateUpdate() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
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
            var vector3 = Camera.transform.TransformDirection( vector2.x, 0, vector2.y );
            return new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
        }
        Vector3? CharacterBody.IContext.GetLookTarget(CharacterBody character) {
            if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                return Camera.HitPoint;
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
    // Level
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
    // Character
    public enum CharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
