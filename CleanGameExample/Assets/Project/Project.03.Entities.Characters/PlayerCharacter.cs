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

        // Player
        public IPlayer Player { get; set; } = default!;

        // Awake
        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
        }
        public override void FixedUpdate() {
            if (IsAlive) {
                MoveableBody.FixedUpdate2();
            }
        }
        public override void Update() {
            if (IsAlive) {
                MoveableBody.Update2( Player.IsMovePressed( out var moveVector ), moveVector, Player.GetLookTarget(), Player.IsJumpPressed(), Player.IsCrouchPressed(), Player.IsAcceleratePressed() );
                HeadAt( Player.GetHeadTarget() );
                AimAt( Player.GetAimTarget() );
                if (Player.IsAimPressed()) {

                }
                if (Player.IsFirePressed()) {
                    Weapon?.Fire( this );
                }
                if (Player.IsInteractPressed( out var interactable )) {
                    if (interactable is IWeapon weapon) {
                        SetWeapon( weapon );
                    } else {
                        SetWeapon( null );
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
