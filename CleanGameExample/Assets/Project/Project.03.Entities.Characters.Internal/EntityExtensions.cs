#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class EntityExtensions {

        // IsWeapon
        public static bool IsWeapon(this GameObject gameObject) {
            return gameObject.GetComponent<Weapon>() != null && gameObject.transform.parent == null;
        }
        public static bool IsGun(this GameObject gameObject) {
            return gameObject.GetComponent<Gun>() != null && gameObject.transform.parent == null;
        }

        // IsBullet
        public static bool IsBullet(this GameObject gameObject) {
            return gameObject.GetComponent<Bullet>() != null && gameObject.transform.parent == null;
        }

    }
}
