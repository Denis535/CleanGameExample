#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class EnemyCharacter : Character {

        private EnemyContext Context { get; set; }

        // Awake
        public override void Awake() {
            base.Awake();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
            EntityFactory2.Gun( WeaponSlot );
        }
        public override void FixedUpdate() {
            PhysicsFixedUpdate();
            Context = GetContext( transform );
        }
        public override void Update() {
        }

        // Heleprs
        private static EnemyContext GetContext(Transform transform) {
            var mask = ~0 & ~LayerMask.GetMask( "Bullet" );
            var colliders = Physics.OverlapSphere( transform.position, 16, mask, QueryTriggerInteraction.Ignore );
            return new EnemyContext() {
                Player = colliders.Where( i => i.transform.parent == null ).Select( i => i.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
            };
        }

    }
    internal struct EnemyContext {
        public PlayerCharacter Player { get; init; }
    }
}
