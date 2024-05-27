#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Bullet : EntityBase {
        public delegate void OnDamageCallback(IDamageable damageable, Bullet bullet);

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; set; } = default!;
        // Damager
        public IDamager Damager { get; private set; } = default!;
        // Weapon
        public Gun Gun { get; private set; } = default!;
        // OnDamage
        private OnDamageCallback? OnDamage { get; set; } = default!;

        // Awake
        public override void Awake() {
            Awake( Context.GetValue<BulletFactory.Args>() );
        }
        private void Awake(BulletFactory.Args args) {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            Damager = args.Damager;
            Gun = args.Gun;
            OnDamage = args.OnDamage;
            Rigidbody.AddForce( transform.forward * args.Force, ForceMode.Impulse );
            Destroy( gameObject, 10 );
        }
        public override void OnDestroy() {
        }

        // OnCollisionEnter
        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                if (damageable != null && damageable != Damager) {
                    damageable.OnDamage( this, 5, Rigidbody.position, Rigidbody.velocity.normalized );
                    OnDamage?.Invoke( damageable, this );
                }
                enabled = false;
            }
        }

    }
}
