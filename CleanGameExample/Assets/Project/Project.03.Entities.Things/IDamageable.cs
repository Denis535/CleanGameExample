#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IDamageable {
        bool OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(IDamager Damager);
    public record GunDamageInfo(IDamager Damager, Gun Gun, Bullet Bullet, float Damage, Vector3 Point, Vector3 Direction) : DamageInfo( Damager );
    public delegate void OnDamageCallback(DamageInfo info, IDamageable damageable, bool isKilled);
}
