#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerCharacter : Character {

        // Actions
        public IPlayerCharacterInputActions? Actions { get; set; }

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
            PhysicsFixedUpdate();
        }
        public override void Update() {
            if (Actions != null) {
                SetMovementInput( Actions.IsMovePressed( out var moveVector_ ), moveVector_, Actions.IsJumpPressed(), Actions.IsCrouchPressed(), Actions.IsAcceleratePressed() );
                if (Actions.IsFirePressed() || Actions.IsAimPressed()) {
                    SetLookInput( true, Actions.LookTarget );
                    PhysicsUpdate();
                } else {
                    if (Actions.IsMovePressed( out var moveVector )) {
                        SetLookInput( true, transform.position + moveVector );
                        PhysicsUpdate();
                    } else {
                        SetLookInput( false, Actions.LookTarget );
                        PhysicsUpdate();
                    }
                }
                if (Actions.IsFirePressed() || Actions.IsAimPressed()) {
                    LookAt( Actions.LookTarget );
                    AimAt( Actions.LookTarget );
                } else {
                    if (Actions.IsMovePressed( out _ )) {
                        LookAt( Actions.LookTarget );
                        AimAt( null );
                    } else {
                        LookAt( Actions.LookTarget );
                        AimAt( null );
                    }
                }
                if (Actions.IsFirePressed()) {
                    Weapon?.Fire();
                }
                if (Actions.IsAimPressed()) {

                }
                if (Actions.IsInteractPressed( out var interactable )) {
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
    // IPlayerCharacterInputActions
    public interface IPlayerCharacterInputActions {

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
