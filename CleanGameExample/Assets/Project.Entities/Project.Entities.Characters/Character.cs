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

        private bool fixedUpdateWasInvoked;

        // Components
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Actions
        private ICharacterInputActions? Actions { get; set; }

        // Awake
        public override void Awake() {
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public override void OnDestroy() {
        }

        // SetActions
        internal void SetActions(ICharacterInputActions? actions) {
            Actions = actions;
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            fixedUpdateWasInvoked = true;
            if (Actions != null) {
                Body.UpdatePosition();
            }
        }
        public void Update() {
            if (Actions != null) {
                Body.SetUp( ref fixedUpdateWasInvoked, Actions.IsMovePressed( out var moveVector_ ), moveVector_, Actions.IsJumpPressed(), Actions.IsCrouchPressed(), Actions.IsAcceleratePressed() );
                if (Actions.IsFirePressed() || Actions.IsAimPressed()) {
                    Body.SetUp( true, Actions.LookTarget );
                    Body.UpdateRotation();
                } else {
                    if (Actions.IsMovePressed( out var moveVector )) {
                        Body.SetUp( true, transform.position + moveVector );
                        Body.UpdateRotation();
                    } else {
                        Body.SetUp( false, Actions.LookTarget );
                        Body.UpdateRotation();
                    }
                }
                if (Actions.IsFirePressed() || Actions.IsAimPressed()) {
                    View.LookAt( Actions.LookTarget );
                    View.AimAt( Actions.LookTarget );
                } else {
                    if (Actions.IsMovePressed( out var moveVector )) {
                        View.LookAt( null );
                        View.AimAt( null );
                    } else {
                        View.LookAt( Actions.LookTarget );
                        View.AimAt( Actions.LookTarget );
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
