#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Weapon {

        private readonly Delay delay = new Delay( 0.25f );

        // Awake
        public override void Awake() {
            base.Awake();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public override void Fire() {
            if (delay.IsCompleted) {
                delay.Start();
                EntityFactory.Bullet( SpawnPoint.transform.position, SpawnPoint.transform.rotation, this, 20 );
            }
        }

    }
}
