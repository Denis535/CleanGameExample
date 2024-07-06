#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public partial class Gun {
        public static class Factory {

            private static readonly PrefabListHandle<Gun> Prefabs = new PrefabListHandle<Gun>( new[] {
                R.Project.Entities.Things.Value_Gun_Gray,
                R.Project.Entities.Things.Value_Gun_Red,
                R.Project.Entities.Things.Value_Gun_Green,
                R.Project.Entities.Things.Value_Gun_Blue,
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static Gun Create(Transform? parent) {
                var result = GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandom(), parent );
                return result;
            }
            public static Gun Create(Vector3 position, Quaternion rotation, Transform? parent) {
                var result = GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandom(), position, rotation, parent );
                return result;
            }

        }
    }
    public partial class Gun : Weapon {

        private FireDelay FireDelay { get; } = new FireDelay( 0.25f );
        private FirePoint FirePoint { get; set; } = default!;

        protected override void Awake() {
            base.Awake();
            FirePoint = gameObject.RequireComponentInChildren<FirePoint>();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        public override void Fire(IDamager damager) {
            if (FireDelay.CanFire) {
                FireDelay.Fire();
                var bullet = Bullet.Factory.Create( FirePoint.transform.position, FirePoint.transform.rotation, null, 5, damager, this );
                Physics.IgnoreCollision( gameObject.RequireComponentInChildren<Collider>(), bullet.gameObject.RequireComponentInChildren<Collider>() );
            }
        }

    }
}
