#nullable enable
namespace Project.Entities.Actors {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder( 100 )]
    public abstract class Actor : MonoBehaviour, IActor, IDamageable {

        public bool IsAlive { get; private set; } = true;
        //public event Action<DamageInfo>? OnDamageEvent;
        public event Action<DamageInfo>? OnDeathEvent;

        protected virtual void Awake() {
        }
        protected virtual void OnDestroy() {
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }
        protected virtual void LateUpdate() {
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
