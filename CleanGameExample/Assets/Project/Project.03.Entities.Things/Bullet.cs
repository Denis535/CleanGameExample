#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Bullet : EntityBase {
        public record Args(IDamageable Owner, Weapon Weapon, float Force);

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; set; } = default!;
        // Owner
        private IDamageable Owner { get; set; } = default!;
        // Weapon
        private Weapon Weapon { get; set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            Rigidbody.AddForce( transform.forward * args.Force, ForceMode.Impulse );
            Owner = args.Owner;
            Weapon = args.Weapon;
            Destroy( gameObject, 10 );
        }
        public override void OnDestroy() {
        }

        // OnCollisionEnter
        public void OnCollisionEnter(Collision collision) {
            if (enabled) {
                var damageable = collision.transform.root.GetComponent<IDamageable>();
                damageable?.OnDamage( 5, transform.position, transform.forward );
                enabled = false;
            }
        }

    }
}
