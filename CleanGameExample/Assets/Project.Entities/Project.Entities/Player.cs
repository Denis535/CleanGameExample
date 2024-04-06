#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class Player : PlayerBase {
        public record Arguments(Character Character);

        // Args
        private Arguments Args { get; set; } = default!;
        // Character
        private AsyncOperationHandle<GameObject>? CharacterHandle { get; set; }
        public GameObject? Character => CharacterHandle?.Result;

        // Awake
        public void Awake() {
            Args = ComponentContext.GetArguments<Player, Arguments>();
        }
        public void OnDestroy() {
            if (CharacterHandle != null) Addressables.ReleaseInstance( CharacterHandle.Value );
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

        // Spawn
        public void Spawn(PlayerSpawnPoint point) {
            switch (Args.Character) {
                case Project.Entities.Character.Gray: {
                    CharacterHandle = Addressables.InstantiateAsync( R.Project.Entities.Characters.Character_Gray_Value, point.transform.position, point.transform.rotation );
                    break;
                }
                case Project.Entities.Character.Red: {
                    CharacterHandle = Addressables.InstantiateAsync( R.Project.Entities.Characters.Character_Red_Value, point.transform.position, point.transform.rotation );
                    break;
                }
                case Project.Entities.Character.Green: {
                    CharacterHandle = Addressables.InstantiateAsync( R.Project.Entities.Characters.Character_Green_Value, point.transform.position, point.transform.rotation );
                    break;
                }
                case Project.Entities.Character.Blue: {
                    CharacterHandle = Addressables.InstantiateAsync( R.Project.Entities.Characters.Character_Blue_Value, point.transform.position, point.transform.rotation );
                    break;
                }
                default: {
                    throw Exceptions.Internal.NotSupported( $"Character {Args.Character} is not supported" );
                }
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
