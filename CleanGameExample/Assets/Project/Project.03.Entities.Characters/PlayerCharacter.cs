#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    public class PlayerCharacter : Character {

        // Input
        public IPlayerCharacterInput? Input { get; set; }

        // Awake
        public override void Awake() {
            base.Awake();
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
            if (IsAlive && Input != null) {
                SetMovementInput( Input.IsMovePressed( out var moveVector_ ), moveVector_, Input.IsJumpPressed(), Input.IsCrouchPressed(), Input.IsAcceleratePressed() );
                if (Input.IsFirePressed() || Input.IsAimPressed()) {
                    RotateAt( Input.LookTarget );
                } else {
                    if (Input.IsMovePressed( out var moveVector )) {
                        RotateAt( transform.position + moveVector );
                    } else {
                        RotateAt( null );
                    }
                }
                if (Input.IsFirePressed() || Input.IsAimPressed()) {
                    LookAt( Input.LookTarget );
                    AimAt( Input.LookTarget );
                } else {
                    if (Input.IsMovePressed( out _ )) {
                        LookAt( Input.LookTarget );
                        AimAt( null );
                    } else {
                        LookAt( Input.LookTarget );
                        AimAt( null );
                    }
                }
                if (Input.IsFirePressed()) {
                    Weapon?.Fire();
                }
                if (Input.IsAimPressed()) {

                }
                if (Input.IsInteractPressed( out var interactable )) {
                    if (interactable != null && interactable.IsWeapon()) {
                        SetWeapon( interactable.RequireComponent<Weapon>() );
                    } else {
                        SetWeapon( null );
                    }
                }
            }
        }

    }
    // PlayerCharacterEnum
    public enum PlayerCharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
    // IPlayerCharacterInput
    public interface IPlayerCharacterInput {

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
}
