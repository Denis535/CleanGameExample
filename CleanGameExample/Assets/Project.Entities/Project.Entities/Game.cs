#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {

        private bool isPlaying = true;

        // System
        private bool IsInitialized { get; set; }
        // IsPlaying
        public bool IsPlaying {
            get => isPlaying;
            set {
                isPlaying = value;
            }
        }
        // Player
        public Player? Player { get; private set; } = default!;

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // Initialize
        public void Initialize() {
            Assert.Object.Message( $"Game {this} must be awakened" ).Initialized( didAwake );
            Player = gameObject.AddComponent<Player>();
            Player.Initialize();
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
        }
        public void Update() {
            Assert.Operation.Message( $"Game {this} must be initialized" ).Valid( IsInitialized );
        }

    }
}
