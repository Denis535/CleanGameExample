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

        private static readonly PrefabListHandle<PlayerCharacter> Prefabs = new PrefabListHandle<PlayerCharacter>( new[] {
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
                return GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) character ], position, rotation );
            }
        }

    }
    // EnemyCharacter
    public static class EnemyCharacterFactory {
        public record Args(IGame Game);

        private static readonly PrefabListHandle<EnemyCharacter> Prefabs = new PrefabListHandle<EnemyCharacter>( new[] {
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
                return GameObject.Instantiate<EnemyCharacter>( Prefabs.GetValues().GetRandomValue(), position, rotation );
            }
        }

    }
}
