#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Entities
        public Game? Game { get; private set; }

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // RunGame
        public void RunGame(LevelEnum level, PlayerCharacterEnum character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Game = Utils.Container.RequireDependency<Game>( null );
            Game.RunGame( level, character );
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.StopGame();
            Game = null;
        }

    }
}
