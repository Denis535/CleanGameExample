#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Weapon {

        private readonly Delay delay = new Delay( 0.25f );

        // FirePoint
        private FirePoint FirePoint { get; set; } = default!;

        // Awake
        public override void Awake() {
            base.Awake();
            FirePoint = gameObject.RequireComponentInChildren<FirePoint>();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public override void Fire() {
            if (delay.IsCompleted) {
                delay.Start();
                var bullet = ThingFactory.Bullet( FirePoint.transform.position, FirePoint.transform.rotation, Owner, this, 5 );
                Physics.IgnoreCollision( Collider, bullet.Collider );
            }
        }

    }
}