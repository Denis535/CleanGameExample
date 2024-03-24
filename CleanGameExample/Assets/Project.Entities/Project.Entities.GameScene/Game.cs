#nullable enable
namespace Project.Entities.GameScene {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {

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
        public void StartGame() {
            Assert.Operation.Message( $"IsPlaying {IsPlaying} must be false" ).Valid( !IsPlaying );
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
