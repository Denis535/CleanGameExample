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
        private ICharacterInputActions? Actions { get; set; }

        // Awake
        public void Awake() {
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // SetActions
        internal void SetActions(ICharacterInputActions? actions) {
            Actions = actions;
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
                    View.LookAt( lookTarget );
                    View.AimAt( lookTarget );
                } else {
                    if (Actions.IsMovePressed( out var moveVector )) {
                        Body.MoveRotation( true, transform.position + moveVector );
                        View.LookAt( null );
                        View.AimAt( null );
                    } else {
                        View.LookAt( lookTarget );
                        View.AimAt( null );
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
    // ICharacterInputActions
    internal interface ICharacterInputActions {

        bool IsEnabled { get; }

        void FixedUpdate();
        void Update();

        bool IsMovePressed(out Vector3 moveVector);
        bool IsLookPressed(out Vector3 lookTarget);
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed();
        bool IsAimPressed();
        bool IsInteractPressed(out GameObject? interactable);

    }
    internal abstract class CharacterInputActionsBase : ICharacterInputActions {

        private bool fixedUpdateWasInvoked;
        private bool isMovePressedCached;
        private Vector3 moveVectorCached;
        private bool isJumpPressedCached;
        private bool isCrouchPressedCached;
        private bool isAcceleratePressedCached;

        public abstract bool IsEnabled { get; }

        public CharacterInputActionsBase() {
        }

        public void FixedUpdate() {
            Assert.Operation.Message( $"Method 'FixedUpdate' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            fixedUpdateWasInvoked = true;
        }
        public void Update() {
            // accumulate actions that happened between fixed updates
            Assert.Operation.Message( $"Method 'Update' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (IsEnabled) {
                if (fixedUpdateWasInvoked) {
                    isMovePressedCached = IsMovePressedInternal( out moveVectorCached );
                    isJumpPressedCached = IsJumpPressedInternal();
                    isCrouchPressedCached = IsCrouchPressedInternal();
                    isAcceleratePressedCached = IsAcceleratePressedInternal();
                } else {
                    if (IsMovePressedInternal( out var moveVector )) {
                        isMovePressedCached = true;
                        moveVectorCached = Vector3.Max( moveVectorCached, moveVector );
                    }
                    if (IsJumpPressedInternal()) {
                        isJumpPressedCached = true;
                    }
                    if (IsCrouchPressedInternal()) {
                        isCrouchPressedCached = true;
                    }
                    if (IsAcceleratePressedInternal()) {
                        isAcceleratePressedCached = true;
                    }
                }
            } else {
                Clear();
            }
        }

        private void Clear() {
            fixedUpdateWasInvoked = false;
            isMovePressedCached = false;
            moveVectorCached = default;
            isJumpPressedCached = false;
            isCrouchPressedCached = false;
            isAcceleratePressedCached = false;
        }

        public bool IsMovePressed(out Vector3 moveVector) {
            if (IsEnabled) {
                if (Time.inFixedTimeStep) { moveVector = moveVectorCached; return isMovePressedCached; }
                return IsMovePressedInternal( out moveVector );
            }
            moveVector = default;
            return false;
        }
        public bool IsLookPressed(out Vector3 lookTarget) {
            return IsLookPressedInternal( out lookTarget );
        }
        public bool IsJumpPressed() {
            if (IsEnabled) {
                if (Time.inFixedTimeStep) return isJumpPressedCached;
                return IsJumpPressedInternal();
            }
            return false;
        }
        public bool IsCrouchPressed() {
            if (IsEnabled) {
                if (Time.inFixedTimeStep) return isCrouchPressedCached;
                return IsCrouchPressedInternal();
            }
            return false;
        }
        public bool IsAcceleratePressed() {
            if (IsEnabled) {
                if (Time.inFixedTimeStep) return isAcceleratePressedCached;
                return IsAcceleratePressedInternal();
            }
            return false;
        }
        public bool IsFirePressed() {
            return IsFirePressedInternal();
        }
        public bool IsAimPressed() {
            return IsAimPressedInternal();
        }
        public bool IsInteractPressed(out GameObject? interactable) {
            return IsInteractPressedInternal( out interactable );
        }

        protected abstract bool IsMovePressedInternal(out Vector3 moveVector);
        protected abstract bool IsLookPressedInternal(out Vector3 lookTarget);
        protected abstract bool IsJumpPressedInternal();
        protected abstract bool IsCrouchPressedInternal();
        protected abstract bool IsAcceleratePressedInternal();
        protected abstract bool IsFirePressedInternal();
        protected abstract bool IsAimPressedInternal();
        protected abstract bool IsInteractPressedInternal(out GameObject? interactable);

    }
}
