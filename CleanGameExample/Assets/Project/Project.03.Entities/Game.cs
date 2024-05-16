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
        public record Args(PlayerCharacterType PlayerCharacterType, LevelType LevelType);

        // State
        [MemberNotNullWhen( true, "Player", "World" )]
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        // PlayerCharacterType
        public PlayerCharacterType PlayerCharacterType { get; private set; }
        // LevelType
        public LevelType LevelType { get; private set; }
        // Entities
        public Player Player { get; private set; } = default!;
        public World World { get; private set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            PlayerCharacterType = args.PlayerCharacterType;
            LevelType = args.LevelType;
            Player = new Player();
            World = Utils.Container.RequireDependency<World>( null );
        }
        public override void OnDestroy() {
            Player.Dispose();
        }

        // RunGame
        public void RunGame() {
            Assert.Operation.Message( $"Game must be non-running" ).Valid( !IsRunning );
            IsRunning = true;
            {
                var point = World.PlayerSpawnPoints.First();
                var camera = EntityFactory.Camera();
                var character = EntityFactory.PlayerCharacter( PlayerCharacterType, point.transform.position, point.transform.rotation );
                Player.RunGame( camera, character );
            }
            foreach (var point in World.EnemySpawnPoints) {
                EntityFactory.EnemyCharacter( point.transform.position, point.transform.rotation );
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
                Player.Update();
            }
        }
        public void LateUpdate() {
            if (IsRunning) {
                Player.LateUpdate();
            }
        }

    }
    // PlayerCharacterType
    public enum PlayerCharacterType {
        Gray,
        Red,
        Green,
        Blue
    }
    // LevelType
    public enum LevelType {
        Level1,
        Level2,
        Level3
    }
}
