#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase, Player.IContext {
        public record Arguments(LevelEnum Level, CharacterEnum Character);
        private readonly Lock @lock = new Lock();
        private bool isPlaying = true;

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public World World { get; private set; } = default!;
        // Player
        public Player Player { get; private set; } = default!;
        // IsPlaying
        public bool IsPlaying {
            get => isPlaying;
            set {
                isPlaying = value;
                Player.enabled = value;
            }
        }
        // Instances
        private List<InstanceHandle<Character>> Characters { get; } = new List<InstanceHandle<Character>>();
        private List<InstanceHandle<Transform>> Enemies { get; } = new List<InstanceHandle<Transform>>();
        private List<InstanceHandle<Transform>> Loots { get; } = new List<InstanceHandle<Transform>>();

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            World = this.GetDependencyContainer().RequireDependency<World>( null );
            using (Context.Begin<Player, Player.Arguments>( new Player.Arguments( this ) )) {
                Player = gameObject.AddComponent<Player>();
            }
        }
        public void OnDestroy() {
            DestroyImmediate( Player );
            foreach (var instance in Characters) {
                instance.ReleaseSafe();
            }
            foreach (var instance in Enemies) {
                instance.ReleaseSafe();
            }
            foreach (var instance in Loots) {
                instance.ReleaseSafe();
            }
        }

        // Start
        public async void Start() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    var tasks = new List<Task>();
                    {
                        tasks.Add( Player.SpawnCharacterAsync( World.PlayerSpawnPoints.First(), Args.Character, destroyCancellationToken ).AsTask() );
                    }
                    foreach (var enemySpawnPoint in World.EnemySpawnPoints) {
                        tasks.Add( SpawnEnemyAsync( enemySpawnPoint, destroyCancellationToken ).AsTask() );
                    }
                    foreach (var lootSpawnPoint in World.LootSpawnPoints) {
                        tasks.Add( SpawnLootAsync( lootSpawnPoint, destroyCancellationToken ).AsTask() );
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
        public void LateUpdate() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
        }

        // Player.IContext
        ValueTask<Character> Player.IContext.SpawnCharacterAsync(PlayerSpawnPoint point, Player player, CharacterEnum character, CancellationToken cancellationToken) {
            return SpawnCharacterAsync( point, player, character, cancellationToken );
        }

        // SpawnCharacterAsync
        private async ValueTask<Character> SpawnCharacterAsync(PlayerSpawnPoint point, Player player, CharacterEnum character, CancellationToken cancellationToken) {
            using (Context.Begin<Character, Character.Arguments>( new Character.Arguments( player ) )) {
                using (Context.Begin<CharacterBody, CharacterBody.Arguments>( new CharacterBody.Arguments( player ) )) {
                    var instance = new InstanceHandle<Character>( GetCharacterAddress( character ) );
                    Characters.Add( instance );
                    return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, transform, cancellationToken );
                }
            }
        }
        // SpawnEnemyAsync
        private async ValueTask<Transform> SpawnEnemyAsync(EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( R.Project.Entities.Characters.Secondary.EnemyCharacter_Gray_Value );
            Enemies.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, transform, cancellationToken );
        }
        // SpawnLootAsync
        private async ValueTask<Transform> SpawnLootAsync(LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( R.Project.Entities.Characters.Inventory.Gun_Gray_Value );
            Loots.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, transform, cancellationToken );
        }

        // Heleprs
        private static string GetCharacterAddress(CharacterEnum character) {
            switch (character) {
                case CharacterEnum.Gray: return R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value;
                case CharacterEnum.Red: return R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value;
                case CharacterEnum.Green: return R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value;
                case CharacterEnum.Blue: return R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value;
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
    public enum CharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
