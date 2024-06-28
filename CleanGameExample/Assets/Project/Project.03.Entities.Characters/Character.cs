#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( MoveableBody ) )]
    public abstract class Character : EntityBase, IDamager, IDamageable {

        // MoveableBody
        private MoveableBody MoveableBody { get; set; } = default!;
        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Body
        private Transform Body { get; set; } = default!;
        // Head
        private Transform Head { get; set; } = default!;
        // WeaponSlot
        private Slot WeaponSlot { get; set; } = default!;
        // Game
        public IGame Game { get; set; } = default!;
        // IsAlive
        public bool IsAlive => MoveableBody.enabled;
        // Weapon
        public IWeapon? Weapon => GetWeapon( WeaponSlot );
        // OnDamageEvent
        public event Action<DamageInfo>? OnDamageEvent;

        // Awake
        protected override void Awake() {
            MoveableBody = gameObject.RequireComponent<MoveableBody>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Body = transform.Require( "Body" );
            Head = transform.Require( "Head" );
            WeaponSlot = gameObject.RequireComponentInChildren<Slot>();
        }
        protected override void OnDestroy() {
        }

        // Start
        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
            MoveableBody.FixedUpdate2();
        }
        protected virtual void Update() {
            MoveableBody.Update2();
        }

        // Move
        protected void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            MoveableBody.Move( moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
        }

        // BodyAt
        protected void BodyAt(Vector3? target) {
            MoveableBody.SetLookTarget( target );
        }

        // HeadAt
        protected bool HeadAt(Vector3? target) {
            Assert.Operation.Message( $"Character {this} must be alive" ).Valid( IsAlive );
            return HeadAt( Head, target );
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
        void IDamageable.OnDamage(DamageInfo info) {
            OnDamage( info );
        }
        protected virtual void OnDamage(DamageInfo info) {
            if (IsAlive) {
                SetWeapon( null );
                SetPhysical( this, true );
                Rigidbody.AddForceAtPosition( info.Direction * 5, info.Point, ForceMode.Impulse );
                OnDamageEvent?.Invoke( info );
            }
        }

        // Helpers
        private static bool HeadAt(Transform transform, Vector3? target) {
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
                SetPhysical( (IThing) prevWeapon, null );
            }
            if (weapon != null) {
                SetPhysical( (IThing) weapon, slot.transform );
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
        // Helpers
        private static void SetPhysical(Character character, bool value) {
            if (value) {
                character.gameObject.SetLayerRecursively( Layers.Entity );
                character.Rigidbody.isKinematic = false;
                character.MoveableBody.enabled = false;
            } else {
                character.gameObject.SetLayerRecursively( Layers.CharacterEntity, Layers.CharacterEntityInternal );
                character.Rigidbody.isKinematic = true;
                character.MoveableBody.enabled = true;
            }
        }
        private static void SetPhysical(IThing thing, Transform? parent) {
            if (parent != null) {
                thing.gameObject.SetLayerRecursively( Layers.CharacterEntityInternal );
                thing.transform.localPosition = Vector3.zero;
                thing.transform.localRotation = Quaternion.identity;
                thing.transform.SetParent( parent, false );
                thing.gameObject.RequireComponent<Rigidbody>().isKinematic = true;
            } else {
                thing.gameObject.SetLayerRecursively( Layers.Entity );
                thing.transform.SetParent( null, true );
                thing.gameObject.RequireComponent<Rigidbody>().isKinematic = false;
            }
        }

    }
}
