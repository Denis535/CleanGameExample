#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Weapon {

        private readonly Delay delay = new Delay( 0.25f );

        // BulletPoint
        protected Point BulletPoint { get; private set; } = default!;

        // Awake
        public override void Awake() {
            base.Awake();
            BulletPoint = gameObject.RequireComponentInChildren<Point>();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public override void Fire() {
            if (delay.IsCompleted) {
                delay.Start();
                var bullet = EntityFactory3.Bullet( BulletPoint.transform.position, BulletPoint.transform.rotation, 5 );
                Physics.IgnoreCollision( Collider, bullet.Collider );
            }
        }

    }
}
