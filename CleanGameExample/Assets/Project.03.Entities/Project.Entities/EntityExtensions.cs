#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class EntityExtensions {

        // IsCharacter
        public static bool IsPlayer(this GameObject gameObject) {
            return gameObject.name.Contains( "Player" );
        }
        public static bool IsEnemy(this GameObject gameObject) {
            return gameObject.name.Contains( "Enemy" );
        }

        // IsLoot
        public static bool IsLoot(this GameObject gameObject) {
            return gameObject.name.Contains( "Gun" ) && gameObject.transform.parent == null;
        }
        public static bool IsWeapon(this GameObject gameObject) {
            return gameObject.name.Contains( "Gun" ) && gameObject.transform.parent == null;
        }

    }
}
