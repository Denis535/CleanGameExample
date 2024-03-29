#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Project.Entities;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Game
        private Game? Game { get; set; }
        [MemberNotNullWhen( true, "Game" )] public bool IsGameRunning => Game is not null;
        public bool IsGamePlaying {
            get {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
                return Game.IsPlaying;
            }
        }
        public bool IsGamePaused {
            get {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
                return Game.IsPaused;
            }
        }
        public bool IsGameUnPaused {
            get {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
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
        public void StartGame(Level level, Character character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Cursor.lockState = CursorLockMode.Locked;
            Game = GetGame( level );
            Game.StartGame( GetWorld(), GetPlayer( Game, character ) );
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.StopGame();
            Game = null;
            Cursor.lockState = CursorLockMode.None;
        }

        // Pause
        public void Pause() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.Pause();
            Cursor.lockState = CursorLockMode.None;
        }
        public void UnPause() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.UnPause();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Helpers
        private static Game GetGame(Level level) {
            switch (level) {
                case Level.Level1: {
                    var gameObject = new GameObject( "Game" );
                    return gameObject.AddComponent<Game>();
                }
                case Level.Level2: {
                    var gameObject = new GameObject( "Game" );
                    return gameObject.AddComponent<Game>();
                }
                case Level.Level3: {
                    var gameObject = new GameObject( "Game" );
                    return gameObject.AddComponent<Game>();
                }
                default:
                    throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }
        private static World GetWorld() {
            return GameObject2.RequireAnyObjectByType<World>( FindObjectsInactive.Exclude );
        }
        private static Player GetPlayer(Game game, Character character) {
            switch (character) {
                case Character.White:
                    return game.gameObject.AddComponent<Player>();
                case Character.Red:
                    return game.gameObject.AddComponent<Player>();
                case Character.Green:
                    return game.gameObject.AddComponent<Player>();
                case Character.Blue:
                    return game.gameObject.AddComponent<Player>();
                default:
                    throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
    // Character
    public enum Character {
        White,
        Red,
        Green,
        Blue
    }
}
