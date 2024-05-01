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
            if (Actions != null && Actions.IsEnabled()) {
                {
                    var isMovePressed = Actions.IsMovePressed( out var moveVector );
                    var isJumpPressed = Actions.IsJumpPressed();
                    var isCrouchPressed = Actions.IsCrouchPressed();
                    var isAcceleratePressed = Actions.IsAcceleratePressed();
                    Body.FixedUpdatePosition( isMovePressed, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
                }
                {
                    var isLookPressed = Actions.IsLookPressed( out var lookTarget );
                    Body.FixedUpdateRotation( isLookPressed, lookTarget );
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
        public void Update() {
        }

    }
}
