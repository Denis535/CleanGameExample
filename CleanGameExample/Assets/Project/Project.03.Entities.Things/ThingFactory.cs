#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class ThingFactory {

        private static readonly AssetListHandle<GameObject> WeaponPrefabs = new AssetListHandle<GameObject>( new[] {
            R.Project.Entities.Things.Gun_Gray_Value,
            R.Project.Entities.Things.Gun_Red_Value,
            R.Project.Entities.Things.Gun_Green_Value,
            R.Project.Entities.Things.Gun_Blue_Value,
        } );
        private static readonly AssetHandle<GameObject> BulletPrefab = new AssetHandle<GameObject>( R.Project.Entities.Things.Bullet_Value );

        // Initialize
        public static void Initialize() {
            WeaponPrefabs.Load().Wait();
            BulletPrefab.Load().Wait();
        }
        public static void Deinitialize() {
            WeaponPrefabs.Release();
            BulletPrefab.Release();
        }

        // Gun
        public static Gun Gun(Vector3 position, Quaternion rotation) {
            return WeaponPrefabs.Values.GetRandomValue().Instantiate<Gun>( position, rotation );
        }
        public static void Gun(Slot slot) {
            WeaponPrefabs.Values.GetRandomValue().Instantiate<Gun>( slot.transform );
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, IDamageable owner, Weapon weapon, float force) {
            using (Context.Begin( new Bullet.Args( owner, weapon, force ) )) {
                return BulletPrefab.Value.Instantiate<Bullet>( position, rotation );
            }
        }

    }
}
