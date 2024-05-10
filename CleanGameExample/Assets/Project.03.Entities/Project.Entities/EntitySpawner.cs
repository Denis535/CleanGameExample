#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public static class EntitySpawner {

        // Spawn
        public static Character SpawnPlayerCharacter(PlayerSpawnPoint point, string key) {
            var instance = Addressables2.Instantiate( key, point.transform.position, point.transform.rotation );
            return instance.RequireComponent<Character>();
        }

        // SpawnAsync
        public static async ValueTask<Character> SpawnPlayerCharacterAsync(PlayerSpawnPoint point, string key, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( key, point.transform.position, point.transform.rotation, cancellationToken );
            return instance.RequireComponent<Character>();
        }
        public static async ValueTask<GameObject> SpawnEnemyCharacterAsync(EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( GetEnemyCharacter(), point.transform.position, point.transform.rotation, cancellationToken );
            return instance;
        }
        public static async ValueTask<GameObject> SpawnLootAsync(LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( GetLoot(), point.transform.position, point.transform.rotation, cancellationToken );
            return instance;
        }
        public static async ValueTask<GameObject> SpawnBulletAsync(Transform point, Gun gun, CancellationToken cancellationToken) {
            var instance = await Addressables2.InstantiateAsync( R.Project.Entities.Loots.Bullet_Value, prefab => {
                return Object2.Instantiate( prefab, point.position, point.rotation, new Bullet.Args( gun ) );
            }, cancellationToken );
            return instance;
        }

        // Heleprs
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
                R.Project.Entities.Loots.Gun_Gray_Value,
                R.Project.Entities.Loots.Gun_Red_Value,
                R.Project.Entities.Loots.Gun_Green_Value,
                R.Project.Entities.Loots.Gun_Blue_Value,
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }

    }
}
