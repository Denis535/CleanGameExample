#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
}
