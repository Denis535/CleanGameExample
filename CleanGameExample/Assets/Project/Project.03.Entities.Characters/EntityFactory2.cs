#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class EntityFactory2 {

        private static string[] PlayerCharacters = new[] {
            R.Project.Entities.Characters.PlayerCharacter_Gray_Value,
            R.Project.Entities.Characters.PlayerCharacter_Red_Value,
            R.Project.Entities.Characters.PlayerCharacter_Green_Value,
            R.Project.Entities.Characters.PlayerCharacter_Blue_Value
        };
        private static string[] EnemyCharacters = new[] {
            R.Project.Entities.Characters.EnemyCharacter_Gray_Value,
            R.Project.Entities.Characters.EnemyCharacter_Red_Value,
            R.Project.Entities.Characters.EnemyCharacter_Green_Value,
            R.Project.Entities.Characters.EnemyCharacter_Blue_Value
        };

        // Initialize
        public static void Initialize() {
        }
        public static void Deinitialize() {
        }

        // PlayerCharacter
        public static PlayerCharacter PlayerCharacter(PlayerCharacterEnum character, Vector3 position, Quaternion rotation) {
            var key = PlayerCharacters[ (int) character ];
            return Addressables2.Instantiate<PlayerCharacter>( key, position, rotation );
        }

        // EnemyCharacter
        public static EnemyCharacter EnemyCharacter(Vector3 position, Quaternion rotation) {
            var key = EnemyCharacters[ UnityEngine.Random.Range( 0, EnemyCharacters.Length ) ];
            return Addressables2.Instantiate<EnemyCharacter>( key, position, rotation );
        }

    }
}
