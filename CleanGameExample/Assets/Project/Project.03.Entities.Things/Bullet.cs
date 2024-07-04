#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class Bullet {
        public static class Factory {

            private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Entities.Things.Value_Bullet );

            public static void Load() {
                Prefab.Load().Wait();
            }
            public static void Unload() {
                Prefab.Release();
            }

            public static Bullet Create(Vector3 position, Quaternion rotation, Transform? parent, float force, Gun gun, ICharacter character) {
                var result = GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation, parent );
                result.Awake( force, gun, character );
                return result;
            }

        }
    }
    public partial class Bullet : MonoBehaviour {

        public float Force { get; private set; } = default!;
        public Gun Gun { get; private set; } = default!;
        public ICharacter Character { get; private set; } = default!;
        private Rigidbody Rigidbody { get; set; } = default!;

        protected void Awake() {
        }
        protected void Awake(float force, Gun gun, ICharacter character) {
            Force = force;
            Gun = gun;
            Character = character;
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Rigidbody.AddForce( transform.forward * force, ForceMode.Impulse );
            GameObject.Destroy( gameObject, 10 );
        }
        protected void OnDestroy() {
        }

        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                if (damageable != null && damageable != Character) {
                    damageable.OnDamage( new BulletDamageInfo( Force, Character, Rigidbody.position, Rigidbody.velocity.normalized, this ) );
                }
                enabled = false;
            }
        }

    }
}
