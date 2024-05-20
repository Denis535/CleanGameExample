#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class CharacterFactory {

        // PlayerCharacter
        public static PlayerCharacter PlayerCharacter(PlayerCharacterEnum character, Vector3 position, Quaternion rotation) {
            var key = character switch {
                PlayerCharacterEnum.Gray => R.Project.Entities.Characters.PlayerCharacter_Gray_Value,
                PlayerCharacterEnum.Red => R.Project.Entities.Characters.PlayerCharacter_Red_Value,
                PlayerCharacterEnum.Green => R.Project.Entities.Characters.PlayerCharacter_Green_Value,
                PlayerCharacterEnum.Blue => R.Project.Entities.Characters.PlayerCharacter_Blue_Value,
                _ => throw Exceptions.Internal.NotSupported( $"PlayerCharacter {character} is not supported" )
            };
            return EntityFactory.Instantiate<PlayerCharacter>( key, position, rotation );
        }

        // EnemyCharacter
        public static EnemyCharacter EnemyCharacter(Vector3 position, Quaternion rotation) {
            var keys = new[] {
                R.Project.Entities.Characters.EnemyCharacter_Gray_Value,
                R.Project.Entities.Characters.EnemyCharacter_Red_Value,
                R.Project.Entities.Characters.EnemyCharacter_Green_Value,
                R.Project.Entities.Characters.EnemyCharacter_Blue_Value
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return EntityFactory.Instantiate<EnemyCharacter>( key, position, rotation );
        }

    }
}
