#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {
        private readonly Lock @lock = new Lock();

        // State
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        // Level
        public LevelEnum? Level { get; private set; }
        // Character
        public PlayerCharacterEnum? Character { get; private set; }
        // Entities
        public World World { get; private set; } = default!;
        public Player Player { get; private set; } = default!;

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // RunGame
        public async void RunGame(LevelEnum level, PlayerCharacterEnum character) {
            Assert.Operation.Message( $"Game must be non-running" ).Valid( !IsRunning );
            IsRunning = true;
            Level = level;
            Character = character;
            World = Utils.Container.RequireDependency<World>( null );
            Player = gameObject.AddComponent<Player>();
            using (@lock.Enter()) {
                Player.SetCharacter( EntitySpawner.SpawnPlayerCharacter( World.PlayerSpawnPoints.First(), GetPlayerCharacter( character ) ) );
                var tasks = new List<Task>();
                foreach (var point in World.EnemySpawnPoints) {
                    tasks.Add( EntitySpawner.SpawnEnemyCharacterAsync( point, destroyCancellationToken ).AsTask() );
                }
                foreach (var point in World.LootSpawnPoints) {
                    tasks.Add( EntitySpawner.SpawnLootAsync( point, destroyCancellationToken ).AsTask() );
                }
                await Task.WhenAll( tasks );
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

        // Helpers
        private static string GetPlayerCharacter(PlayerCharacterEnum character) {
            switch (character) {
                case PlayerCharacterEnum.Gray: return R.Project.Entities.Characters.PlayerCharacter_Gray_Value;
                case PlayerCharacterEnum.Red: return R.Project.Entities.Characters.PlayerCharacter_Red_Value;
                case PlayerCharacterEnum.Green: return R.Project.Entities.Characters.PlayerCharacter_Green_Value;
                case PlayerCharacterEnum.Blue: return R.Project.Entities.Characters.PlayerCharacter_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }

    }
    // Level
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
    // Character
    public enum PlayerCharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
