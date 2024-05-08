#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

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
