#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Game
        public Game? Game { get; private set; }

        // Awake
        public new void Awake() {
            base.Awake();
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // RunGame
        public void RunGame(Level level, Character character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Game = CreateGame( level );
            Game.Initialize();
            Game.CreatePlayer<Player>().Initialize();
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Assert.Operation.Message( $"Player must be non-null" ).Valid( Game.Player is not null );
            Game.Player.Deinitialize();
            Game.DestroyPlayer();
            Game.Deinitialize();
            Game = null;
        }

        // Helpers
        private static Game CreateGame(Level level) {
            var gameObject = new GameObject( "Game" );
            switch (level) {
                case Level.Level1: {
                    return gameObject.AddComponent<Game>();
                }
                case Level.Level2: {
                    return gameObject.AddComponent<Game>();
                }
                case Level.Level3: {
                    return gameObject.AddComponent<Game>();
                }
                default:
                    throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }
        //private static Player CreatePlayer(Game game, Character character) {
        //    switch (character) {
        //        case Character.White: {
        //            return game.CreatePlayer<Player>();
        //        }
        //        case Character.Red: {
        //            return game.CreatePlayer<Player>();
        //        }
        //        case Character.Green: {
        //            return game.CreatePlayer<Player>();
        //        }
        //        case Character.Blue: {
        //            return game.CreatePlayer<Player>();
        //        }
        //        default:
        //            throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
        //    }
        //}

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
