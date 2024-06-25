#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class Gun {

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
    public partial class Gun : Thing, IWeapon {

        private readonly Delay delay = new Delay( 0.25f );

        // FirePoint
        private FirePoint FirePoint { get; set; } = default!;

        // Awake
        protected override void Awake() {
            base.Awake();
            FirePoint = gameObject.RequireComponentInChildren<FirePoint>();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public void Fire(IDamager damager) {
            if (delay.IsCompleted) {
                delay.Start();
                var bullet = Bullet.Create( 5, this, damager, FirePoint.transform.position, FirePoint.transform.rotation, null );
                Physics.IgnoreCollision( Collider, bullet.GetComponent<Collider>() );
            }
        }

    }
}
