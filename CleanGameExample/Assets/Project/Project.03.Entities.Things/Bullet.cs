#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public partial class Bullet {
        public static class Factory {

            private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Entities.Things.Value_Bullet );

            public static void Load() {
                Prefab.Load().Wait();
            }
            public static void Unload() {
                Prefab.Release();
            }

            public static Bullet Create(Vector3 position, Quaternion rotation, Transform? parent, float force, IDamager damager, IWeapon weapon) {
                var result = GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation, parent );
                result.Awake( force, damager, weapon );
                return result;
            }

        }
    }
    public partial class Bullet : MonoBehaviour {

        private Rigidbody Rigidbody { get; set; } = default!;
        public float Force { get; private set; } = default!;
        public IDamager Damager { get; private set; } = default!;
        public IWeapon Weapon { get; private set; } = default!;

        protected void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        protected void Awake(float force, IDamager damager, IWeapon weapon) {
            Force = force;
            Damager = damager;
            Weapon = weapon;
            Rigidbody.AddForce( transform.forward * force, ForceMode.Impulse );
            GameObject.Destroy( gameObject, 10 );
        }
        protected void OnDestroy() {
        }

        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                if (damageable != null && damageable != Damager) {
                    damageable.OnDamage( new BulletDamageInfo( Force, Damager, Weapon, Rigidbody.position, Rigidbody.velocity.normalized ) );
                }
                enabled = false;
            }
        }

    }
}
