#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters.Inventory;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Character : EntityBase<CharacterBody, CharacterView> {

        private bool fixedUpdateWasInvoked;

        // Body
        protected override CharacterBody Body { get; set; } = default!;
        // View
        protected override CharacterView View { get; set; } = default!;
        // Weapon
        private Weapon? Weapon => View.Weapon?.RequireComponent<Weapon>();
        // Actions
        private ICharacterInputActions? Actions { get; set; }

        // Awake
        public override void Awake() {
            Body = new CharacterBody( gameObject );
            View = new CharacterView( gameObject );
        }
        public override void OnDestroy() {
            Body.Dispose();
            View.Dispose();
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
                    if (Actions.IsMovePressed( out _ )) {
                        View.LookAt( Actions.LookTarget );
                        View.AimAt( Actions.LookTarget );
                    } else {
                        View.LookAt( Actions.LookTarget );
                        View.AimAt( Actions.LookTarget );
                    }
                }
                if (Actions.IsFirePressed()) {
                    Weapon?.Fire();
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
