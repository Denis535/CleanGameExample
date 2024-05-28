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

        private static readonly PrefabHandle<Game> Prefab = new PrefabHandle<Game>( R.Project.Entities.Game_Value );

        public static void Initialize() {
            Prefab.Load().Wait();
        }
        public static void Deinitialize() {
            Prefab.Release();
        }

        public static Game Create(LevelEnum level, string name, PlayerCharacterEnum character) {
            using (Context.Begin( new Args( level, name, character ) )) {
                return GameObject.Instantiate( Prefab.GetValue() );
            }
        }

    }
    // Camera
    public static class CameraFactory {

        private static readonly PrefabHandle<Camera2> Prefab = new PrefabHandle<Camera2>( R.Project.Entities.Camera_Value );

        public static void Initialize() {
            Prefab.Load().Wait();
        }
        public static void Deinitialize() {
            Prefab.Release();
        }

        public static Camera2 Camera() {
            return GameObject.Instantiate( Prefab.GetValue() );
        }

    }
}
