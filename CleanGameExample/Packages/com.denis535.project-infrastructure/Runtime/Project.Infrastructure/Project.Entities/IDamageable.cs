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
    public abstract record DamageInfo(float Damage, ICharacter? Character, PlayerBase? Player);
    //public record KillZoneDamageInfo(float Damage) : DamageInfo( Damage, null, null );
    //public record PunchDamageInfo(Vector3 Point, Vector3 Direction, float Damage, ICharacter? Character, PlayerBase? Player) : DamageInfo( Damage, Character, Player );
    public record BulletDamageInfo(Vector3 Point, Vector3 Direction, float Damage, IWeapon Weapon, ICharacter? Character, PlayerBase? Player) : DamageInfo( Damage, Character, Player );
    public interface IWeapon {
    }
    public interface ICharacter {
    }
}
