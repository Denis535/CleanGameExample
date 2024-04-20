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

    public class Game : GameBase, Player.IContext {
        public record Arguments(LevelEnum Level, CharacterEnum Character);
        private readonly Lock @lock = new Lock();

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public World World { get; private set; } = default!;
        // Instances
        private List<InstanceHandle<Transform>> Instances { get; } = new List<InstanceHandle<Transform>>();
        // IsPlaying
        public bool IsPlaying { get; private set; }
        // Player
        public Player Player { get; private set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            World = this.GetDependencyContainer().RequireDependency<World>( null );
            using (Context.Begin<Player, Player.Arguments>( new Player.Arguments( this ) )) {
                Player = gameObject.AddComponent<Player>();
            }
        }
        public void OnDestroy() {
            foreach (var instance in Instances) {
                instance.ReleaseSafe();
            }
        }

        // Start
        public async void Start() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    var tasks = new List<Task>();
                    {
                        tasks.Add( Player.SpawnAsync( World.PlayerSpawnPoints.First(), Args.Character, destroyCancellationToken ).AsTask() );
                    }
                    foreach (var enemySpawnPoint in World.EnemySpawnPoints) {
                        tasks.Add( Instances.SpawnEnemyCharacterAsync( enemySpawnPoint, destroyCancellationToken ).AsTask() );
                    }
                    foreach (var lootSpawnPoint in World.LootSpawnPoints) {
                        tasks.Add( Instances.SpawnLootAsync( lootSpawnPoint, destroyCancellationToken ).AsTask() );
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
