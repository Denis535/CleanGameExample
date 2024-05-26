#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Things;
    using UnityEngine;

    public class EnemyCharacter : Character {
        public record Args(IGame Game);
        private struct Environment_ {
            public PlayerCharacter? Player { get; init; }
        }

        // Game
        private IGame Game { get; set; } = default!;
        // Environment
        private Environment_ Environment { get; set; }

        // Awake
        public override void Awake() {
            base.Awake();
            var args = Context.GetValue<Args>();
            Game = args.Game;
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
            ThingFactory.Gun( WeaponSlot );
        }
        public override void FixedUpdate() {
            base.FixedUpdate();
            Environment = GetEnvironment( transform );
        }
        public override void Update() {
            if (IsAlive) {
                if (Environment.Player != null && Environment.Player.IsAlive) {
                    var target = GetTarget( Environment.Player );
                    RotateAt( target );
                    LookAt( target );
                    AimAt( target );
                    Weapon?.Fire();
                } else {
                    RotateAt( null );
                    LookAt( null );
                    AimAt( null );
                }
            }
        }

        // OnDamage
        protected override void OnDamage(float damage, Vector3 point, Vector3 direction) {
            base.OnDamage( damage, point, direction );
        }

        // OnDead
        protected override void OnDead(Vector3 point, Vector3 direction) {
            base.OnDead( point, direction );
        }

        // Heleprs
        private static Environment_ GetEnvironment(Transform transform) {
            return new Environment_() {
                Player = Physics2.OverlapSphere( transform.position, 8 ).Select( i => i.transform.root.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
            };
        }
        private static Vector3 GetTarget(PlayerCharacter player) {
            return player.transform.position + Vector3.up * 1.75f;
        }

    }
}
