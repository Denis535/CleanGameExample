#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class GameExtensions {

        // SpawnPlayerCharacter
        public static Character SpawnPlayerCharacter(this Game game, PlayerSpawnPoint point, CharacterEnum character) {
            var instance = new InstanceHandle<Character>( GetPlayerCharacter( character ) );
            ((IList<InstanceHandle>) game.Instances).Add( instance );
            return instance.Instantiate( i => UnityEngine.Object.Instantiate( i.RequireComponent<Character>(), point.transform.position, point.transform.rotation, game.transform ) ).GetValue();
        }

        // SpawnPlayerCharacterAsync
        public static ValueTask<Character> SpawnPlayerCharacterAsync(this Game game, PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Character>( GetPlayerCharacter( character ) );
            ((IList<InstanceHandle>) game.Instances).Add( instance );
            return instance.Instantiate( i => UnityEngine.Object.Instantiate( i.RequireComponent<Character>(), point.transform.position, point.transform.rotation, game.transform ) ).GetValueAsync( cancellationToken );
        }

        // SpawnEnemyCharacterAsync
        public static ValueTask<Transform> SpawnEnemyCharacterAsync(this Game game, EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( GetEnemyCharacter() );
            ((IList<InstanceHandle>) game.Instances).Add( instance );
            return instance.Instantiate( point.transform.position, point.transform.rotation, game.transform ).GetValueAsync( cancellationToken );
        }

        // SpawnLootAsync
        public static ValueTask<Transform> SpawnLootAsync(this Game game, LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( GetLoot() );
            ((IList<InstanceHandle>) game.Instances).Add( instance );
            return instance.Instantiate( point.transform.position, point.transform.rotation, game.transform ).GetValueAsync( cancellationToken );
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
