#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class EntityFactory {

        private static readonly AssetHandle<GameObject> GamePrefab = new AssetHandle<GameObject>( R.Project.Entities.Game_Value );
        private static readonly AssetHandle<GameObject> CameraPrefab = new AssetHandle<GameObject>( R.Project.Entities.Camera_Value );

        // Initialize
        public static void Initialize() {
            GamePrefab.Load().Wait();
            CameraPrefab.Load().Wait();
        }
        public static void Deinitialize() {
            GamePrefab.Release();
            CameraPrefab.Release();
        }

        // Game
        public static Game Game(PlayerCharacterEnum character, LevelEnum level) {
            using (Context.Begin( new Game.Args( character, level ) )) {
                return GamePrefab.Value.Instantiate<Game>();
            }
        }

        // Camera
        public static Camera2 Camera() {
            return CameraPrefab.Value.Instantiate<Camera2>();
        }

    }
}
