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
    public abstract record DamageInfo(ICharacter Character, float Damage, Vector3 Point, Vector3 Direction);
    public record BulletDamageInfo(ICharacter Character, float Damage, Vector3 Point, Vector3 Direction, Bullet Bullet) : DamageInfo( Character, Damage, Point, Direction );
}
