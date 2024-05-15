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

        private readonly Lock @lock = new Lock();

        // State
        [MemberNotNullWhen( true, "Player", "World" )]
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        // Character
        public PlayerCharacterEnum? Character { get; private set; }
        // Level
        public LevelEnum? Level { get; private set; }
        // Entities
        public Player? Player { get; private set; }
        public World? World { get; private set; }

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
            {
                var point = World.PlayerSpawnPoints.First();
                Player.SetCharacter( EntityFactory2.PlayerCharacter( character, point.transform.position, point.transform.rotation ) );
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
