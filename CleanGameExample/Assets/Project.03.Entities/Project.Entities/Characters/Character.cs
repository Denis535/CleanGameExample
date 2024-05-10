#nullable enable
namespace Project.Entities {
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
        public Weapon? Weapon => GetWeapon( WeaponSlot );
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
        public void SetActions(ICharacterInputActions? actions) {
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
                    LookAt( Head, Actions.LookTarget );
                    AimAt( WeaponSlot, Actions.LookTarget );
                } else {
                    if (Actions.IsMovePressed( out _ )) {
                        LookAt( Head, Actions.LookTarget );
                        AimAt( WeaponSlot, Actions.LookTarget );
                    } else {
                        LookAt( Head, Actions.LookTarget );
                        AimAt( WeaponSlot, Actions.LookTarget );
                    }
                }
                if (Actions.IsFirePressed()) {
                    Weapon?.Fire();
                }
                if (Actions.IsAimPressed()) {

                }
                if (Actions.IsInteractPressed( out var interactable )) {
                    if (interactable != null && interactable.IsWeapon()) {
                        SetWeapon( WeaponSlot, interactable.RequireComponent<Weapon>() );
                    } else {
                        SetWeapon( WeaponSlot, null );
                    }
                }
            }
        }

        // Helpers
        private static Weapon? GetWeapon(Transform transform) {
            return transform.childCount > 0 ? transform.GetChild( 0 )?.gameObject.RequireComponent<Weapon>() : null;
        }
        private static void SetWeapon(Transform transform, Weapon? weapon) {
            transform.DetachChildren();
            if (weapon != null) {
                weapon.transform.parent = transform;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
            }
        }
        // Helpers
        private static bool LookAt(Transform transform, Vector3? target) {
            var rotation = transform.localRotation;
            if (target != null) {
                transform.localRotation = Quaternion.identity;
                var direction = transform.InverseTransformPoint( target.Value );
                var rotation2 = GetHeadRotation( direction );
                if (rotation2 != null) {
                    transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }
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
        // Helpers
        private static bool AimAt(Transform transform, Vector3? target) {
            var rotation = transform.localRotation;
            if (target != null) {
                transform.localRotation = Quaternion.identity;
                var direction = transform.InverseTransformPoint( target.Value );
                var rotation2 = GetWeaponRotation( direction );
                if (rotation2 != null) {
                    transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
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
    public interface ICharacterInputActions {

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
