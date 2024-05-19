#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class EntityFactory {

        // Gun
        public static void Gun(Slot slot) {
            var keys = new[] {
                R.Project.Entities.Weapons.Gun_Gray_Value,
                R.Project.Entities.Weapons.Gun_Red_Value,
                R.Project.Entities.Weapons.Gun_Green_Value,
                R.Project.Entities.Weapons.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            Instantiate<Gun>( key, slot.transform );
        }
        public static Gun Gun(Vector3 position, Quaternion rotation) {
            var keys = new[] {
                R.Project.Entities.Weapons.Gun_Gray_Value,
                R.Project.Entities.Weapons.Gun_Red_Value,
                R.Project.Entities.Weapons.Gun_Green_Value,
                R.Project.Entities.Weapons.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return Instantiate<Gun>( key, position, rotation );
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, float force) {
            using (Context.Begin( new Bullet.Args( force ) )) {
                return Instantiate<Bullet>( R.Project.Entities.Weapons.Bullet_Value, position, rotation );
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
