#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class GameFactory {

        // Game
        public static Game Game(PlayerCharacterEnum character, LevelEnum level) {
            using (Context.Begin( new Game.Args( character, level ) )) {
                return Instantiate<Game>( R.Project.Entities.Game_Value );
            }
        }

        // Camera
        public static Camera2 Camera() {
            return Instantiate<Camera2>( R.Project.Entities.Camera_Value );
        }

        // Helpers
        private static T Instantiate<T>(string key) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>() );
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
