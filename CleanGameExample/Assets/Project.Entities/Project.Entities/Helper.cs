#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class Helper {

        // IsCharacter
        public static bool IsPlayerCharacter(this GameObject gameObject) {
            return gameObject.name.Contains( "PlayerCharacter" );
        }
        public static bool IsEnemyCharacter(this GameObject gameObject) {
            return gameObject.name.Contains( "EnemyCharacter" );
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
