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

        // SetWeapon
        public void SetWeapon(GameObject? weapon) {
            if (Weapon != null) {
                Weapon.GetComponent<Rigidbody>().isKinematic = false;
                Weapon.transform.parent = null;
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
