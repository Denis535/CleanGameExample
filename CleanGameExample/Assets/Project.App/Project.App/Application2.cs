#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Project.Entities;
    using Project.Entities.Environment;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Game
        private Game? Game { get; set; }
        [MemberNotNullWhen( true, "Game" )]
        public bool IsGameRunning => Game is not null;
        // Game/State
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
            switch (level) {
                case Level.Level1: {
                    Game = new GameObject( "Game" ).AddComponent<Game>();
                    var player = Game.gameObject.AddComponent<Player>();
                    var world = GameObject2.RequireAnyObjectByType<World>( FindObjectsInactive.Exclude );
                    Game.StartGame( player, world );
                    break;
                }
                case Level.Level2: {
                    Game = new GameObject( "Game" ).AddComponent<Game>();
                    var player = Game.gameObject.AddComponent<Player>();
                    var world = GameObject2.RequireAnyObjectByType<World>( FindObjectsInactive.Exclude );
                    Game.StartGame( player, world );
                    break;
                }
                case Level.Level3: {
                    Game = new GameObject( "Game" ).AddComponent<Game>();
                    var player = Game.gameObject.AddComponent<Player>();
                    var world = GameObject2.RequireAnyObjectByType<World>( FindObjectsInactive.Exclude );
                    Game.StartGame( player, world );
                    break;
                }
                default:
                    throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.StopGame();
            Addressables2.ReleaseInstance( Game.gameObject );
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
