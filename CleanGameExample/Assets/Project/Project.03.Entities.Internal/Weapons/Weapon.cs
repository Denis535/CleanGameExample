#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Weapon : EntityBase {

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Collider
        internal Collider Collider { get; private set; } = default!;
        // SpawnPoint
        protected SpawnPoint SpawnPoint { get; private set; } = default!;
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
            SpawnPoint = gameObject.RequireComponentInChildren<SpawnPoint>();
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
