#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    internal static class SpawnHelper {

        // Spawn
        public static async ValueTask<Character> SpawnPlayerCharacterAsync(this List<InstanceHandle<Character>> instances, PlayerSpawnPoint point, CharacterEnum character, Player player, CancellationToken cancellationToken) {
            using (Context.Begin<Character, Character.Arguments>( new Character.Arguments( player ) )) {
                using (Context.Begin<CharacterBody, CharacterBody.Arguments>( new CharacterBody.Arguments( player ) )) {
                    var instance = new InstanceHandle<Character>( GetPlayerCharacter( character ) );
                    instances.Add( instance );
                    return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, cancellationToken );
                }
            }
        }
        public static async ValueTask<Transform> SpawnEnemyCharacterAsync(this List<InstanceHandle<Transform>> instances, EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( GetEnemyCharacter() );
            instances.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, cancellationToken );
        }
        public static async ValueTask<Transform> SpawnLootAsync(this List<InstanceHandle<Transform>> instances, LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( GetLoot() );
            instances.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, cancellationToken );
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
