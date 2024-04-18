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

    public class Game : GameBase {
        public record Arguments(LevelEnum Level);
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
        private InputAction Fire => Actions.Game.Fire;
        private InputAction Aim => Actions.Game.Aim;
        private InputAction Interact => Actions.Game.Interact;
        private InputAction Look => Actions.Game.Look;
        private InputAction Zoom => Actions.Game.Zoom;
        private InputAction Move => Actions.Game.Move;
        private InputAction Jump => Actions.Game.Jump;
        private InputAction Crouch => Actions.Game.Crouch;
        private InputAction Accelerate => Actions.Game.Accelerate;

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
                    await Player.SpawnAsync( World.PlayerSpawnPoints.First() );
                }
            }
        }
        public void Update() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
            if (Character != null) {
                {
                    Camera.SetTarget( Character.transform, Vector3.up * 2 );
                    Camera.Rotate( Look.ReadValue<Vector2>() );
                    Camera.Zoom( Zoom.ReadValue<Vector2>().y );
                    Camera.Apply();
                }
                {
                    Character.Fire( Fire.IsPressed(), Fire.WasPressedThisFrame() );
                    Character.Aim( Aim.IsPressed(), Aim.WasPressedThisFrame() );
                    Character.Interact( Interact.IsPressed(), Interact.WasPressedThisFrame() );
                }
                {
                    Character.Move( Move.ReadValue<Vector2>().Convert( i => GetMoveVector( i, Camera.transform ) ), Move.IsPressed(), Move.WasPressedThisFrame() );
                    Character.LookAt( null, false );
                    if (Fire.IsPressed() || Aim.IsPressed() || Interact.IsPressed()) {
                        if (Character.LookTarget == null) {
                            Character.LookAt( Camera.HitPoint, Fire.WasPerformedThisFrame() || Aim.WasPerformedThisFrame() || Interact.WasPerformedThisFrame() );
                        }
                    }
                    if (Character.MoveVector != null && Character.MoveVector.Value != default) {
                        if (Character.LookTarget == null) {
                            Character.LookAt( Character.transform.position + Character.MoveVector.Value * 128f, Move.WasPressedThisFrame() );
                        }
                    }
                    Character.Jump( Jump.IsPressed(), Jump.WasPressedThisFrame() );
                    Character.Crouch( Crouch.IsPressed(), Crouch.WasPressedThisFrame() );
                    Character.Accelerate( Accelerate.IsPressed(), Accelerate.WasPressedThisFrame() );
                }
            }
        }
        public void LateUpdate() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
        }

        // Heleprs
        private static Vector3 GetMoveVector(Vector2 vector, Transform camera) {
            var result = camera.TransformDirection( vector.x, 0, vector.y );
            return new Vector3( result.x, 0, result.z ).normalized * vector.magnitude;
        }

    }
    // Level
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
}
