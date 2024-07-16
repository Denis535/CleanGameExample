#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Actors;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage, Actor? Actor, PlayerBase? Player);
    //public record KillZoneDamageInfo(float Damage) : DamageInfo( Damage, null, null );
    //public record PunchDamageInfo(Vector3 Point, Vector3 Direction, float Damage, Actor? Actor, PlayerBase? Player) : DamageInfo( Damage, Actor, Player );
    public record BulletDamageInfo(Vector3 Point, Vector3 Direction, float Damage, Weapon Weapon, Actor? Actor, PlayerBase? Player) : DamageInfo( Damage, Actor, Player );
}
