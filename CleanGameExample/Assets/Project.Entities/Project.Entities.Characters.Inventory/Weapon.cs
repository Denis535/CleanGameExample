#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Weapon<TView> : EntityBase<TView> where TView : WeaponView {

        // Awake
        public override void Awake() {
            View = gameObject.RequireComponent<TView>();
        }
        public override void OnDestroy() {
        }

        // Fire
        public abstract void Fire();

    }
}
