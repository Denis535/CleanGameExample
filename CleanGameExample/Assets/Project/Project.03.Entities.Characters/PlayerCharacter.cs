#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    public class PlayerCharacter : Character {
        public record Args(IGame game, IPlayer Player);

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
                RotateAt( GetTarget( Player, this ) );
                LookAt( GetLookTarget( Player ) );
                AimAt( GetAimTarget( Player ) );
                if (Player.IsAimPressed()) {
                    
                }
                if (Player.IsFirePressed()) {
                    Weapon?.Fire();
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
}
