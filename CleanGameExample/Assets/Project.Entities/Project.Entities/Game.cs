#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {
        public record Arguments(LevelEnum Level, CharacterEnum Character);

        private readonly Lock @lock = new Lock();

        // IsPaused
        public bool IsPaused { get; private set; }
        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public World World { get; private set; } = default!;
        // Player
        public Player Player { get; private set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            World = Utils.Container.RequireDependency<World>( null );
            Player = gameObject.AddComponent<Player>();
        }
        public void OnDestroy() {
            Spawner.ReleaseAll();
        }

        // SetPaused
        public void SetPaused(bool value) {
            IsPaused = value;
            Player.SetPaused( value );
        }

        // Start
        public async void Start() {
            Player.SetCharacter( Spawner.SpawnPlayerCharacter( World.PlayerSpawnPoints.First(), Args.Character ) );
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    var tasks = new List<Task>();
                    foreach (var enemySpawnPoint in World.EnemySpawnPoints) {
                        var task = Spawner.SpawnEnemyCharacterAsync( enemySpawnPoint, destroyCancellationToken ).AsTask();
                        tasks.Add( task );
                    }
                    foreach (var lootSpawnPoint in World.LootSpawnPoints) {
                        var task = Spawner.SpawnLootAsync( lootSpawnPoint, destroyCancellationToken ).AsTask();
                        tasks.Add( task );
                    }
                    await Task.WhenAll( tasks );
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
    // Level
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
    // Character
    public enum CharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
