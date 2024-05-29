#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    // DamageInfo
    public abstract record DamageInfo(IDamager Damager, float Damage, Vector3 Point, Vector3 Direction);
    public record BulletDamageInfo(IDamager Damager, float Damage, Vector3 Point, Vector3 Direction, Bullet Bullet) : DamageInfo( Damager, Damage, Point, Direction );
}
