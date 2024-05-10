#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    internal static class EntitySpawner {

        // Spawn
        public static Character SpawnPlayerCharacter(PlayerSpawnPoint point, PlayerCharacterEnum character) {
            var instance = Addressables2.Instantiate( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation );
            return instance.RequireComponent<Character>();
        }

        // SpawnAsync
        public static async ValueTask<Character> SpawnPlayerCharacterAsync(PlayerSpawnPoint point, PlayerCharacterEnum character, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation, cancellationToken );
            return instance.RequireComponent<Character>();
        }
        public static async ValueTask SpawnEnemyCharacterAsync(EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( GetEnemyCharacter(), point.transform.position, point.transform.rotation, cancellationToken );
        }
        public static async ValueTask SpawnLootAsync(LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( GetLoot(), point.transform.position, point.transform.rotation, cancellationToken );
        }
        public static async ValueTask SpawnBulletAsync(Transform point, Gun gun, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( R.Project.Entities.Characters.Bullet_Value, prefab => {
                return Object2.Instantiate( prefab, point.position, point.rotation, new Bullet.Args( gun ) );
            }, cancellationToken );
        }

        // Heleprs
        private static string GetPlayerCharacter(PlayerCharacterEnum character) {
            switch (character) {
                case PlayerCharacterEnum.Gray: return R.Project.Entities.Characters.PlayerCharacter_Gray_Value;
                case PlayerCharacterEnum.Red: return R.Project.Entities.Characters.PlayerCharacter_Red_Value;
                case PlayerCharacterEnum.Green: return R.Project.Entities.Characters.PlayerCharacter_Green_Value;
                case PlayerCharacterEnum.Blue: return R.Project.Entities.Characters.PlayerCharacter_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }
        private static string GetEnemyCharacter() {
            var array = new[] {
                R.Project.Entities.Characters.EnemyCharacter_Gray_Value,
                R.Project.Entities.Characters.EnemyCharacter_Red_Value,
                R.Project.Entities.Characters.EnemyCharacter_Green_Value,
                R.Project.Entities.Characters.EnemyCharacter_Blue_Value
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }
        private static string GetLoot() {
            var array = new[] {
                R.Project.Entities.Characters.Gun_Gray_Value,
                R.Project.Entities.Characters.Gun_Red_Value,
                R.Project.Entities.Characters.Gun_Green_Value,
                R.Project.Entities.Characters.Gun_Blue_Value,
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }

    }
}
