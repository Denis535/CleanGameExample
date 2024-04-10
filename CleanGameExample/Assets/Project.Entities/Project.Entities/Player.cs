#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {
        public record Arguments(Character Character);
        private readonly Lock @lock = new Lock();

        // Args
        private Arguments Args { get; set; } = default!;
        // Character
        private DynamicPrefabHandle<Transform> CharacterPrefab = new DynamicPrefabHandle<Transform>();
        public Transform? Character { get; private set; }

        // Awake
        public void Awake() {
            Args = Context.Get<Player, Arguments>();
            CharacterPrefab.LoadAsync( GetCharacterAddress( Args.Character ), destroyCancellationToken );
        }
        public void OnDestroy() {
            if (Character) DestroyImmediate( Character?.gameObject );
            CharacterPrefab.Release();
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
        public async Task Spawn(PlayerSpawnPoint point) {
            using (@lock.Enter()) {
                var prefab = await CharacterPrefab.GetResultAsync( destroyCancellationToken );
                Character = GameObject.Instantiate( prefab, point.transform.position, point.transform.rotation );
            }
        }

        // Heleprs
        private static string GetCharacterAddress(Character character) {
            switch (character) {
                case Project.Entities.Character.Gray: return R.Project.Entities.Characters.Character_Gray_Value;
                case Project.Entities.Character.Red: return R.Project.Entities.Characters.Character_Red_Value;
                case Project.Entities.Character.Green: return R.Project.Entities.Characters.Character_Green_Value;
                case Project.Entities.Character.Blue: return R.Project.Entities.Characters.Character_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }

    }
    // Character
    public enum Character {
        Gray,
        Red,
        Green,
        Blue
    }
}
