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

        public static Bullet Create(Vector3 position, Quaternion rotation, Transform? parent, Gun gun, IDamager damager, float force) {
            var result = GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation, parent );
            result.Awake( gun, damager, force );
            return result;
        }

    }
    public partial class Bullet : EntityBase<BulletBody, BulletView> {

        public Gun Gun { get; set; } = default!;
        public IDamager Damager { get; set; } = default!;

        protected override void Awake() {
            Body = new BulletBody( gameObject );
            View = new BulletView( gameObject );
        }
        protected void Awake(Gun gun, IDamager damager, float force) {
            Gun = gun;
            Damager = damager;
            Body.AddImpulse( force );
            GameObject.Destroy( gameObject, 10 );
        }
        protected override void OnDestroy() {
            View.Dispose();
            Body.Dispose();
        }

        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                if (damageable != null && damageable != Damager) {
                    damageable.OnDamage( new BulletDamageInfo( Damager, 5, Body.Position, Body.Velocity.normalized, this ) );
                }
                enabled = false;
            }
        }

    }
    public class BulletBody : EntityBodyBase {

        private Rigidbody Rigidbody { get; }
        private Collider Collider { get; }
        public Vector3 Position => Rigidbody.position;
        public Quaternion Rotation => Rigidbody.rotation;
        public Vector3 Velocity => Rigidbody.velocity;

        public BulletBody(GameObject gameObject) : base( gameObject ) {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        public void AddImpulse(float force) {
            Rigidbody.AddForce( Transform.forward * force, ForceMode.Impulse );
        }

    }
    public class BulletView : EntityViewBase {

        public BulletView(GameObject gameObject) : base( gameObject ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
