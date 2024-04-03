#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {
        //public struct Arguments {
        //    public Level Level { get; init; }
        //}

        private bool isPlaying = true;

        // System
        private bool IsInitialized { get; set; }
        public bool IsPlaying {
            get => isPlaying;
            set {
                isPlaying = value;
            }
        }
        // Globals
        public World World { get; private set; } = default!;
        public Camera2 Camera { get; private set; } = default!;
        // Player
        public Player? Player { get; private set; } = default!;

        // Awake
        public void Awake() {
            World = this.GetDependencyContainer().Resolve<World>( null );
            Camera = this.GetDependencyContainer().Resolve<Camera2>( null );
        }
        public void OnDestroy() {
        }

        // Initialize
        public void Initialize(Level level, Character character) {
            Assert.Object.Message( $"Game {this} must be awakened" ).Initialized( didAwake );
            Player = gameObject.AddComponent<Player>();
            Player.Initialize( character );
            IsInitialized = true;
        }
        public void Deinitialize() {
            Assert.Object.Message( $"Game {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
            Player!.Deinitialize();
            DestroyImmediate( Player );
            Player = null;
        }

        // Start
        public void Start() {
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
            var startPoint = World.PlayerStarts.FirstOrDefault();
        }
        public void Update() {
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
        }

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
}
