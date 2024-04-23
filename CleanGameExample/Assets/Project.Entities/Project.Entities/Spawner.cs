#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    internal static class Spawner {

        // SpawnPlayerCharacter
        public static Character SpawnPlayerCharacter(this Game game, PlayerSpawnPoint point, CharacterEnum character, Character.IContext context, CharacterBody.IContext context2) {
            var instance = new InstanceHandle<Character>( GetPlayerCharacter( character ) );
            game.instances.Add( instance );
            return instance.Instantiate( i => Instantiate( i.RequireComponent<Character>(), point.transform.position, point.transform.rotation, game.transform, context, context2 ) );
        }

        // SpawnPlayerCharacterAsync
        public static async ValueTask<Character> SpawnPlayerCharacterAsync(this Game game, PlayerSpawnPoint point, CharacterEnum character, Character.IContext context, CharacterBody.IContext context2, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Character>( GetPlayerCharacter( character ) );
            game.instances.Add( instance );
            return await instance.InstantiateAsync( i => Instantiate( i.RequireComponent<Character>(), point.transform.position, point.transform.rotation, game.transform, context, context2 ), cancellationToken );
        }
        private static Character Instantiate(Character prefab, Vector3 position, Quaternion rotation, Transform? parent, Character.IContext context, CharacterBody.IContext context2) {
            using (Context.Begin<Character, Character.Arguments>( new Character.Arguments( context ) )) {
                using (Context.Begin<CharacterBody, CharacterBody.Arguments>( new CharacterBody.Arguments( context2 ) )) {
                    return UnityEngine.Object.Instantiate( prefab, position, rotation, parent );
                }
            }
        }

        // SpawnEnemyCharacterAsync
        public static async ValueTask<Transform> SpawnEnemyCharacterAsync(this Game game, EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( GetEnemyCharacter() );
            game.instances.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, game.transform, cancellationToken );
        }

        // SpawnLootAsync
        public static async ValueTask<Transform> SpawnLootAsync(this Game game, LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( GetLoot() );
            game.instances.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, game.transform, cancellationToken );
        }

        // Heleprs
        private static string GetPlayerCharacter(CharacterEnum character) {
            switch (character) {
                case CharacterEnum.Gray: return R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value;
                case CharacterEnum.Red: return R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value;
                case CharacterEnum.Green: return R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value;
                case CharacterEnum.Blue: return R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }
        private static string GetEnemyCharacter() {
            var array = new[] {
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Gray_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Red_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Green_Value,
                R.Project.Entities.Characters.Secondary.EnemyCharacter_Blue_Value
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }
        private static string GetLoot() {
            var array = new[] {
                R.Project.Entities.Characters.Inventory.Gun_Gray_Value,
                R.Project.Entities.Characters.Inventory.Gun_Red_Value,
                R.Project.Entities.Characters.Inventory.Gun_Green_Value,
                R.Project.Entities.Characters.Inventory.Gun_Blue_Value,
            };
            return array[ UnityEngine.Random.Range( 0, array.Length ) ];
        }

    }
}
