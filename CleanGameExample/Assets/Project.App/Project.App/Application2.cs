#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Globals
        public Game? Game { get; private set; }

        // Awake
        public new void Awake() {
            base.Awake();
            Game = null;
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // RunGame
        public void RunGame(Level level, Character character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Game = this.GetDependencyContainer().Resolve<Game>( null );
            Game.Initialize( level, character );
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Assert.Operation.Message( $"Player must be non-null" ).Valid( Game.Player is not null );
            Game.Deinitialize();
            Game = null;
        }

    }
}
