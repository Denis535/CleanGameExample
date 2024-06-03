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
            R.Project.Entities.Things.Value_Gun_Gray,
            R.Project.Entities.Things.Value_Gun_Red,
            R.Project.Entities.Things.Value_Gun_Green,
            R.Project.Entities.Things.Value_Gun_Blue,
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

        private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Entities.Things.Value_Bullet );

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
