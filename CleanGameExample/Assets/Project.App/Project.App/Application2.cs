#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.App;
    using UnityEngine.ResourceManagement.AsyncOperations;

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
        public void StartGame(World world, Character character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Cursor.lockState = CursorLockMode.Locked;
            switch (world) {
                case World.World1:
                    var gameObject = Addressables2.InstantiateAsync( R.Project.Entities.Game ).GetResult( null, null );
                    Game = gameObject.RequireComponent<Game>();
                    Game.StartGame();
                    break;
                case World.World2:
                    gameObject = Addressables2.InstantiateAsync( R.Project.Entities.Game ).GetResult( null, null );
                    Game = gameObject.RequireComponent<Game>();
                    Game.StartGame();
                    break;
                case World.World3:
                    gameObject = Addressables2.InstantiateAsync( R.Project.Entities.Game ).GetResult( null, null );
                    Game = gameObject.RequireComponent<Game>();
                    Game.StartGame();
                    break;
                default:
                    throw Exceptions.Internal.NotSupported( $"World {world} is not supported" );
            }
            switch (character) {
                case Character.White:
                    break;
                case Character.Red:
                    break;
                case Character.Green:
                    break;
                case Character.Blue:
                    break;
                default:
                    throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
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
