#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Game
        private Game? Game { get; set; }
        [MemberNotNullWhen( true, "Game" )]
        public bool IsGameRunning => Game != null;
        // Game/State
        public bool IsGamePlaying {
            get {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( Game != null );
                return Game.IsPlaying;
            }
        }
        public bool IsGamePaused {
            get {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( Game != null );
                return Game.IsPaused;
            }
        }
        public bool IsGameUnPaused {
            get {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( Game != null );
                return Game.IsUnPaused;
            }
        }

        // Awake
        public new void Awake() {
            base.Awake();
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // StartGame
        public void StartGame(World world, Character character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game == null );
            Game = GameObject2.RequireAnyObjectByType<Game>( FindObjectsInactive.Exclude );
            Game.StartGame();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game != null );
            Game.StopGame();
            Game = null;
            Cursor.lockState = CursorLockMode.None;
        }

        // Pause
        public void Pause() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game != null );
            Game.Pause();
            Cursor.lockState = CursorLockMode.None;
        }
        public void UnPause() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game != null );
            Game.UnPause();
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    // World
    public enum World {
        World1,
        World2,
        World3
    }
    // Character
    public enum Character { 
        White,
        Red,
        Green,
        Blue
    }
}
