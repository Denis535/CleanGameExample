#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Weapon {

        // Awake
        public override void Awake() {
            base.Awake();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public override void Fire() {
            Debug.Log( "Fire" );
        }

    }
}
