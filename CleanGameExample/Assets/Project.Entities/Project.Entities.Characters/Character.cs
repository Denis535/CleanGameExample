#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( CharacterBody ) )]
    [RequireComponent( typeof( CharacterView ) )]
    public class Character : EntityBase {
        public interface IInputActions {
            bool IsEnabled();
            bool IsMovePressed(out Vector3 moveVector);
            bool IsLookPressed(out Vector3 lookTarget);
            bool IsJumpPressed();
            bool IsCrouchPressed();
            bool IsAcceleratePressed();
            bool IsFirePressed();
            bool IsAimPressed();
            bool IsInteractPressed(out GameObject? interactable);
        }

        private bool fixedUpdateWasInvoked;
        private bool isMovePressed;
        private Vector3 moveVector;
        private bool isJumpPressed;
        private bool isCrouchPressed;
        private bool isAcceleratePressed;

        // Components
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Actions
        public IInputActions? Actions { get; set; }

        // Awake
        public void Awake() {
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            fixedUpdateWasInvoked = true;
            if (Actions != null && Actions.IsEnabled()) {
                Body.MovePosition( isMovePressed, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
            }
        }
        public void Update() {
            if (Actions != null && Actions.IsEnabled()) {
                if (fixedUpdateWasInvoked) {
                    fixedUpdateWasInvoked = false;
                    isMovePressed = Actions.IsMovePressed( out moveVector );
                    isJumpPressed = Actions.IsJumpPressed();
                    isCrouchPressed = Actions.IsCrouchPressed();
                    isAcceleratePressed = Actions.IsAcceleratePressed();
                } else {
                    if (Actions.IsMovePressed( out var moveVector_ )) {
                        isMovePressed = true;
                        moveVector = Vector3.Max( moveVector, moveVector_ );
                    }
                    if (Actions.IsJumpPressed()) {
                        isJumpPressed = true;
                    }
                    if (Actions.IsCrouchPressed()) {
                        isCrouchPressed = true;
                    }
                    if (Actions.IsAcceleratePressed()) {
                        isAcceleratePressed = true;
                    }
                }
            } else {
                isMovePressed = false;
                moveVector = default;
                isJumpPressed = false;
                isCrouchPressed = false;
                isAcceleratePressed = false;
            }
            if (Actions != null && Actions.IsEnabled()) {
                {
                    var isLookPressed = Actions.IsLookPressed( out var lookTarget );
                    Body.MoveRotation( isLookPressed, lookTarget );
                }
                if (Actions.IsFirePressed()) {

                }
                if (Actions.IsAimPressed()) {

                }
                if (Actions.IsInteractPressed( out var interactable )) {
                    if (interactable != null && interactable.IsWeapon()) {
                        View.SetWeapon( interactable, out var prevWeapon );
                    } else {
                        View.SetWeapon( null, out var prevWeapon );
                    }
                }
            }
        }

    }
}
