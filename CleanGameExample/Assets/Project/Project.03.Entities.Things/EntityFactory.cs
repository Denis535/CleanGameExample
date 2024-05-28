#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    // Gun
    public static class GunFactory {
        public record Args();

        private static readonly AssetListHandle<GameObject> Prefabs = new AssetListHandle<GameObject>( new[] {
            R.Project.Entities.Things.Gun_Gray_Value,
            R.Project.Entities.Things.Gun_Red_Value,
            R.Project.Entities.Things.Gun_Green_Value,
            R.Project.Entities.Things.Gun_Blue_Value,
        } );

        public static void Initialize() {
            Prefabs.Load().Wait();
        }
        public static void Deinitialize() {
            Prefabs.Release();
        }

        public static Gun Create() {
            using (Context.Begin( new Args() )) {
                return Prefabs.Values.GetRandomValue().Instantiate<Gun>();
            }
        }
        public static Gun Create(Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return Prefabs.Values.GetRandomValue().Instantiate<Gun>( position, rotation );
            }
        }

    }
    // Bullet
    public static class BulletFactory {
        public record Args(IDamager Damager, Gun Gun, float Force, BulletHitCallback? Callback);

        private static readonly AssetHandle<GameObject> Prefab = new AssetHandle<GameObject>( R.Project.Entities.Things.Bullet_Value );

        public static void Initialize() {
            Prefab.Load().Wait();
        }
        public static void Deinitialize() {
            Prefab.Release();
        }

        public static Bullet Create(IDamager damager, Gun gun, float force, BulletHitCallback? callback, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args( damager, gun, force, callback ) )) {
                return Prefab.Value.Instantiate<Bullet>( position, rotation );
            }
        }

    }
}
