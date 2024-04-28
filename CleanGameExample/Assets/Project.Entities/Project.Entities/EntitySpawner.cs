#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    internal static class EntitySpawner {

        // Spawn
        public static Character SpawnPlayerCharacter(PlayerSpawnPoint point, CharacterEnum character) {
            return Addressables2.Instantiate<Character>( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation );
        }

        // SpawnAsync
        public static ValueTask<Character> SpawnPlayerCharacterAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            return Addressables2.InstantiateAsync<Character>( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation, cancellationToken );
        }
        public static ValueTask<Transform> SpawnEnemyCharacterAsync(EnemySpawnPoint point, CancellationToken cancellationToken) {
            return Addressables2.InstantiateAsync<Transform>( GetEnemyCharacter(), point.transform.position, point.transform.rotation, cancellationToken );
        }
        public static ValueTask<Transform> SpawnLootAsync(LootSpawnPoint point, CancellationToken cancellationToken) {
            return Addressables2.InstantiateAsync<Transform>( GetLoot(), point.transform.position, point.transform.rotation, cancellationToken );
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
