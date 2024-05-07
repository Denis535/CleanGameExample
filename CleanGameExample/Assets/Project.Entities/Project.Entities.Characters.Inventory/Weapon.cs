#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Weapon : EntityBase<WeaponView> {

        // View
        protected override WeaponView View { get; set; } = default!;

        // Awake
        public override void Awake() {
        }
        public override void OnDestroy() {
        }

        // Fire
        public abstract void Fire();

        // OnTransformParentChanged
        public void OnTransformParentChanged() {
            if (transform.parent != null) {
                View.IsPhysical = false;
            } else {
                View.IsPhysical = true;
            }
        }

    }
    public abstract class WeaponView : EntityViewBase {

        // GameObject
        protected override GameObject GameObject { get; }
        // Rigidbody
        private Rigidbody Rigidbody { get; }
        // IsPhysics
        public bool IsPhysical { get => !Rigidbody.isKinematic; set => Rigidbody.isKinematic = !value; }

        // Constructor
        public WeaponView(GameObject gameObject) {
            GameObject = gameObject;
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public override void Dispose() {
        }

    }
}
