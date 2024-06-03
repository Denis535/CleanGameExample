#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class GunFactory {
        public record Args();

        private static readonly PrefabListHandle<Gun> Prefabs = new PrefabListHandle<Gun>( new[] {
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
                return GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandomValue() );
            }
        }
        public static Gun Create(Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandomValue(), position, rotation );
            }
        }

    }
    public static class BulletFactory {
        public record Args(IDamager Damager, Gun Gun, float Force);

        private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Entities.Things.Bullet_Value );

        public static void Initialize() {
            Prefab.Load().Wait();
        }
        public static void Deinitialize() {
            Prefab.Release();
        }

        public static Bullet Create(IDamager damager, Gun gun, float force, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args( damager, gun, force ) )) {
                return GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation );
            }
        }

    }
}
