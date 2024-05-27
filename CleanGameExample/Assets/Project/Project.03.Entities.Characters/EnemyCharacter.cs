#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Things;
    using UnityEngine;

    public class EnemyCharacter : Character {
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
            Awake( Context.GetValue<EnemyCharacterFactory.Args>() );
        }
        private void Awake(EnemyCharacterFactory.Args args) {
            Game = args.Game;
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
            SetWeapon( GunFactory.Create() );
        }
        public override void FixedUpdate() {
            base.FixedUpdate();
            Environment = GetEnvironment( transform );
        }
        public override void Update() {
            if (IsAlive) {
                RotateAt( GetTarget( Environment ) );
                LookAt( GetLookTarget( Environment ) );
                AimAt( GetAimTarget( Environment ) );
                if (Environment.Player != null && Environment.Player.IsAlive) {
                    if (Weapon != null) {
                        if (Weapon is Gun gun) {
                            gun.Fire( this, null );
                        } else {
                            throw Exceptions.Internal.NotSupported( $"Weapon {Weapon} is not supported" );
                        }
                    }
                }
            }
        }

        // Heleprs
        private static Environment_ GetEnvironment(Transform transform) {
            return new Environment_() {
                Player = Physics2.OverlapSphere( transform.position, 8 ).Select( i => i.transform.root.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
            };
        }
        // Heleprs
        private static Vector3? GetTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.75f;
                }
                return null;
            }
            return null;
        }
        private static Vector3? GetLookTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.75f;
                }
                return environment.Player.transform.position;
            }
            return null;
        }
        private static Vector3? GetAimTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.75f;
                }
                return null;
            }
            return null;
        }

    }
}
