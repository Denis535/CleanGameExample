#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class EnemyCharacter {
        public static class Factory {

            private static readonly PrefabListHandle<EnemyCharacter> Prefabs = new PrefabListHandle<EnemyCharacter>( new[] {
                R.Project.Entities.Characters.Value_EnemyCharacter_Gray,
                R.Project.Entities.Characters.Value_EnemyCharacter_Red,
                R.Project.Entities.Characters.Value_EnemyCharacter_Green,
                R.Project.Entities.Characters.Value_EnemyCharacter_Blue
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static EnemyCharacter Create(Vector3 position, Quaternion rotation) {
                return GameObject.Instantiate<EnemyCharacter>( Prefabs.GetValues().GetRandom(), position, rotation );
            }

        }
    }
    public partial class EnemyCharacter : Character {
        private struct Environment_ {
            public PlayerCharacter? Player { get; init; }
        }
        private Environment_ Environment { get; set; }

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        protected override void Start() {
            Weapon = Gun.Factory.Create( null );
        }
        protected override void FixedUpdate() {
            Environment = GetEnvironment( transform );
        }
        protected override void Update() {
            if (IsAlive) {
                Body.Move( Vector3.zero, false, false, false );
                Body.LookAt( GetBodyTarget( Environment ) );
                View.HeadAt( GetHeadTarget( Environment ) );
                View.WeaponAt( GetWeaponTarget( Environment ) );
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
        private static Vector3? GetBodyTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.75f;
                }
                return null;
            }
            return null;
        }
        private static Vector3? GetHeadTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.75f;
                }
                return environment.Player.transform.position;
            }
            return null;
        }
        private static Vector3? GetWeaponTarget(Environment_ environment) {
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
