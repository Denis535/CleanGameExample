#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Project.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {

        // State
        [MemberNotNullWhen( true, "Player", "World" )]
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        // PlayerCharacterType
        public PlayerCharacterType? PlayerCharacterType { get; private set; }
        // LevelType
        public LevelType? LevelType { get; private set; }
        // Entities
        public Player? Player { get; private set; }
        public World? World { get; private set; }

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // RunGame
        public void RunGame(PlayerCharacterType playerCharacterType, LevelType levelType) {
            Assert.Operation.Message( $"Game must be non-running" ).Valid( !IsRunning );
            IsRunning = true;
            PlayerCharacterType = playerCharacterType;
            LevelType = levelType;
            Player = gameObject.AddComponent<Player>();
            World = Utils.Container.RequireDependency<World>( null );
            {
                var point = World.PlayerSpawnPoints.First();
                var camera = EntityFactory.Camera();
                var character = EntityFactory2.PlayerCharacter( playerCharacterType, point.transform.position, point.transform.rotation );
                Player.RunGame( camera, character );
            }
            foreach (var point in World.EnemySpawnPoints) {
                EntityFactory2.EnemyCharacter( point.transform.position, point.transform.rotation );
            }
            foreach (var point in World.LootSpawnPoints) {
                EntityFactory2.Gun( point.transform.position, point.transform.rotation );
            }
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be running" ).Valid( IsRunning );
            IsRunning = false;
            Player.StopGame();
        }

        // Pause
        public void Pause() {
            Assert.Operation.Message( $"Game must be running" ).Valid( IsRunning );
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            Player.Pause();
        }
        public void UnPause() {
            Assert.Operation.Message( $"Game must be running" ).Valid( IsRunning );
            Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
            IsPaused = false;
            Player.UnPause();
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (IsRunning) {
            }
        }

    }
}
