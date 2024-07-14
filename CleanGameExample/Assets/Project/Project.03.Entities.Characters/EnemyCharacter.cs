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
                var result = GameObject.Instantiate<EnemyCharacter>( Prefabs.GetValues().GetRandom(), position, rotation );
                return result;
            }

        }
    }
    public partial class EnemyCharacter : NonPlayableCharacter {
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
            WeaponSlot.Weapon = Gun.Factory.Create( null );
        }
        protected override void FixedUpdate() {
            Environment = GetEnvironment( transform );
        }
        protected override void Update() {
            if (IsAlive) {
                Facade.Move( Vector3.zero, false, false, false );
                Facade.LookAt( GetBodyTarget( Environment ) );
                Head.LookAt( GetHeadTarget( Environment ) );
                WeaponSlot.LookAt( GetWeaponTarget( Environment ) );
                if (Environment.Player != null && Environment.Player.IsAlive) {
                    WeaponSlot.Weapon?.Fire( this, null );
                }
            }
        }

        private static Environment_ GetEnvironment(Transform transform) {
            var mask = ~(Masks.Entity_Approximate | Masks.Trivial);
            return new Environment_() {
                Player = Utils.OverlapSphere( transform.position, 8, mask, QueryTriggerInteraction.Ignore ).Select( i => i.transform.root.GetComponent<PlayerCharacter>() ).FirstOrDefault( i => i != null )
            };
        }
        private static Vector3? GetBodyTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.5f;
                }
                return null;
            }
            return null;
        }
        private static Vector3? GetHeadTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.5f;
                }
                return environment.Player.transform.position;
            }
            return null;
        }
        private static Vector3? GetWeaponTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.5f;
                }
                return null;
            }
            return null;
        }

    }
}
