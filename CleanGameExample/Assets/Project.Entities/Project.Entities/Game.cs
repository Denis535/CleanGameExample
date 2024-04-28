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
        public record Arguments(LevelEnum Level, PlayerCharacterEnum Character);
        private readonly Lock @lock = new Lock();

        // IsPaused
        public bool IsPaused { get; private set; }
        // Args
        private Arguments Args { get; set; } = default!;
        // Objects
        public Player Player { get; private set; } = default!;
        public World World { get; private set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            Player = gameObject.AddComponent<Player>();
            World = Utils.Container.RequireDependency<World>( null );
        }
        public void OnDestroy() {
        }

        // SetPaused
        public void SetPaused(bool value) {
            IsPaused = value;
            Player.SetPaused( value );
        }

        // Start
        public async void Start() {
            Player.SetCharacter( EntitySpawner.SpawnPlayerCharacter( World.PlayerSpawnPoints.First(), Args.Character ) );
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    var tasks = new List<Task>();
                    foreach (var point in World.EnemySpawnPoints) {
                        var task = EntitySpawner.SpawnEnemyCharacterAsync( point, destroyCancellationToken ).AsTask();
                        tasks.Add( task );
                    }
                    foreach (var point in World.LootSpawnPoints) {
                        var task = EntitySpawner.SpawnLootAsync( point, destroyCancellationToken ).AsTask();
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
    public enum PlayerCharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
