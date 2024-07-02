#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ICharacter {
    }
    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage, ICharacter Character);
    public record BulletDamageInfo(float Damage, ICharacter Character, Vector3 Point, Vector3 Direction, Bullet Bullet) : DamageInfo( Damage, Character );
}
