#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    internal static class Spawner {

        // Instances
        private static List<InstanceHandle> Instances { get; } = new List<InstanceHandle>();

        // Spawn
        public static Character SpawnPlayerCharacter(PlayerSpawnPoint point, CharacterEnum character) {
            return Instantiate<Character>( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation ).GetValue();
        }

        // SpawnAsync
        public static ValueTask<Character> SpawnPlayerCharacterAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            return Instantiate<Character>( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation ).GetValueAsync( cancellationToken );
        }
        public static ValueTask<Transform> SpawnEnemyCharacterAsync(EnemySpawnPoint point, CancellationToken cancellationToken) {
            return Instantiate<Transform>( GetEnemyCharacter(), point.transform.position, point.transform.rotation ).GetValueAsync( cancellationToken );
        }
        public static ValueTask<Transform> SpawnLootAsync(LootSpawnPoint point, CancellationToken cancellationToken) {
            return Instantiate<Transform>( GetLoot(), point.transform.position, point.transform.rotation ).GetValueAsync( cancellationToken );
        }

        // Instantiate
        public static InstanceHandle<T> Instantiate<T>(string key) where T : notnull, Component {
            var instance = new InstanceHandle<T>( key );
            Instances.Add( instance );
            return instance.Instantiate();
        }
        public static InstanceHandle<T> Instantiate<T>(string key, Transform? parent) where T : notnull, Component {
            var instance = new InstanceHandle<T>( key );
            Instances.Add( instance );
            return instance.Instantiate( parent );
        }
        public static InstanceHandle<T> Instantiate<T>(string key, Vector3 position, Quaternion rotation) where T : notnull, Component {
            var instance = new InstanceHandle<T>( key );
            Instances.Add( instance );
            return instance.Instantiate( position, rotation );
        }
        public static InstanceHandle<T> Instantiate<T>(string key, Vector3 position, Quaternion rotation, Transform? parent) where T : notnull, Component {
            var instance = new InstanceHandle<T>( key );
            Instances.Add( instance );
            return instance.Instantiate( position, rotation, parent );
        }

        // Release
        public static void Release<T>(T value) where T : notnull, Component {
            var instance = Instances.OfType<InstanceHandle<T>>().First( i => i.ValueSafe == value );
            instance.Release();
            Instances.Remove( instance );
        }

        // ReleaseAll
        public static void ReleaseAll() {
            Instances.RemoveAll( instance => {
                instance.Release();
                return true;
            } );
        }

        // Heleprs
        private static string GetPlayerCharacter(CharacterEnum character) {
            switch (character) {
                case CharacterEnum.Gray: return R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value;
                case CharacterEnum.Red: return R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value;
                case CharacterEnum.Green: return R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value;
                case CharacterEnum.Blue: return R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }
        private static string GetEnemyCharacter() {
            var array = new[] {
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Gray_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Red_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Green_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Blue_Value
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }
        private static string GetLoot() {
            var array = new[] {
                R.Project.Entities.Characters.Inventory.Gun_Gray_Value,
                R.Project.Entities.Characters.Inventory.Gun_Red_Value,
                R.Project.Entities.Characters.Inventory.Gun_Green_Value,
                R.Project.Entities.Characters.Inventory.Gun_Blue_Value,
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }

    }
}
