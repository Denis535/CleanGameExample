#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class CharacterFactory {

        // PlayerCharacter
        public static PlayerCharacter PlayerCharacter(PlayerCharacterEnum character, Vector3 position, Quaternion rotation) {
            var key = character switch {
                PlayerCharacterEnum.Gray => R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value,
                PlayerCharacterEnum.Red => R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value,
                PlayerCharacterEnum.Green => R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value,
                PlayerCharacterEnum.Blue => R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value,
                _ => throw Exceptions.Internal.NotSupported( $"PlayerCharacter {character} is not supported" )
            };
            return Instantiate<PlayerCharacter>( key, position, rotation );
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
            return Instantiate<EnemyCharacter>( key, position, rotation );
        }

        // Helpers
        private static T Instantiate<T>(string key, Vector3 position, Quaternion rotation) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>(), position, rotation );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }
        private static T Instantiate<T>(string key, Transform parent) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>(), parent );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }

    }
}
