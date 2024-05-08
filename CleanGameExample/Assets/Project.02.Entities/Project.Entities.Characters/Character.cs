#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Character : PhysicsCharacter {

        // Body
        private Transform Body { get; set; } = default!;
        // Head
        private Transform Head { get; set; } = default!;
        // WeaponSlot
        private Transform WeaponSlot { get; set; } = default!;
        // Weapon
        private Weapon? Weapon => WeaponSlot.childCount > 0 ? WeaponSlot.GetChild( 0 )?.gameObject?.RequireComponent<Weapon>() : null;
        // Actions
        private ICharacterInputActions? Actions { get; set; }

        // Awake
        public override void Awake() {
            base.Awake();
            Body = gameObject.transform.Require( "Body" );
            Head = gameObject.transform.Require( "Head" );
            WeaponSlot = gameObject.transform.Require( "WeaponSlot" );
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // SetActions
        internal void SetActions(ICharacterInputActions? actions) {
            Actions = actions;
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            PhysicsFixedUpdate();
        }
        public void Update() {
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
                        AimAt( Actions.LookTarget );
                    } else {
                        LookAt( Actions.LookTarget );
                        AimAt( Actions.LookTarget );
                    }
                }
                if (Actions.IsFirePressed()) {
                    Weapon?.Fire();
                }
                if (Actions.IsAimPressed()) {

                }
                if (Actions.IsInteractPressed( out var interactable )) {
                    if (interactable != null && interactable.IsWeapon()) {
                        SetWeapon( interactable.RequireComponent<Weapon>(), out var prevWeapon );
                    } else {
                        SetWeapon( null, out var prevWeapon );
                    }
                }
            }
        }

        // LookAt
        private bool LookAt(Vector3? target) {
            var rotation = Head.localRotation;
            if (target != null) {
                Head.localRotation = Quaternion.identity;
                var direction = Head.InverseTransformPoint( target.Value );
                var rotation2 = GetHeadRotation( direction );
                if (rotation2 != null) {
                    Head.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    Head.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                Head.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }

        // SetWeapon
        private void SetWeapon(Weapon? weapon, out Weapon? prevWeapon) {
            prevWeapon = Weapon;
            SetWeapon( weapon );
        }
        private void SetWeapon(Weapon? weapon) {
            if (Weapon != null) {
                Weapon.transform.parent = null;
            }
            if (weapon != null) {
                weapon.transform.parent = WeaponSlot;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
            }
        }

        // AimAt
        private bool AimAt(Vector3? target) {
            var rotation = WeaponSlot.localRotation;
            if (target != null) {
                WeaponSlot.localRotation = Quaternion.identity;
                var direction = WeaponSlot.InverseTransformPoint( target.Value );
                var rotation2 = GetWeaponRotation( direction );
                if (rotation2 != null) {
                    WeaponSlot.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    WeaponSlot.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                WeaponSlot.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }

        // Helpers
        private static Quaternion? GetHeadRotation(Vector3 direction) {
            var rotation = Quaternion.LookRotation( direction );
            var angles = rotation.eulerAngles;
            if (angles.x > 180) angles.x -= 360;
            if (angles.y > 180) angles.y -= 360;
            if (angles.y >= -80 && angles.y <= 80) {
                angles.x = Mathf.Clamp( angles.x, -80, 80 );
                angles.y = Mathf.Clamp( angles.y, -80, 80 );
                return Quaternion.Euler( angles );
            }
            return null;
        }
        private static Quaternion? GetWeaponRotation(Vector3 direction) {
            var rotation = Quaternion.LookRotation( direction );
            var angles = rotation.eulerAngles;
            if (angles.x > 180) angles.x -= 360;
            if (angles.y > 180) angles.y -= 360;
            if (angles.y >= -80 && angles.y <= 80) {
                angles.y = Mathf.Clamp( angles.y, -80, 80 );
                return Quaternion.Euler( angles );
            }
            return null;
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
