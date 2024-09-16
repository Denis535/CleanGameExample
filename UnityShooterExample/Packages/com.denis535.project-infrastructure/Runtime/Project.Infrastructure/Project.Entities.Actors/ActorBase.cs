#nullable enable
namespace Project.Entities.Actors {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Project.Entities.Things;

    [DefaultExecutionOrder( 100 )]
    public abstract class ActorBase : MonoBehaviour, IDamageable {

        public bool IsAlive { get; private set; } = true;
        //public event Action<DamageInfo>? OnDamageEvent;
        public event Action<DamageInfo>? OnDeathEvent;

        protected virtual void Awake() {
        }
        protected virtual void OnDestroy() {
        }

        void IDamageable.OnDamage(DamageInfo info) => OnDamage( info );
        protected virtual void OnDamage(DamageInfo info) {
            if (IsAlive) {
                IsAlive = false;
                OnDeath( info );
                //OnDamageEvent?.Invoke( info );
                OnDeathEvent?.Invoke( info );
            }
        }
        protected virtual void OnDeath(DamageInfo info) {
        }

    }
}
