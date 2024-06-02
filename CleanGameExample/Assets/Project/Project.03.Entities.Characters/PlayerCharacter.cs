#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    public class PlayerCharacter : Character {

        // Player
        public IPlayer Player { get; set; } = default!;

        // Awake
        protected override void Awake() {
            base.Awake();
            Awake( Context.GetValue<PlayerCharacterFactory.Args>() );
        }
        private void Awake(PlayerCharacterFactory.Args args) {
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
        }
        public override void FixedUpdate() {
            base.FixedUpdate();
        }
        public override void Update() {
            if (IsAlive) {
                SetMovementInput( Player.IsMovePressed( out var moveVector_ ), moveVector_, Player.IsJumpPressed(), Player.IsCrouchPressed(), Player.IsAcceleratePressed() );
                RotateAt( GetTarget( Player, this ) );
                LookAt( GetLookTarget( Player ) );
                AimAt( GetAimTarget( Player ) );
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

        // Helpers
        private static Vector3? GetTarget(IPlayer player, PlayerCharacter character) {
            if (player.IsAimPressed() || player.IsFirePressed()) {
                return player.GetLookTarget();
            }
            if (player.IsMovePressed( out var moveVector )) {
                return character.transform.position + moveVector;
            }
            return null;
        }
        private static Vector3? GetLookTarget(IPlayer player) {
            if (player.IsAimPressed() || player.IsFirePressed()) {
                return player.GetLookTarget();
            }
            if (player.IsMovePressed( out _ )) {
                return player.GetLookTarget();
            }
            return player.GetLookTarget();
        }
        private static Vector3? GetAimTarget(IPlayer player) {
            if (player.IsAimPressed() || player.IsFirePressed()) {
                return player.GetLookTarget();
            }
            if (player.IsMovePressed( out _ )) {
                return null;
            }
            return null;
        }

    }
    public enum PlayerCharacterKind {
        Gray,
        Red,
        Green,
        Blue
    }
}
