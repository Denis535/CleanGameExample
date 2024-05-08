#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Weapon : EntityBase {

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // IsPhysics
        private bool IsPhysical { get => !Rigidbody.isKinematic; set => Rigidbody.isKinematic = !value; }

        // Awake
        public override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public override void OnDestroy() {
        }

        // Fire
        public abstract void Fire();

        // OnTransformParentChanged
        public void OnTransformParentChanged() {
            if (transform.parent != null) {
                IsPhysical = false;
            } else {
                IsPhysical = true;
            }
        }

    }
}
