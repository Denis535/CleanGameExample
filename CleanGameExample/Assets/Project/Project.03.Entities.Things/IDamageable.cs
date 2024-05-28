#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IDamageable {
        bool IsAlive { get; }
        void OnDamage(BulletHitInfo info);
    }
    // HitInfo
    public record BulletHitInfo(IDamager Damager, Bullet Bullet, float Damage, Vector3 Point, Vector3 Direction);
    // HitCallback
    public delegate void BulletHitCallback(IDamageable damageable, BulletHitInfo info, bool isKilled);
}
