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
            bool IsFirePressed();
            bool IsAimPressed();
            bool IsInteractPressed(out GameObject? interactable);
            bool IsMovePressed(out Vector3 moveVector);
            bool IsLookPressed(out Vector3 lookTarget);
            bool IsJumpPressed();
            bool IsCrouchPressed();
            bool IsAcceleratePressed();
        }

        private bool fixedUpdateWasInvoked = false;
        private bool isJumpPressed = false;
        private bool isCrouchPressed = false;
        private bool isAcceleratePressed = false;

        // Objects
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
                var isMovePressed = Actions.IsMovePressed( out var moveVector );
                var isJumpPressed = this.isJumpPressed;
                var isCrouchPressed = this.isCrouchPressed;
                var isAcceleratePressed = this.isAcceleratePressed;
                Body.UpdatePosition( isMovePressed, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
            }
        }
        public void Update() {
            if (Actions != null && Actions.IsEnabled()) {
                if (fixedUpdateWasInvoked) {
                    fixedUpdateWasInvoked = false;
                    isJumpPressed = Actions.IsJumpPressed();
                    isCrouchPressed = Actions.IsCrouchPressed();
                    isAcceleratePressed = Actions.IsAcceleratePressed();
                } else {
                    isJumpPressed |= Actions.IsJumpPressed();
                    isCrouchPressed |= Actions.IsCrouchPressed();
                    isAcceleratePressed |= Actions.IsAcceleratePressed();
                }
                {
                    var isLookPressed = Actions.IsLookPressed( out var lookTarget );
                    Body.UpdateRotation( isLookPressed, lookTarget );
                }
                if (Actions.IsFirePressed()) {

                }
                if (Actions.IsAimPressed()) {

                }
                if (Actions.IsInteractPressed( out var interactable )) {

                }
            } else {
                isJumpPressed = false;
                isCrouchPressed = false;
                isAcceleratePressed = false;
            }
        }

    }
}
