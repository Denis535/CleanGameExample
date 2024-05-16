#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class EntityFactory2 {

        // Gun
        public static Gun Gun(Vector3 position, Quaternion rotation) {
            var keys = new[] {
                R.Project.Entities.Misc.Gun_Gray_Value,
                R.Project.Entities.Misc.Gun_Red_Value,
                R.Project.Entities.Misc.Gun_Green_Value,
                R.Project.Entities.Misc.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return Instantiate<Gun>( key, null, position, rotation );
        }
        public static Gun Gun(Transform parent) {
            var keys = new[] {
                R.Project.Entities.Misc.Gun_Gray_Value,
                R.Project.Entities.Misc.Gun_Red_Value,
                R.Project.Entities.Misc.Gun_Green_Value,
                R.Project.Entities.Misc.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return Instantiate<Gun>( key, null, parent );
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, Gun gun, float force) {
            return Instantiate<Bullet>( R.Project.Entities.Misc.Bullet_Value, new Bullet.Args( gun, force ), position, rotation );
        }

        // Helpers
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
