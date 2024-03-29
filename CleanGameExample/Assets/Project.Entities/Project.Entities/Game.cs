#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Environment;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {

        // Globals
        private World? World { get; set; }
        private Player? Player { get; set; }
        // State
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsUnPaused => !IsPaused;

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

        // StartGame
        public void StartGame(World world, Player player) {
            Assert.Operation.Message( $"IsPlaying {IsPlaying} must be false" ).Valid( !IsPlaying );
            World = world;
            Player = player;
            IsPlaying = true;
        }
        public void StopGame() {
            Assert.Operation.Message( $"IsPlaying {IsPlaying} must be true" ).Valid( IsPlaying );
            IsPlaying = false;
        }

        // Pause
        public void Pause() {
            Assert.Operation.Message( $"IsPlaying {IsPlaying} must be true" ).Valid( IsPlaying );
            Assert.Operation.Message( $"IsPaused {IsPaused} must be false" ).Valid( !IsPaused );
            IsPaused = true;
        }
        public void UnPause() {
            Assert.Operation.Message( $"IsPlaying {IsPlaying} must be true" ).Valid( IsPlaying );
            Assert.Operation.Message( $"IsPaused {IsPaused} must be true" ).Valid( IsPaused );
            IsPaused = false;
        }

    }
}
