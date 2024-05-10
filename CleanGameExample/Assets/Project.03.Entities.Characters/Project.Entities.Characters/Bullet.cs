#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Bullet : EntityBase {
        public record Args(Gun Gun);

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        private Collider Collider { get; set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            Rigidbody.AddForce( args.Gun.transform.forward * 100, ForceMode.Impulse );
            Physics.IgnoreCollision( Collider, args.Gun.Collider );
            Destroy( gameObject, 10 );
        }
        public override void OnDestroy() {
        }

    }
}
