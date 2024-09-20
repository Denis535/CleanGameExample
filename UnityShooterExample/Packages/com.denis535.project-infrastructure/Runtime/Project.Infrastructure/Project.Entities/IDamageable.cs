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
        void Damage(DamageInfo info);
    }
    public static class IDamageableExtensions {

        public static void Damage(this Collider collider, DamageInfo info) {
            var damageable = collider.transform.root.GetComponent<IDamageable>();
            if (damageable != null && damageable != (IDamageable?) info.Actor) {
                damageable.Damage( info );
            }
        }

    }
    public abstract record DamageInfo(float Damage, ThingBase? Thing, ActorBase? Actor, PlayerBase? Player);
    public record KillZoneDamageInfo(float Damage) : DamageInfo( Damage, null, null, null );
    public record BulletDamageInfo(Vector3 Point, Vector3 Direction, float Damage, ThingBase? Thing, ActorBase? Actor, PlayerBase? Player) : DamageInfo( Damage, Thing, Actor, Player );
}
