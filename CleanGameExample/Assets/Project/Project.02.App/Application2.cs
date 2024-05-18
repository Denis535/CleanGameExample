﻿#nullable enable
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
        public void RunGame(PlayerCharacterEnum character, LevelEnum level) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Game = EntityFactory2.Game( character, level );
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            GameObject.DestroyImmediate( Game );
            Game = null;
        }

    }
}