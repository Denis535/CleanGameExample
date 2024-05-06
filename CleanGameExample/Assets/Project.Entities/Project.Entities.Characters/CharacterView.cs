#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterView : EntityViewBase {

        // GameObject
        protected override GameObject GameObject { get; }
        // Body
        private Transform Body { get; }
        // Head
        private Transform Head { get; }
        // WeaponSlot
        private Transform WeaponSlot { get; }
        // Weapon
        public GameObject? Weapon => WeaponSlot.childCount > 0 ? WeaponSlot.GetChild( 0 )?.gameObject : null;

        // Constructor
        public CharacterView(GameObject gameObject) {
            GameObject = gameObject;
            Body = gameObject.transform.Require( "Body" );
            Head = gameObject.transform.Require( "Head" );
            WeaponSlot = gameObject.transform.Require( "WeaponSlot" );
        }
        public override void Dispose() {
        }

        // LookAt
        public bool LookAt(Vector3? target) {
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

        // AimAt
        public bool AimAt(Vector3? target) {
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

        // SetWeapon
        public void SetWeapon(GameObject? weapon, out GameObject? prevWeapon) {
            prevWeapon = Weapon;
            SetWeapon( weapon );
        }
        public void SetWeapon(GameObject? weapon) {
            if (Weapon != null) {
                Weapon.transform.parent = null;
            }
            if (weapon != null) {
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
