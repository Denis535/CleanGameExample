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
            R.Project.Entities.Characters.Value_PlayerCharacter_Gray,
            R.Project.Entities.Characters.Value_PlayerCharacter_Red,
            R.Project.Entities.Characters.Value_PlayerCharacter_Green,
            R.Project.Entities.Characters.Value_PlayerCharacter_Blue
        } );

        public static void Initialize() {
            Prefabs.Load().Wait();
        }
        public static void Deinitialize() {
            Prefabs.Release();
        }

        public static PlayerCharacter Create(PlayerCharacterType type, Vector3 position, Quaternion rotation) {
            using (Context.Begin( new Args() )) {
                return GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) type ], position, rotation );
            }
        }

    }
    public static class EnemyCharacterFactory {
        public record Args();

        private static readonly PrefabListHandle<EnemyCharacter> Prefabs = new PrefabListHandle<EnemyCharacter>( new[] {
            R.Project.Entities.Characters.Value_EnemyCharacter_Gray,
            R.Project.Entities.Characters.Value_EnemyCharacter_Red,
            R.Project.Entities.Characters.Value_EnemyCharacter_Green,
            R.Project.Entities.Characters.Value_EnemyCharacter_Blue
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
    public enum PlayerCharacterType {
        Gray,
        Red,
        Green,
        Blue
    }
}
