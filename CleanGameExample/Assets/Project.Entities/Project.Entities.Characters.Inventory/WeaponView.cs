#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class WeaponView : EntityViewBase {

        protected Rigidbody Rigidbody { get; set; } = default!;

        // Awake
        public override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public override void OnDestroy() {
        }

    }
}
