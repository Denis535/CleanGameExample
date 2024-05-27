#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( CharacterBody ) )]
    public abstract class Character : EntityBase, IDamager, IDamageable {

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // CharacterBody
        private CharacterBody CharacterBody { get; set; } = default!;
        // Head
        private Transform Head { get; set; } = default!;
        // Body
        private Transform Body { get; set; } = default!;
        // WeaponSlot
        private Slot WeaponSlot { get; set; } = default!;
        // IsAlive
        public bool IsAlive => CharacterBody.enabled;
        // Weapon
        public IWeapon? Weapon => GetWeapon( WeaponSlot );

        // Awake
        public override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            CharacterBody = gameObject.RequireComponent<CharacterBody>();
            Head = transform.Require( "Head" );
            Body = transform.Require( "Body" );
            WeaponSlot = gameObject.RequireComponentInChildren<Slot>();
        }
        public override void OnDestroy() {
        }

        // Start
        public virtual void Start() {
        }
        public virtual void FixedUpdate() {
            if (CharacterBody.enabled) {
                CharacterBody.PhysicsFixedUpdate();
            }
        }
        public virtual void Update() {
        }

        // SetMovementInput
        protected void SetMovementInput(bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Character {this} must be alive" ).Valid( IsAlive );
            CharacterBody.SetMovementInput( isMovePressed, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
        }

        // RotateAt
        protected void RotateAt(Vector3? target) {
            Assert.Operation.Message( $"Character {this} must be alive" ).Valid( IsAlive );
            if (target != null) {
                CharacterBody.SetLookInput( true, target.Value );
                CharacterBody.PhysicsUpdate();
            } else {
                CharacterBody.SetLookInput( false, CharacterBody.LookTarget );
                CharacterBody.PhysicsUpdate();
            }
        }

        // LookAt
        protected bool LookAt(Vector3? target) {
            Assert.Operation.Message( $"Character {this} must be alive" ).Valid( IsAlive );
            return LookAt( Head, target );
        }

        // AimAt
        protected bool AimAt(Vector3? target) {
            Assert.Operation.Message( $"Character {this} must be alive" ).Valid( IsAlive );
            return AimAt( WeaponSlot.transform, target );
        }

        // SetWeapon
        protected void SetWeapon(IWeapon? weapon) {
            Assert.Operation.Message( $"Character {this} must be alive" ).Valid( IsAlive );
            SetWeapon( WeaponSlot, weapon );
        }

        // OnDamage
        void IDamageable.OnDamage(Bullet bullet, float damage, Vector3 point, Vector3 direction) {
            OnDamage( bullet, damage, point, direction );
        }
        protected virtual void OnDamage(Bullet bullet, float damage, Vector3 point, Vector3 direction) {
            if (IsAlive) {
                SetWeapon( null );
                gameObject.SetLayerRecursively( Layers.Entity );
                CharacterBody.enabled = false;
                Rigidbody.isKinematic = false;
                Rigidbody.AddForceAtPosition( direction * 5, point, ForceMode.Impulse );
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
        private static IWeapon? GetWeapon(Slot slot) {
            return slot.transform.childCount >= 1 ? slot.transform.GetChild( 0 )?.gameObject.RequireComponent<IWeapon>() : null;
        }
        private static void SetWeapon(Slot slot, IWeapon? weapon) {
            var prevWeapon = GetWeapon( slot );
            if (prevWeapon != null) {
                prevWeapon.gameObject.SetLayerRecursively( Layers.Entity );
                prevWeapon.transform.SetParent( null, true );
            }
            if (weapon != null) {
                weapon.gameObject.SetLayerRecursively( Layers.CharacterEntityInternal );
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
                weapon.transform.SetParent( slot.transform, false );
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
}
