#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage);
    public record BulletDamageInfo(float Damage, IWeapon Weapon, IDamager Damager, Vector3 Point, Vector3 Direction) : DamageInfo( Damage );
}
