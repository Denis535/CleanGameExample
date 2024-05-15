#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class EntityExtensions {

        // IsCharacter
        public static bool IsCharacter(this GameObject gameObject) {
            return gameObject.GetComponent<Character>() != null;
        }
        public static bool IsPlayer(this GameObject gameObject) {
            return gameObject.GetComponent<PlayerCharacter>() != null;
        }
        public static bool IsEnemy(this GameObject gameObject) {
            return gameObject.GetComponent<EnemyCharacter>() != null;
        }

        // IsLoot
        public static bool IsLoot(this GameObject gameObject) {
            return gameObject.GetComponent<Weapon>() != null && gameObject.transform.parent == null;
        }
        public static bool IsWeapon(this GameObject gameObject) {
            return gameObject.GetComponent<Weapon>() != null && gameObject.transform.parent == null;
        }

        // IsBullet
        public static bool IsBullet(this GameObject gameObject) {
            return gameObject.GetComponent<Bullet>() != null && gameObject.transform.parent == null;
        }

    }
}
