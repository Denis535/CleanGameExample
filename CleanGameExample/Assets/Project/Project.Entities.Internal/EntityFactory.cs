#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class EntityFactory {

        // PlayerCharacter
        public static PlayerCharacter PlayerCharacter(PlayerCharacterEnum character, Vector3 position, Quaternion rotation) {
            var key = character switch {
                PlayerCharacterEnum.Gray => R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value,
                PlayerCharacterEnum.Red => R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value,
                PlayerCharacterEnum.Green => R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value,
                PlayerCharacterEnum.Blue => R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value,
                _ => throw Exceptions.Internal.NotSupported( $"PlayerCharacter {character} is not supported" )
            };
            var handle = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = Object2.Instantiate( handle.GetResult<PlayerCharacter>(), position, rotation, null );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( handle ) );
            return instance;
        }

        // EnemyCharacter
        public static EnemyCharacter EnemyCharacter(Vector3 position, Quaternion rotation) {
            var keys = new[] {
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Gray_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Red_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Green_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Blue_Value
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            var handle = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = Object2.Instantiate( handle.GetResult<EnemyCharacter>(), position, rotation, null );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( handle ) );
            return instance;
        }

        // Gun
        public static Gun Gun(Vector3 position, Quaternion rotation) {
            var keys = new[] {
                R.Project.Entities.Misc.Gun_Gray_Value,
                R.Project.Entities.Misc.Gun_Red_Value,
                R.Project.Entities.Misc.Gun_Green_Value,
                R.Project.Entities.Misc.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            var handle = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = Object2.Instantiate( handle.GetResult<Gun>(), position, rotation, null );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( handle ) );
            return instance;
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, Gun gun, float force) {
            var handle = Addressables.LoadAssetAsync<GameObject>( R.Project.Entities.Misc.Bullet_Value );
            var instance = Object2.Instantiate( handle.GetResult<Bullet>(), position, rotation, new Bullet.Args( gun, force ) );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( handle ) );
            return instance;
        }

    }
}
