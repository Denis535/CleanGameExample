#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Bullet : EntityBase {
        public record Args(IDamager Damager, Gun Gun, float Force);

        // Damager
        public IDamager Damager { get; private set; } = default!;
        // Weapon
        public Gun Gun { get; private set; } = default!;
        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            Damager = args.Damager;
            Gun = args.Gun;
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
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
                }
                enabled = false;
            }
        }

    }
}
