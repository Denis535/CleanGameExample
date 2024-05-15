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
            var prefab = Addressables.LoadAssetAsync<GameObject>( R.Project.Entities.Camera_Value );
            var instance = Object2.Instantiate( prefab.GetResult<Camera2>(), null );
            instance.destroyCancellationToken.Register( () => Addressables.ReleaseInstance( prefab ) );
            return instance;
        }

    }
}
