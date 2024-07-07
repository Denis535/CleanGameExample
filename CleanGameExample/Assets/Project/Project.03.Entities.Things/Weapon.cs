#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;

    public abstract class Weapon : Thing, IWeapon {

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        public abstract void Fire(IDamager damager);

    }
}
