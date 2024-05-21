#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class CharacterExtensions {

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

    }
}
