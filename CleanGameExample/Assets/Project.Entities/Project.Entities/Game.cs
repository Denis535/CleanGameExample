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
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {
        public record Arguments(LevelEnum Level, CharacterEnum Character);

        private readonly Lock @lock = new Lock();
        private readonly List<InstanceHandle> instances = new List<InstanceHandle>();

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public World World { get; private set; } = default!;
        // IsPlaying
        public bool IsPlaying { get; private set; }
        // Player
        public Player Player { get; private set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            World = this.GetDependencyContainer().RequireDependency<World>( null );
            using (Context.Begin<Player, Player.Arguments>( new Player.Arguments() )) {
                Player = gameObject.AddComponent<Player>();
            }
        }
        public void OnDestroy() {
            foreach (var instance in instances) {
                instance.ReleaseSafe();
            }
        }

        // Start
        public async void Start() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    var tasks = new List<Task>();
                    foreach (var enemySpawnPoint in World.EnemySpawnPoints) {
                        var task = instances.SpawnEnemyCharacterAsync( enemySpawnPoint, destroyCancellationToken ).AsTask();
                        tasks.Add( task );
                    }
                    foreach (var lootSpawnPoint in World.LootSpawnPoints) {
                        var task = instances.SpawnLootAsync( lootSpawnPoint, destroyCancellationToken ).AsTask();
                        tasks.Add( task );
                    }
                    await Task.WhenAll( tasks );
                    {
                        var character = await instances.SpawnPlayerCharacterAsync( World.PlayerSpawnPoints.First(), Args.Character, Player, destroyCancellationToken ).AsTask();
                        Player.SetCharacter( character );
                    }
                }
            }
        }
        public void Update() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
        }

        // SetPlaying
        public void SetPlaying(bool value) {
            IsPlaying = value;
            Player.SetPlaying( value );
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
