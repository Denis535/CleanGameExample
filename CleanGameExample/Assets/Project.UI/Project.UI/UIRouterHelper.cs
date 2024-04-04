#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;

    internal static class UIRouterHelper {

        private static AsyncOperationHandle<SceneInstance>? programHandle;
        private static AsyncOperationHandle<SceneInstance>? mainSceneHandle;
        private static AsyncOperationHandle<SceneInstance>? gameSceneHandle;
        private static AsyncOperationHandle<SceneInstance>? worldSceneHandle;

        public static bool IsProgramLoaded => programHandle != null;
        public static bool IsMainSceneLoaded => mainSceneHandle != null;
        public static bool IsGameSceneLoaded => gameSceneHandle != null;
        public static bool IsWorldSceneLoaded => worldSceneHandle != null;

        // LoadScene
        public static Task LoadProgramAsync() {
            return LoadSceneAsync( R.Project.Scenes.Program_Value, LoadSceneMode.Single, ref programHandle );
        }
        public static Task LoadMainSceneAsync() {
            return LoadSceneAsync( R.Project.Scenes.MainScene_Value, LoadSceneMode.Additive, ref mainSceneHandle );
        }
        public static Task LoadGameSceneAsync() {
            return LoadSceneAsync( R.Project.Scenes.GameScene_Value, LoadSceneMode.Additive, ref gameSceneHandle );
        }
        public static Task LoadWorldSceneAsync(string key) {
            return LoadSceneAsync( key, LoadSceneMode.Additive, ref worldSceneHandle );
        }

        // UnloadScene
        public static Task UnloadMainSceneAsync() {
            return UnloadSceneAsync( ref mainSceneHandle );
        }
        public static Task UnloadGameSceneAsync() {
            return UnloadSceneAsync( ref gameSceneHandle );
        }
        public static Task UnloadWorldSceneAsync() {
            return UnloadSceneAsync( ref worldSceneHandle );
        }

        // Heleprs
        private static Task LoadSceneAsync(string key, LoadSceneMode mode, ref AsyncOperationHandle<SceneInstance>? handle) {
            Assert.Operation.Message( $"Handle {handle} must be null" ).Valid( handle == null );
            handle = Addressables2.LoadSceneAsync( key, mode, false );
            return handle.Value.GetResultAsync( default ).ContinueWith( async i => {
                var sceneInstance = i.Result;
                await sceneInstance.ActivateAsync();
                SceneManager.SetActiveScene( sceneInstance.Scene );
            }, TaskScheduler.FromCurrentSynchronizationContext() ).Unwrap();
        }
        private static Task UnloadSceneAsync(ref AsyncOperationHandle<SceneInstance>? handle) {
            Assert.Operation.Message( $"Handle {handle} must be non-null" ).Valid( handle != null );
            Assert.Operation.Message( $"Handle {handle} must be valid" ).Valid( handle.Value.IsValid() );
            var tmp_handle = handle;
            handle = null;
            return Addressables2.UnloadSceneAsync( tmp_handle.Value ).WaitAsync( default );
        }

    }
}
