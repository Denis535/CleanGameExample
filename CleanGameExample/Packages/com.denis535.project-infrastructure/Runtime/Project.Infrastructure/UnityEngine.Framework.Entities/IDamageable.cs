#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage);
    public record BulletDamageInfo(float Damage, IWeapon Weapon, IDamager Damager, Vector3 Point, Vector3 Direction) : DamageInfo( Damage );
}
