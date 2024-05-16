#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class Character : PhysicsCharacter {

        // Head
        protected Transform Head { get; private set; } = default!;
        // Body
        protected Transform Body { get; private set; } = default!;
        // WeaponSlot
        protected Transform WeaponSlot { get; private set; } = default!;
        // Weapon
        public Weapon? Weapon => GetWeapon( WeaponSlot );

        // Awake
        public override void Awake() {
            base.Awake();
            Head = transform.Require( "Head" );
            Body = transform.Require( "Body" );
            WeaponSlot = transform.Require( "WeaponSlot" );
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public abstract void Start();
        public abstract void FixedUpdate();
        public abstract void Update();

        // LookAt
        public bool LookAt(Vector3? target) {
            return LookAt( Head, target );
        }

        // AimAt
        public bool AimAt(Vector3? target) {
            return AimAt( WeaponSlot, target );
        }

        // SetWeapon
        public void SetWeapon(Weapon? weapon) {
            SetWeapon( WeaponSlot, weapon );
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
