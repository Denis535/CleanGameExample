#nullable enable
namespace Project.Entities.Characters.Inventory {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Weapon {

        // View
        protected new GunView View { get => (GunView) base.View; set => base.View = value; }

        // Awake
        public override void Awake() {
            base.Awake();
            View = new GunView( gameObject );
        }
        public override void OnDestroy() {
            View.Dispose();
            base.OnDestroy();
        }

        // Fire
        public override void Fire() {
            Debug.Log( "Fire" );
        }

    }
    public class GunView : WeaponView {

        // Constructor
        public GunView(GameObject gameObject) : base( gameObject ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
