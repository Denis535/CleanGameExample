#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class Gun : Weapon {

        // BulletSpawnPoint
        private Transform BulletSpawnPoint { get; set; } = default!;
        // FireTime
        private TimePoint FireTime { get; set; }

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
            if (FireTime.Duration >= 0.2f) {
                FireTime = TimePoint.Now;
                var bullet = await Addressables2.InstantiateAsync<Bullet>( R.Project.Entities.Characters.Bullet_Value, BulletSpawnPoint.position, BulletSpawnPoint.rotation, destroyCancellationToken );
                Physics.IgnoreCollision( Collider, bullet.Collider );
                Destroy( bullet.gameObject, 10 );
            }
        }

    }
}
