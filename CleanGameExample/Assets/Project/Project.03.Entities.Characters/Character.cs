#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Character : EntityBase, IDamageable {

        // PhysicsCharacter
        private PhysicsCharacter PhysicsCharacter { get; set; }
        // Head
        protected Transform Head { get; private set; } = default!;
        // Body
        protected Transform Body { get; private set; } = default!;
        // WeaponSlot
        protected Slot WeaponSlot { get; private set; } = default!;
        // Weapon
        public Weapon? Weapon => GetWeapon( WeaponSlot );
        // IsAlive
        public bool IsAlive => PhysicsCharacter != null;

        // Awake
        public override void Awake() {
            PhysicsCharacter = gameObject.RequireComponent<PhysicsCharacter>();
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
            if (PhysicsCharacter.enabled) {
                PhysicsCharacter.PhysicsFixedUpdate();
            }
        }
        public virtual void Update() {
        }

        // SetMovementInput
        protected void SetMovementInput(bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Object.Message( $"Character {this} must be awakened" ).Initialized( didAwake );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Character {this} must be enabled" ).Valid( enabled );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( IsAlive );
            PhysicsCharacter.SetMovementInput( isMovePressed, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
        }

        // RotateAt
        protected void RotateAt(Vector3? target) {
            Assert.Object.Message( $"Character {this} must be awakened" ).Initialized( didAwake );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Character {this} must be enabled" ).Valid( enabled );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( IsAlive );
            if (target != null) {
                PhysicsCharacter.SetLookInput( true, target.Value );
                PhysicsCharacter.PhysicsUpdate();
            } else {
                PhysicsCharacter.SetLookInput( false, PhysicsCharacter.LookTarget );
                PhysicsCharacter.PhysicsUpdate();
            }
        }

        // AimAt
        protected bool AimAt(Vector3? target) {
            Assert.Object.Message( $"Character {this} must be awakened" ).Initialized( didAwake );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Character {this} must be enabled" ).Valid( enabled );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( IsAlive );
            return AimAt( WeaponSlot.transform, target );
        }

        // LookAt
        protected bool LookAt(Vector3? target) {
            Assert.Object.Message( $"Character {this} must be awakened" ).Initialized( didAwake );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Character {this} must be enabled" ).Valid( enabled );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( IsAlive );
            return LookAt( Head, target );
        }

        // SetWeapon
        protected void SetWeapon(Weapon? weapon) {
            Assert.Object.Message( $"Character {this} must be awakened" ).Initialized( didAwake );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Character {this} must be enabled" ).Valid( enabled );
            Assert.Object.Message( $"Character {this} must be alive" ).Alive( IsAlive );
            SetWeapon( WeaponSlot, weapon );
        }

        // OnDamage
        void IDamageable.OnDamage(float damage, Vector3 point, Vector3 direction) {
            OnDamage( damage, point, direction );
        }
        protected virtual void OnDamage(float damage, Vector3 point, Vector3 direction) {
            if (IsAlive) {
                OnDead();
            }
        }

        // OnDead
        protected virtual void OnDead() {
            WeaponSlot.transform.DetachChildren();
            PhysicsCharacter.enabled = false;
        }

        // Helpers
        private static Weapon? GetWeapon(Slot slot) {
            return slot.transform.childCount > 0 ? slot.transform.GetChild( 0 )?.gameObject.RequireComponent<Weapon>() : null;
        }
        private static void SetWeapon(Slot slot, Weapon? weapon) {
            slot.transform.DetachChildren();
            if (weapon != null) {
                weapon.transform.parent = slot.transform;
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
