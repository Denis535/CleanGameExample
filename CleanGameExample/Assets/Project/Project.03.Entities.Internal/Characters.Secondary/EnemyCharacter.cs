#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class EnemyCharacter : Character {

        // Context
        private EnemyCharacterContext Context { get; set; }

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
            if (Context.Player != null) {
                var target = Context.Player.transform.position + Vector3.up * 1.75f;
                SetLookInput( true, target );
                PhysicsUpdate();
                LookAt( target );
                AimAt( target );
                //Weapon?.Fire();
            } else {
                SetLookInput( false, LookTarget );
                PhysicsUpdate();
                LookAt( null );
                AimAt( null );
            }
        }

        // Heleprs
        private static EnemyCharacterContext GetContext(Transform transform) {
            var mask = ~0 & ~LayerMask2.BulletMask;
            var colliders = Physics.OverlapSphere( transform.position, 16, mask, QueryTriggerInteraction.Ignore );
            return new EnemyCharacterContext() {
                Player = colliders.Select( i => i.transform.root.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
            };
        }

    }
    internal struct EnemyCharacterContext {
        public PlayerCharacter? Player { get; init; }
    }
}
