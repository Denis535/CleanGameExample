#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class WeaponBase<TView> : EntityBase<TView> where TView : WeaponViewBase {

        // Awake
        public override void Awake() {
            View = gameObject.RequireComponent<TView>();
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
}
