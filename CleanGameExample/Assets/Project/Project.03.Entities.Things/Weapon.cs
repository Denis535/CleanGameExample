#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Weapon : EntityBase {

        // Rigidbody
        protected Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        protected Collider Collider { get; private set; } = default!;
        // IsAttached
        public bool IsAttached => transform.parent != null;
        // Owner
        public IDamageable Owner => transform.parent.gameObject.RequireComponentInParent<IDamageable>();

        // Awake
        public override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            SetPhysical( Rigidbody, transform.parent == null );
        }
        public override void OnDestroy() {
        }

        // Fire
        public abstract void Fire();

        // OnTransformParentChanged
        public void OnTransformParentChanged() {
            SetPhysical( Rigidbody, transform.parent == null );
        }

        // Helpers
        private static void SetPhysical(Rigidbody rigidbody, bool value) {
            rigidbody.isKinematic = !value;
        }

    }
}