#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class WeaponViewBase : EntityViewBase {

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // IsRagdoll
        public bool IsRagdoll { get => !Rigidbody.isKinematic; set => Rigidbody.isKinematic = !value; }

        // Awake
        public override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public override void OnDestroy() {
        }

    }
}
