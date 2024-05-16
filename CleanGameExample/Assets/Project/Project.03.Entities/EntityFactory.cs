#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class EntityFactory {

        // Game
        public static Game Game(PlayerCharacterType character, LevelType level) {
            return Instantiate<Game>( R.Project.Entities.Game_Value, new Game.Args( character, level ) );
        }
        public static Player AddPlayer(this Game game) {
            using (Context.Begin( new Player.Args() )) {
                return game.gameObject.AddComponent<Player>();
            }
        }

        // Camera
        public static Camera2 Camera() {
            return Instantiate<Camera2>( R.Project.Entities.Camera_Value, null );
        }

        // PlayerCharacter
        public static PlayerCharacter PlayerCharacter(PlayerCharacterType character, Vector3 position, Quaternion rotation) {
            var key = character switch {
                PlayerCharacterType.Gray => R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value,
                PlayerCharacterType.Red => R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value,
                PlayerCharacterType.Green => R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value,
                PlayerCharacterType.Blue => R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value,
                _ => throw Exceptions.Internal.NotSupported( $"PlayerCharacter {character} is not supported" )
            };
            return Instantiate<PlayerCharacter>( key, null, position, rotation );
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
            return Instantiate<EnemyCharacter>( key, null, position, rotation );
        }

        // Helpers
        private static T Instantiate<T>(string key, object? arguments) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = Object2.Instantiate( prefab.GetResult<T>(), arguments );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }
        private static T Instantiate<T>(string key, object? arguments, Vector3 position, Quaternion rotation) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = Object2.Instantiate( prefab.GetResult<T>(), arguments, position, rotation );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }
        private static T Instantiate<T>(string key, object? arguments, Transform parent) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = Object2.Instantiate( prefab.GetResult<T>(), arguments, parent );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }

    }
}
