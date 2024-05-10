#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : Weapon {

        private readonly Delay delay = new Delay( 0.2f );

        // BulletSpawnPoint
        private Transform BulletSpawnPoint { get; set; } = default!;

        // Awake
        public override void Awake() {
            base.Awake();
            BulletSpawnPoint = transform.Find( "BulletSpawnPoint" );
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Fire
        public override async void Fire() {
            if (delay.IsCompleted) {
                delay.Start();
                await EntitySpawner.SpawnBulletAsync( BulletSpawnPoint, this, destroyCancellationToken );
            }
        }

    }
}
