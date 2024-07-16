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

            public static Bullet Create(Vector3 position, Quaternion rotation, Transform? parent, float force, Weapon weapon, Actor actor, PlayerBase? player) {
                var result = GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation, parent );
                result.Force = force;
                result.Weapon = weapon;
                result.Actor = actor;
                result.Player = player;
                result.Rigidbody.AddForce( result.transform.forward * force, ForceMode.Impulse );
                GameObject.Destroy( result.gameObject, 10 );
                return result;
            }

        }
    }
    [DefaultExecutionOrder( 100 )]
    public partial class Bullet : MonoBehaviour {

        private Rigidbody Rigidbody { get; set; } = default!;
        public float Force { get; private set; } = default!;
        public Weapon Weapon { get; private set; } = default!;
        public Actor Actor { get; private set; } = default!;
        public PlayerBase? Player { get; private set; } = default!;

        protected void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        protected void OnDestroy() {
        }

        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                if (damageable != null && damageable != (IDamageable) Actor) {
                    damageable.OnDamage( new BulletDamageInfo( Rigidbody.position, Rigidbody.velocity.normalized, Force, Weapon, Actor, Player ) );
                }
                enabled = false;
            }
        }

    }
}
