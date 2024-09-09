#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Actors;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class WeaponBase : ThingBase {

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        public abstract void Fire(ActorBase actor, PlayerBase? player);

    }
}
