#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class EntityFactory {

        // Initialize
        public static void Initialize() {
        }
        public static void Deinitialize() {
        }

        // Game
        public static Game Game(PlayerCharacterEnum character, LevelEnum level) {
            using (Context.Begin( new Game.Args( character, level ) )) {
                return Addressables2.Instantiate<Game>( R.Project.Entities.Game_Value );
            }
        }

        // Camera
        public static Camera2 Camera() {
            return Addressables2.Instantiate<Camera2>( R.Project.Entities.Camera_Value );
        }

    }
}
