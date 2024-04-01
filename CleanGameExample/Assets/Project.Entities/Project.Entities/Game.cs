#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {

        private bool isPlaying = true;

        // System
        private bool IsInitialized { get; set; }
        public bool IsPlaying {
            get {
                return isPlaying;
            }
            set {
                isPlaying = value;
            }
        }
        // Globals
        private World World { get; set; } = default!;
        // Player
        public Player? Player { get; private set; } = default!;

        // Awake
        public void Awake() {
            World = GameObject2.RequireAnyObjectByType<World>( FindObjectsInactive.Exclude );
        }
        public void OnDestroy() {
        }

        // Initialize
        public void Initialize() {
            Assert.Object.Message( $"Game {this} must be awakened" ).Initialized( didAwake );
            Assert.Operation.Message( $"Game {this} must not be initialized" ).Valid( !IsInitialized );
            IsInitialized = true;
        }
        public void Deinitialize() {
            Assert.Object.Message( $"Game {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
        }

        // CreatePlayer
        public T CreatePlayer<T>() where T : Player {
            Assert.Object.Message( $"Game {this} must be awakened" ).Initialized( didAwake );
            Assert.Object.Message( $"Game {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
            Assert.Operation.Message( $"Player must be null" ).Valid( Player == null );
            Player = gameObject.AddComponent<T>();
            return (T) Player;
        }
        public void DestroyPlayer() {
            Assert.Operation.Message( $"Player must be non-null" ).Valid( Player != null );
            DestroyImmediate( Player );
        }

        // Start
        public void Start() {
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
        }
        public void Update() {
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
        }

    }
}
