#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class CharacterFactory {

        private static readonly AssetListHandle<GameObject> PlayerCharacterPrefabs = new AssetListHandle<GameObject>( new[] {
            R.Project.Entities.Characters.PlayerCharacter_Gray_Value,
            R.Project.Entities.Characters.PlayerCharacter_Red_Value,
            R.Project.Entities.Characters.PlayerCharacter_Green_Value,
            R.Project.Entities.Characters.PlayerCharacter_Blue_Value
        } );
        private static readonly AssetListHandle<GameObject> EnemyCharacterPrefabs = new AssetListHandle<GameObject>( new[] {
            R.Project.Entities.Characters.EnemyCharacter_Gray_Value,
            R.Project.Entities.Characters.EnemyCharacter_Red_Value,
            R.Project.Entities.Characters.EnemyCharacter_Green_Value,
            R.Project.Entities.Characters.EnemyCharacter_Blue_Value
        } );

        // Initialize
        public static void Initialize() {
            PlayerCharacterPrefabs.Load().Wait();
            EnemyCharacterPrefabs.Load().Wait();
        }
        public static void Deinitialize() {
            PlayerCharacterPrefabs.Release();
            EnemyCharacterPrefabs.Release();
        }

        // PlayerCharacter
        public static PlayerCharacter PlayerCharacter(IGame game, IPlayer player, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new PlayerCharacter.Args( game, player ) )) {
                return PlayerCharacterPrefabs.Values[ (int) player.CharacterEnum ].Instantiate<PlayerCharacter>( position, rotation );
            }
        }

        // EnemyCharacter
        public static EnemyCharacter EnemyCharacter(IGame game, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new EnemyCharacter.Args( game ) )) {
                return EnemyCharacterPrefabs.Values.GetRandomValue().Instantiate<EnemyCharacter>( position, rotation );
            }
        }

    }
}
