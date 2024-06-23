#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class Gun : Thing, IWeapon {

        private readonly Delay delay = new Delay( 0.25f );

        // FirePoint
        private FirePoint FirePoint { get; set; } = default!;

        // Awake
        protected override void Awake() {
            base.Awake();
            Awake( Context.GetValue<GunFactory.Args>() );
        }
        private void Awake(GunFactory.Args args) {
            FirePoint = gameObject.RequireComponentInChildren<FirePoint>();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public void Fire(IDamager damager) {
            if (delay.IsCompleted) {
                delay.Start();
                var bullet = BulletFactory.Create( damager, this, 5, FirePoint.transform.position, FirePoint.transform.rotation );
                Physics.IgnoreCollision( Collider, bullet.Collider );
            }
        }

    }
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
                return GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandom() );
            }
        }
        public static Gun Create(Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandom(), position, rotation );
            }
        }

    }
}
