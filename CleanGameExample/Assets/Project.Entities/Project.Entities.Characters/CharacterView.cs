#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterView : EntityViewBase {

        // Components
        private Transform Body { get; set; } = default!;
        private Transform Head { get; set; } = default!;
        private Transform WeaponSlot { get; set; } = default!;
        // Weapon
        public GameObject? Weapon => WeaponSlot.childCount > 0 ? WeaponSlot.GetChild( 0 )?.gameObject : null;

        // Awake
        public void Awake() {
            Body = transform.Require( "Body" );
            Head = transform.Require( "Head" );
            WeaponSlot = transform.Require( "WeaponSlot" );
        }
        public void OnDestroy() {
        }

        // LookAt
        public void LookAt(Vector3? target) {
            var rotation = Head.localRotation;
            if (target != null) {
                Head.localRotation = Quaternion.identity;
                var direction = Head.InverseTransformPoint( target.Value );
                var rotation2 = GetHeadRotation( direction );
                if (rotation2 != null) {
                    Head.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                } else {
                    Head.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                }
            } else {
                Head.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
            }
        }

        // AimAt
        public void AimAt(Vector3? target) {
            var rotation = WeaponSlot.localRotation;
            if (target != null) {
                WeaponSlot.localRotation = Quaternion.identity;
                var direction = WeaponSlot.InverseTransformPoint( target.Value );
                var rotation2 = GetWeaponRotation( direction );
                if (rotation2 != null) {
                    WeaponSlot.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                } else {
                    WeaponSlot.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                }
            } else {
                WeaponSlot.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
            }
        }

        // SetWeapon
        public void SetWeapon(GameObject? weapon, out GameObject? prevWeapon) {
            if (Weapon != null) {
                prevWeapon = Weapon;
                Weapon.GetComponent<Rigidbody>().isKinematic = false;
                Weapon.transform.parent = null;
            } else {
                prevWeapon = null;
            }
            if (weapon != null) {
                weapon.GetComponent<Rigidbody>().isKinematic = true;
                weapon.transform.parent = WeaponSlot;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
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
