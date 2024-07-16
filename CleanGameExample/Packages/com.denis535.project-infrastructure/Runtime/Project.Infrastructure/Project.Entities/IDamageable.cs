#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage, IActor? Actor, PlayerBase? Player);
    //public record KillZoneDamageInfo(float Damage) : DamageInfo( Damage, null, null );
    //public record PunchDamageInfo(Vector3 Point, Vector3 Direction, float Damage, IActor? Actor, PlayerBase? Player) : DamageInfo( Damage, Actor, Player );
    public record BulletDamageInfo(Vector3 Point, Vector3 Direction, float Damage, IWeapon Weapon, IActor? Actor, PlayerBase? Player) : DamageInfo( Damage, Actor, Player );
    public interface IActor {
    }
    public interface IWeapon {
    }
}
