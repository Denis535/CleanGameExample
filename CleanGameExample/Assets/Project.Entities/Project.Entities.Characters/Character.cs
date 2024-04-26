#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( CharacterBody ) )]
    [RequireComponent( typeof( CharacterView ) )]
    public class Character : EntityBase, CharacterBody.IInputActions {
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

        private bool isJumpPressed = false;
        private bool isCrouchPressed = false;
        private bool isAcceleratePressed = false;

        // View
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
            Body.UpdatePosition( this );
        }
        public void Update() {
            if (Actions != null && Actions.IsEnabled()) {
                isJumpPressed |= Actions.IsJumpPressed();
                isCrouchPressed |= Actions.IsCrouchPressed();
                isAcceleratePressed |= Actions.IsAcceleratePressed();
            } else {
                isJumpPressed = false;
                isCrouchPressed = false;
                isAcceleratePressed = false;
            }
            Body.UpdateRotation( this );
            if (Actions != null && Actions.IsEnabled()) {
                if (Actions.IsFirePressed()) {

                }
                if (Actions.IsAimPressed()) {

                }
                if (Actions.IsInteractPressed( out var interactable )) {

                }
            }
        }

        // CharacterBody.IInputActions
        bool CharacterBody.IInputActions.IsMovePressed(out Vector3 moveVector) {
            if (Actions != null && Actions.IsEnabled()) {
                return Actions.IsMovePressed( out moveVector );
            }
            moveVector = Vector3.zero;
            return false;
        }
        bool CharacterBody.IInputActions.IsLookPressed(out Vector3 lookTarget) {
            if (Actions != null && Actions.IsEnabled()) {
                return Actions.IsLookPressed( out lookTarget );
            }
            lookTarget = Vector3.zero;
            return false;
        }
        bool CharacterBody.IInputActions.IsJumpPressed() {
            try {
                return isJumpPressed;
            } finally {
                isJumpPressed = false;
            }
        }
        bool CharacterBody.IInputActions.IsCrouchPressed() {
            try {
                return isCrouchPressed;
            } finally {
                isCrouchPressed = false;
            }
        }
        bool CharacterBody.IInputActions.IsAcceleratePressed() {
            try {
                return isAcceleratePressed;
            } finally {
                isAcceleratePressed = false;
            }
        }

    }
}
