#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Thing, IWeapon {

        private readonly Delay delay = new Delay( 0.25f );

        // FirePoint
        private FirePoint FirePoint { get; set; } = default!;

        // Awake
        public override void Awake() {
            base.Awake();
            Awake( Context.GetValue<GunFactory.Args>() );
        }
        private void Awake(GunFactory.Args args) {
            FirePoint = gameObject.RequireComponentInChildren<FirePoint>();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public void Fire(IDamager damager, BulletHitCallback? callback) {
            if (delay.IsCompleted) {
                delay.Start();
                var bullet = BulletFactory.Create( damager, this, 5, callback, FirePoint.transform.position, FirePoint.transform.rotation );
                Physics.IgnoreCollision( Collider, bullet.Collider );
            }
        }

    }
}
