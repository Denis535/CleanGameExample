#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GunView : WeaponView {

        // Constructor
        public GunView(GameObject gameObject) : base( gameObject ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
