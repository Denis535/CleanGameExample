#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class EntityFactory {

        // Camera
        public static Camera2 Camera() {
            return Instantiate<Camera2>( R.Project.Entities.Camera_Value, null );
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
