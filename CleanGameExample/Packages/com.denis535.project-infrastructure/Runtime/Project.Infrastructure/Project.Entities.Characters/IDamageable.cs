#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage, IDamager Damager, PlayerBase? Player);
    public record BulletDamageInfo(Vector3 Point, Vector3 Direction, float Damage, IWeapon Weapon, IDamager Damager, PlayerBase? Player) : DamageInfo( Damage, Damager, Player );
}
