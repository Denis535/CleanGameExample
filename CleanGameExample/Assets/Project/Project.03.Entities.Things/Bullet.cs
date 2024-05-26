#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Bullet : EntityBase {
        public record Args(IDamager Damager, Weapon Weapon, float Force);

        // Damager
        private IDamager Damager { get; set; } = default!;
        // Weapon
        private Weapon Weapon { get; set; } = default!;
        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            Damager = args.Damager;
            Weapon = args.Weapon;
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
                var character = collision.transform.root.GetComponent<IDamageable>();
                if (character != Damager) {
                    character?.OnDamage( Damager, Weapon, 5, Rigidbody.position, Rigidbody.velocity.normalized );
                    enabled = false;
                }
            }
        }

    }
}
