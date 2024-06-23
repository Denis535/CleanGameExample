#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class EnemyCharacter : Character {
        private struct Environment_ {
            public PlayerCharacter? Player { get; init; }
        }

        // Environment
        private Environment_ Environment { get; set; }

        // Awake
        protected override void Awake() {
            base.Awake();
            Awake( Context.GetValue<EnemyCharacterFactory.Args>() );
        }
        private void Awake(EnemyCharacterFactory.Args args) {
        }
        protected override void OnDestroy() {
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
                    Weapon?.Fire( this );
                }
            }
        }

        // Heleprs
        private static Environment_ GetEnvironment(Transform transform) {
            return new Environment_() {
                Player = Utils.OverlapSphere( transform.position, 8 ).Select( i => i.transform.root.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
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
    public static class EnemyCharacterFactory {
        public record Args();

        private static readonly PrefabListHandle<EnemyCharacter> Prefabs = new PrefabListHandle<EnemyCharacter>( new[] {
            R.Project.Entities.Characters.Value_EnemyCharacter_Gray,
            R.Project.Entities.Characters.Value_EnemyCharacter_Red,
            R.Project.Entities.Characters.Value_EnemyCharacter_Green,
            R.Project.Entities.Characters.Value_EnemyCharacter_Blue
        } );

        public static void Initialize() {
            Prefabs.Load().Wait();
        }
        public static void Deinitialize() {
            Prefabs.Release();
        }

        public static EnemyCharacter Create(Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return GameObject.Instantiate<EnemyCharacter>( Prefabs.GetValues().GetRandom(), position, rotation );
            }
        }

    }
}
