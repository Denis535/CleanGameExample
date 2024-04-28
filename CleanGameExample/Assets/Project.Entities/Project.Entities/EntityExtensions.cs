#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    internal static class EntityExtensions {

        // IsCharacter
        public static bool IsPlayer(this GameObject gameObject) {
            return gameObject.name.Contains( "Player" );
        }
        public static bool IsEnemy(this GameObject gameObject) {
            return gameObject.name.Contains( "Enemy" );
        }

        // IsInteractable
        public static bool IsInteractable(this GameObject gameObject) {
            return gameObject.name.Contains( "Gun" );
        }
        public static bool IsWeapon(this GameObject gameObject) {
            return gameObject.name.Contains( "Gun" );
        }

    }
}
