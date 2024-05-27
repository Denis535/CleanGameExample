#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    // PlayerCharacter
    public static class PlayerCharacterFactory {
        public record Args(IGame Game, IPlayer Player);

        private static readonly AssetListHandle<GameObject> Prefabs = new AssetListHandle<GameObject>( new[] {
            R.Project.Entities.Characters.PlayerCharacter_Gray_Value,
            R.Project.Entities.Characters.PlayerCharacter_Red_Value,
            R.Project.Entities.Characters.PlayerCharacter_Green_Value,
            R.Project.Entities.Characters.PlayerCharacter_Blue_Value
        } );

        public static void Initialize() {
            Prefabs.Load().Wait();
        }
        public static void Deinitialize() {
            Prefabs.Release();
        }

        public static PlayerCharacter Create(IGame game, IPlayer player, PlayerCharacterEnum character, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args( game, player ) )) {
                return Prefabs.Values[ (int) character ].Instantiate<PlayerCharacter>( position, rotation );
            }
        }

    }
    // EnemyCharacter
    public static class EnemyCharacterFactory {
        public record Args(IGame Game);

        private static readonly AssetListHandle<GameObject> Prefabs = new AssetListHandle<GameObject>( new[] {
            R.Project.Entities.Characters.EnemyCharacter_Gray_Value,
            R.Project.Entities.Characters.EnemyCharacter_Red_Value,
            R.Project.Entities.Characters.EnemyCharacter_Green_Value,
            R.Project.Entities.Characters.EnemyCharacter_Blue_Value
        } );

        public static void Initialize() {
            Prefabs.Load().Wait();
        }
        public static void Deinitialize() {
            Prefabs.Release();
        }

        public static EnemyCharacter Create(IGame game, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args( game ) )) {
                return Prefabs.Values.GetRandomValue().Instantiate<EnemyCharacter>( position, rotation );
            }
        }

    }
}
