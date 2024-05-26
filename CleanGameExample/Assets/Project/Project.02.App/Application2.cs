#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using Project.Entities.Characters;
    using Project.Entities.Things;
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
        public void RunGame(LevelEnum level, string name, PlayerCharacterEnum character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            EntityFactory.Initialize();
            CharacterFactory.Initialize();
            ThingFactory.Initialize();
            Game = EntityFactory.Game( level, name, character );
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            GameObject.DestroyImmediate( Game );
            Game = null;
            EntityFactory.Deinitialize();
            CharacterFactory.Deinitialize();
            ThingFactory.Deinitialize();
            Array.Clear( Physics2.RaycastHitBuffer, 0, Physics2.RaycastHitBuffer.Length );
            Array.Clear( Physics2.ColliderBuffer, 0, Physics2.ColliderBuffer.Length );
        }

    }
}
