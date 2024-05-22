#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Things;
    using UnityEngine;

    public class EnemyCharacter : Character {
        private struct Context_ {
            public PlayerCharacter? Player { get; init; }
        }

        // Context
        private Context_ Context { get; set; }

        // Awake
        public override void Awake() {
            base.Awake();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
            ThingFactory.Gun( WeaponSlot );
        }
        public override void FixedUpdate() {
            PhysicsFixedUpdate();
            Context = GetContext( transform );
        }
        public override void Update() {
            if (IsAlive) {
                if (Context.Player != null) {
                    var target = Context.Player.transform.position + Vector3.up * 1.75f;
                    SetLookInput( true, target );
                    PhysicsUpdate();
                    LookAt( target );
                    AimAt( target );
                    Weapon?.Fire();
                } else {
                    SetLookInput( false, LookTarget );
                    PhysicsUpdate();
                    LookAt( null );
                    AimAt( null );
                }
            }
        }

        // Heleprs
        private static Context_ GetContext(Transform transform) {
            var colliders = Physics2.OverlapSphere( transform.position, 8 );
            return new Context_() {
                Player = colliders.Select( i => i.transform.root.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
            };
        }

    }
}
