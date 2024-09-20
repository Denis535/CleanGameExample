#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Actors;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public interface IDamageable {
        void OnDamage(DamageInfo info);
    }
    public abstract record DamageInfo(float Damage, ThingBase? Thing, ActorBase? Actor, PlayerBase? Player);
    public record KillZoneDamageInfo(float Damage) : DamageInfo( Damage, null, null, null );
    public record BulletDamageInfo(Vector3 Point, Vector3 Direction, float Damage, ThingBase? Thing, ActorBase? Actor, PlayerBase? Player) : DamageInfo( Damage, Thing, Actor, Player );
}
