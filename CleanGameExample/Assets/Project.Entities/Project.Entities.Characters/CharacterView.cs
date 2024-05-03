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
        public void LookAt(Vector3 target) {
            Head.LookAt( target, Vector3.up );

            //var direction = (target - Head.position).normalized;
            //Head.forward = direction;
            //Head.localRotation = Quaternion.LookRotation( direction );
            //Head.localEulerAngles = Quaternion.LookRotation( direction ).eulerAngles;
            //Debug.Log( Quaternion.LookRotation( direction ).eulerAngles );
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

    }
}
