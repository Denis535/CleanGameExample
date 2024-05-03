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

        // Components
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Actions
        private InputActionsWrapper? Actions { get; set; }

        // Awake
        public void Awake() {
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // SetActions
        internal void SetActions(IInputActions? actions) {
            Actions = actions != null ? new InputActionsWrapper( actions ) : null;
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            if (Actions != null) {
                Actions.FixedUpdate();
                Body.MovePosition( Actions.IsMovePressed( out var moveVector ), moveVector, Actions.IsJumpPressed(), Actions.IsCrouchPressed(), Actions.IsAcceleratePressed() );
            }
        }
        public void Update() {
            if (Actions != null) {
                Actions.Update();
                if (Actions.IsLookPressed( out var lookTarget )) {
                    Body.MoveRotation( true, lookTarget );
                } else {
                    if (Actions.IsMovePressed( out var moveVector )) {
                        lookTarget = transform.position + moveVector * 128f + Vector3.up * 1.75f;
                        Body.MoveRotation( true, lookTarget );
                    }
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
    internal interface IInputActions {
        bool IsEnabled { get; }
        bool IsMovePressed(out Vector3 moveVector);
        bool IsLookPressed(out Vector3 lookTarget);
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed();
        bool IsAimPressed();
        bool IsInteractPressed(out GameObject? interactable);
    }
    internal class InputActionsWrapper {

        private readonly IInputActions @base;
        private bool fixedUpdateWasInvoked;
        private bool isMovePressed;
        private Vector3 moveVector;
        private bool isJumpPressed;
        private bool isCrouchPressed;
        private bool isAcceleratePressed;

        public bool IsEnabled => @base.IsEnabled;

        public InputActionsWrapper(IInputActions @base) {
            this.@base = @base;
        }

        public void FixedUpdate() {
            Assert.Operation.Message( $"Method 'FixedUpdate' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            this.fixedUpdateWasInvoked = true;
        }
        public void Update() {
            // accumulate input actions that happened between fixed updates
            Assert.Operation.Message( $"Method 'Update' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (this.fixedUpdateWasInvoked) {
                this.fixedUpdateWasInvoked = false;
                this.isMovePressed = @base.IsMovePressed( out var moveVector );
                this.moveVector = moveVector;
                this.isJumpPressed = @base.IsJumpPressed();
                this.isCrouchPressed = @base.IsCrouchPressed();
                this.isAcceleratePressed = @base.IsAcceleratePressed();
            } else {
                if (@base.IsMovePressed( out var moveVector )) {
                    this.isMovePressed = true;
                    this.moveVector = Vector3.Max( this.moveVector, moveVector );
                }
                if (@base.IsJumpPressed()) {
                    this.isJumpPressed = true;
                }
                if (@base.IsCrouchPressed()) {
                    this.isCrouchPressed = true;
                }
                if (@base.IsAcceleratePressed()) {
                    this.isAcceleratePressed = true;
                }
            }
        }

        public bool IsMovePressed(out Vector3 moveVector) {
            moveVector = this.moveVector;
            return isMovePressed;
        }
        public bool IsLookPressed(out Vector3 lookTarget) {
            return @base.IsLookPressed( out lookTarget );
        }
        public bool IsJumpPressed() {
            return isJumpPressed;
        }
        public bool IsCrouchPressed() {
            return isCrouchPressed;
        }
        public bool IsAcceleratePressed() {
            return isAcceleratePressed;
        }
        public bool IsFirePressed() {
            return @base.IsFirePressed();
        }
        public bool IsAimPressed() {
            return @base.IsAimPressed();
        }
        public bool IsInteractPressed(out GameObject? interactable) {
            return @base.IsInteractPressed( out interactable );
        }

    }
}
