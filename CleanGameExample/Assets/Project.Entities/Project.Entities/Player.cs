#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {
        public record Arguments(Character Character);
        private readonly PrefabHandle<Transform> grayCharacter = new PrefabHandle<Transform>( R.Project.Entities.Characters.Character_Gray_Value );
        private readonly PrefabHandle<Transform> redCharacter = new PrefabHandle<Transform>( R.Project.Entities.Characters.Character_Red_Value );
        private readonly PrefabHandle<Transform> greenCharacter = new PrefabHandle<Transform>( R.Project.Entities.Characters.Character_Green_Value );
        private readonly PrefabHandle<Transform> blueCharacter = new PrefabHandle<Transform>( R.Project.Entities.Characters.Character_Blue_Value );

        // Args
        private Arguments Args { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Player, Arguments>();
        }
        public void OnDestroy() {
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
                    grayCharacter.InstantiateAsync( point.transform.position, point.transform.rotation, null, destroyCancellationToken );
                    break;
                }
                case Project.Entities.Character.Red: {
                    redCharacter.InstantiateAsync( point.transform.position, point.transform.rotation, null, destroyCancellationToken );
                    break;
                }
                case Project.Entities.Character.Green: {
                    greenCharacter.InstantiateAsync( point.transform.position, point.transform.rotation, null, destroyCancellationToken );
                    break;
                }
                case Project.Entities.Character.Blue: {
                    blueCharacter.InstantiateAsync( point.transform.position, point.transform.rotation, null, destroyCancellationToken );
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
