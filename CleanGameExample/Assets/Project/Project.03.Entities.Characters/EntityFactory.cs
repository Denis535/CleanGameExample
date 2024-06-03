#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class PlayerCharacterFactory {
        public record Args();

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

        public static PlayerCharacter Create(PlayerCharacterKind kind, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) kind ], position, rotation );
            }
        }

    }
    public static class EnemyCharacterFactory {
        public record Args();

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

        public static EnemyCharacter Create(Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return GameObject.Instantiate<EnemyCharacter>( Prefabs.GetValues().GetRandomValue(), position, rotation );
            }
        }

    }
}
