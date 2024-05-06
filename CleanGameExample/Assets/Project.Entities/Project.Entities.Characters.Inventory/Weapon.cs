#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Weapon<TView> : EntityBase<TView> where TView : WeaponView {

        // View
        protected override TView View { get; set; } = default!;

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // Fire
        public abstract void Fire();

        // OnTransformParentChanged
        public void OnTransformParentChanged() {
            if (transform.parent) {
                View.IsRagdoll = false;
            } else {
                View.IsRagdoll = true;
            }
        }

    }
    public abstract class WeaponView : EntityViewBase {

        // GameObject
        protected override GameObject GameObject { get; }
        // Rigidbody
        private Rigidbody Rigidbody { get; }
        // IsRagdoll
        public bool IsRagdoll { get => !Rigidbody.isKinematic; set => Rigidbody.isKinematic = !value; }

        // Constructor
        public WeaponView(GameObject gameObject) {
            GameObject = gameObject;
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public override void Dispose() {
        }

    }
}
