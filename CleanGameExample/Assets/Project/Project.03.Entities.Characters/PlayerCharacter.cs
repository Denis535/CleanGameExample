#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class PlayerCharacter {
        public static class Factory {

            private static readonly PrefabListHandle<PlayerCharacter> Prefabs = new PrefabListHandle<PlayerCharacter>( new[] {
                R.Project.Entities.Characters.Value_PlayerCharacter_Gray,
                R.Project.Entities.Characters.Value_PlayerCharacter_Red,
                R.Project.Entities.Characters.Value_PlayerCharacter_Green,
                R.Project.Entities.Characters.Value_PlayerCharacter_Blue
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static PlayerCharacter Create(PlayerCharacterType type, Vector3 position, Quaternion rotation) {
                return GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) type ], position, rotation );
            }

        }
    }
    public partial class PlayerCharacter : Character {
        
        public IPlayer Player { get; set; } = default!;

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        protected override void Start() {
        }
        protected override void FixedUpdate() {
        }
        protected override void Update() {
            if (IsAlive) {
                Body.Move( Player.GetMoveVector(), Player.IsJumpPressed(), Player.IsCrouchPressed(), Player.IsAcceleratePressed() );
                Body.LookAt( Player.GetBodyTarget() );
                View.HeadAt( Player.GetHeadTarget() );
                View.WeaponAt( Player.GetWeaponTarget() );
                if (Player.IsAimPressed()) {

                }
                if (Player.IsFirePressed()) {
                    Weapon?.Fire( this );
                }
                if (Player.IsInteractPressed( out var interactable )) {
                    if (interactable is IWeapon weapon) {
                        Weapon = weapon;
                    } else {
                        Weapon = null;
                    }
                }
            }
        }

    }
    public enum PlayerCharacterType {
        Gray,
        Red,
        Green,
        Blue
    }
}
