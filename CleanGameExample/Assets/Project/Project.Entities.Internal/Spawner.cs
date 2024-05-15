#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class Spawner {

        // Spawn
        public static PlayerCharacter SpawnPlayerCharacter(PlayerCharacterEnum character, PlayerSpawnPoint point) {
            var handle = Addressables.InstantiateAsync( GetPlayerCharacter( character ), point.transform.position, point.transform.rotation );
            var instance = handle.GetResult();
            return instance.RequireComponent<PlayerCharacter>();
        }
        public static EnemyCharacter SpawnEnemyCharacter(EnemySpawnPoint point) {
            var handle = Addressables.InstantiateAsync( GetEnemyCharacter(), point.transform.position, point.transform.rotation );
            var instance = handle.GetResult();
            return instance.RequireComponent<EnemyCharacter>();
        }
        public static Weapon SpawnWeapon(LootSpawnPoint point) {
            var handle = Addressables.InstantiateAsync( GetWeapon(), point.transform.position, point.transform.rotation );
            var instance = handle.GetResult();
            return instance.RequireComponent<Weapon>();
        }
        public static Bullet SpawnBullet(Transform point, Gun gun, float force) {
            var handle = Addressables.LoadAssetAsync<GameObject>( R.Project.Entities.Loots.Bullet_Value );
            var prefab = handle.GetResult();
            var instance = Object2.Instantiate( prefab, point.position, point.rotation, new Bullet.Args( gun, force ) );
            return instance.RequireComponent<Bullet>();
        }

        // Heleprs
        private static string GetPlayerCharacter(PlayerCharacterEnum character) {
            switch (character) {
                case PlayerCharacterEnum.Gray: return R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value;
                case PlayerCharacterEnum.Red: return R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value;
                case PlayerCharacterEnum.Green: return R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value;
                case PlayerCharacterEnum.Blue: return R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value;
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
        private static string GetWeapon() {
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
