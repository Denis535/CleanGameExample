#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.Entities.Characters;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {
        public record Arguments(CharacterEnum Character);
        private readonly Lock @lock = new Lock();

        // Args
        private Arguments Args { get; } = Context.Get<Player, Arguments>();
        // Character
        private DynamicInstanceHandle<Character> CharacterInstance { get; } = new DynamicInstanceHandle<Character>();
        public Character? Character => CharacterInstance.ValueSafe;

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
            CharacterInstance.ReleaseInstanceSafe();
        }

        // Start
        public void Start() {
            if (@lock.IsLocked) return;
            using (@lock.Enter()) {
            }
        }
        public void Update() {
            if (@lock.IsLocked) return;
            using (@lock.Enter()) {
            }
        }

        // Spawn
        public async Task SpawnAsync(PlayerSpawnPoint point) {
            using (@lock.Enter()) {
                await CharacterInstance.InstantiateAsync( GetCharacterAddress( Args.Character ), point.transform.position, point.transform.rotation, destroyCancellationToken );
            }
        }

        // Heleprs
        private static string GetCharacterAddress(CharacterEnum character) {
            switch (character) {
                case CharacterEnum.Gray: return R.Project.Entities.Characters.Primary.Character_Gray_Value;
                case CharacterEnum.Red: return R.Project.Entities.Characters.Primary.Character_Red_Value;
                case CharacterEnum.Green: return R.Project.Entities.Characters.Primary.Character_Green_Value;
                case CharacterEnum.Blue: return R.Project.Entities.Characters.Primary.Character_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }

    }
    // Character
    public enum CharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
