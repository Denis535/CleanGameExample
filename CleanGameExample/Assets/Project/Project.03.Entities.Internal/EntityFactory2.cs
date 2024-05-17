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
                R.Project.Entities.Objects.Gun_Gray_Value,
                R.Project.Entities.Objects.Gun_Red_Value,
                R.Project.Entities.Objects.Gun_Green_Value,
                R.Project.Entities.Objects.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return Instantiate<Gun>( key, position, rotation );
        }
        public static Gun Gun(Transform parent) {
            var keys = new[] {
                R.Project.Entities.Objects.Gun_Gray_Value,
                R.Project.Entities.Objects.Gun_Red_Value,
                R.Project.Entities.Objects.Gun_Green_Value,
                R.Project.Entities.Objects.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return Instantiate<Gun>( key, parent );
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, Gun gun, float force) {
            using (Context.Begin( new Bullet.Args( gun, force ) )) {
                return Instantiate<Bullet>( R.Project.Entities.Objects.Bullet_Value, position, rotation );
            }
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
