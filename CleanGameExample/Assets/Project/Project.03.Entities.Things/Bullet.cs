#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public partial class Bullet {

        private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Entities.Things.Value_Bullet );

        public static void Load() {
            Prefab.Load().Wait();
        }
        public static void Unload() {
            Prefab.Release();
        }

        public static Bullet Create(float force, Gun gun, IDamager damager, Vector3 position, Quaternion rotation, Transform? parent) {
            var result = GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation, parent );
            result.Awake( force, gun, damager );
            return result;
        }

    }
    public partial class Bullet : EntityBase {

        private Rigidbody Rigidbody { get; set; } = default!;
        internal Collider Collider { get; set; } = default!;
        public Gun Gun { get; set; } = default!;
        public IDamager Damager { get; set; } = default!;

        protected override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
        }
        protected void Awake(float force, Gun gun, IDamager damager) {
            Rigidbody.AddForce( transform.forward * force, ForceMode.Impulse );
            Gun = gun;
            Damager = damager;
            GameObject.Destroy( gameObject, 10 );
        }
        protected override void OnDestroy() {
        }

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
