#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Thing : MonoBehaviour {

        // Rigidbody
        protected Rigidbody Rigidbody { get; private set; } = default!;
        // Collider
        protected Collider Collider { get; private set; } = default!;
        // IsAttached
        public bool IsAttached => transform.parent != null;

        // Awake
        public virtual void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponentInChildren<Collider>();
        }
        public virtual void OnDestroy() {
        }

    }
}
