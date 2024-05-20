#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class EntityFactory {

        // Instantiate
        public static T Instantiate<T>(string key) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>() );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }
        public static T Instantiate<T>(string key, Vector3 position, Quaternion rotation) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>(), position, rotation );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }

        // Instantiate
        public static T Instantiate<T>(string key, Transform parent) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>(), parent );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }
        public static T Instantiate<T>(string key, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour {
            var prefab = Addressables.LoadAssetAsync<GameObject>( key );
            var instance = UnityEngine.Object.Instantiate( prefab.GetResult<T>(), position, rotation, parent );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }

    }
}
