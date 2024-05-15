#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {
        private readonly Lock @lock = new Lock();

        // State
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        // Character
        public PlayerCharacterEnum? Character { get; private set; }
        // Level
        public LevelEnum? Level { get; private set; }
        // Entities
        public Player Player { get; private set; } = default!;
        public World World { get; private set; } = default!;

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // RunGame
        public void RunGame(PlayerCharacterEnum character, LevelEnum level) {
            Assert.Operation.Message( $"Game must be non-running" ).Valid( !IsRunning );
            IsRunning = true;
            Character = character;
            Level = level;
            Player = gameObject.AddComponent<Player>();
            World = Utils.Container.RequireDependency<World>( null );
            Player.SetCharacter( Spawner.SpawnPlayerCharacter( character, World.PlayerSpawnPoints.First() ) );
            foreach (var point in World.EnemySpawnPoints) {
                Spawner.SpawnEnemyCharacter( point );
            }
            foreach (var point in World.LootSpawnPoints) {
                Spawner.SpawnWeapon( point );
            }
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be running" ).Valid( IsRunning );
            IsRunning = false;
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
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
        }
        public void Update() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
        }

    }
}
