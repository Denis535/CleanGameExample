#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Bullet : EntityBase {
        public record Args(float Force);

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            Rigidbody.AddForce( transform.forward * args.Force, ForceMode.Impulse );
            Destroy( gameObject, 10 );
        }
        public override void OnDestroy() {
        }

    }
}
