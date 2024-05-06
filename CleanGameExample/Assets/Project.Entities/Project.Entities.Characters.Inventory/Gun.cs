#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : WeaponBase<GunView> {

        // Awake
        public override void Awake() {
            base.Awake();
            View = new GunView( gameObject );
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public override void Fire() {
        }

    }
    public class GunView : WeaponViewBase {

        // Constructor
        public GunView(GameObject gameObject) : base( gameObject ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
