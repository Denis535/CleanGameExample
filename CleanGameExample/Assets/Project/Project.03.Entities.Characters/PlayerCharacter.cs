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

            public static PlayerCharacter Create(Vector3 position, Quaternion rotation, PlayerCharacterType type) {
                return GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) type ], position, rotation );
            }

        }
    }
    public partial class PlayerCharacter : PlayableCharacter {

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
                if (Input != null) {
                    MoveableBody.Move( Input.GetMoveVector(), Input.IsJumpPressed(), Input.IsCrouchPressed(), Input.IsAcceleratePressed() );
                    MoveableBody.LookAt( Input.GetBodyTarget() );
                    Head.LookAt( Input.GetHeadTarget() );
                    WeaponSlot.LookAt( Input.GetWeaponTarget() );
                    if (Input.IsAimPressed()) {

                    }
                    if (Input.IsFirePressed()) {
                        WeaponSlot.Weapon?.Fire( this, null );
                    }
                    if (Input.IsInteractPressed( out var interactable )) {
                        if (interactable is Weapon weapon) {
                            WeaponSlot.Weapon = weapon;
                        } else {
                            WeaponSlot.Weapon = null;
                        }
                    }
                }
            }
        }

    }
}
