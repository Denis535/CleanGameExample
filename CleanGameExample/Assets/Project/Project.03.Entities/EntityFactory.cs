#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

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
