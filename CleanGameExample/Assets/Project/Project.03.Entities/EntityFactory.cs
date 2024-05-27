#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    // Game
    public static class GameFactory {
        public record Args(LevelEnum Level, string Name, PlayerCharacterEnum Character);

        private static readonly AssetHandle<GameObject> Prefab = new AssetHandle<GameObject>( R.Project.Entities.Game_Value );

        public static void Initialize() {
            Prefab.Load().Wait();
        }
        public static void Deinitialize() {
            Prefab.Release();
        }

        public static Game Create(LevelEnum level, string name, PlayerCharacterEnum character) {
            using (Context.Begin( new Args( level, name, character ) )) {
                return Prefab.Value.Instantiate<Game>();
            }
        }

    }
    // Camera
    public static class CameraFactory {
        public record Args();

        private static readonly AssetHandle<GameObject> Prefab = new AssetHandle<GameObject>( R.Project.Entities.Camera_Value );

        public static void Initialize() {
            Prefab.Load().Wait();
        }
        public static void Deinitialize() {
            Prefab.Release();
        }

        public static Camera2 Camera() {
            using (Context.Begin( new Args() )) {
                return Prefab.Value.Instantiate<Camera2>();
            }
        }

    }
}
