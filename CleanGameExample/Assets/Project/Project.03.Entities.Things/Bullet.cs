#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public static class BulletFactory {
        public record Args(IDamager Damager, Gun Gun, float Force);

        private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Entities.Things.Value_Bullet );

        public static void Load() {
            Prefab.Load().Wait();
        }
        public static void Unload() {
            Prefab.Release();
        }

        public static Bullet Create(IDamager damager, Gun gun, float force, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args( damager, gun, force ) )) {
                return GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation );
            }
        }

    }
    public class Bullet : EntityBase {

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; set; } = default!;
        // Damager
        public IDamager Damager { get; private set; } = default!;
        // Gun
        public Gun Gun { get; private set; } = default!;

        // Awake
        protected override void Awake() {
            Awake( Context.GetValue<BulletFactory.Args>() );
        }
        private void Awake(BulletFactory.Args args) {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            Damager = args.Damager;
            Gun = args.Gun;
            Rigidbody.AddForce( transform.forward * args.Force, ForceMode.Impulse );
            Destroy( gameObject, 10 );
        }
        protected override void OnDestroy() {
        }

        // OnCollisionEnter
        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                if (damageable != null && damageable != Damager) {
                    damageable.OnDamage( new BulletDamageInfo( Damager, 5, Rigidbody.position, Rigidbody.velocity.normalized, this ) );
                }
                enabled = false;
            }
        }

    }
}
