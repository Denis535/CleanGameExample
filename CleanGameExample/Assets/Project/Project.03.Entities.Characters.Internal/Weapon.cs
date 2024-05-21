#nullable enable
namespace Project.Entities {
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
        // IsFree
        public bool IsFree {
            get => !Rigidbody.isKinematic;
            private set {
                Rigidbody.isKinematic = !value;
                Rigidbody.detectCollisions = value;
            }
        }

        // Awake
        public override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
            IsFree = transform.parent == null;
        }
        public override void OnDestroy() {
        }

        // Fire
        public abstract void Fire();

        // OnTransformParentChanged
        public void OnTransformParentChanged() {
            IsFree = transform.parent == null;
        }

    }
}
