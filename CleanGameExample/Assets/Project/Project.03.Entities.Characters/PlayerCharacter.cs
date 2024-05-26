#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    public class PlayerCharacter : Character {
        public record Args(IPlayer Player);

        // Player
        public IPlayer Player { get; private set; } = default!;

        // Awake
        public override void Awake() {
            base.Awake();
            var args = Context.GetValue<Args>();
            Player = args.Player;
        }
        public override void OnDestroy() {
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
                if (Player.IsFirePressed() || Player.IsAimPressed()) {
                    RotateAt( Player.LookTarget );
                } else {
                    if (Player.IsMovePressed( out var moveVector )) {
                        RotateAt( transform.position + moveVector );
                    } else {
                        RotateAt( null );
                    }
                }
                if (Player.IsFirePressed() || Player.IsAimPressed()) {
                    LookAt( Player.LookTarget );
                    AimAt( Player.LookTarget );
                } else {
                    if (Player.IsMovePressed( out _ )) {
                        LookAt( Player.LookTarget );
                        AimAt( null );
                    } else {
                        LookAt( Player.LookTarget );
                        AimAt( null );
                    }
                }
                if (Player.IsFirePressed()) {
                    Weapon?.Fire();
                }
                if (Player.IsAimPressed()) {

                }
                if (Player.IsInteractPressed( out var interactable )) {
                    var weapon = interactable?.GetComponent<Weapon>();
                    if (weapon != null) {
                        SetWeapon( weapon );
                    } else {
                        SetWeapon( null );
                    }
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

    }
    // IPlayer
    public interface IPlayer {

        string Name { get; }
        PlayerCharacterEnum CharacterEnum { get; }

        bool IsEnabled { get; }
        Vector3 LookTarget { get; }

        bool IsMovePressed(out Vector3 moveVector);
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed();
        bool IsAimPressed();
        bool IsInteractPressed(out GameObject? interactable);

    }
    // PlayerCharacterEnum
    public enum PlayerCharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
