#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;

    public sealed class SceneHandle {

        private readonly string key;
        private AsyncOperationHandle<SceneInstance>? handle;

        public bool IsActive => handle != null;
        public bool IsValid => handle!.Value.IsValid();
        public bool IsSucceeded => handle!.Value.IsSucceeded();
        public bool IsFailed => handle!.Value.IsFailed();
        public Scene Scene => handle!.Value.Task.Result.Scene;
        public Task<Scene> SceneAsync => handle!.Value.Task.ContinueWith( i => i.Result.Scene );

        // Constructor
        public SceneHandle(string key) {
            this.key = key;
        }

        // Load
        public Task<Scene> LoadAsync(LoadSceneMode mode) {
            Assert.Operation.Message( $"SceneHandle {this} must be non-active" ).Valid( handle == null );
            handle = Addressables.LoadSceneAsync( key, mode, false );
            if (handle.Value.IsValid() && handle.Value.IsFailed()) {
                throw handle.Value.OperationException;
            }
            return handle.Value.Task.ContinueWith( async task => {
                var sceneInstance = task.Result;
                if (sceneInstance.Scene.IsValid()) {
                    await sceneInstance.ActivateAsync();
                }
                return sceneInstance.Scene;
            }, TaskScheduler.FromCurrentSynchronizationContext() ).Unwrap();
        }

        // Unload
        public async Task UnloadAsync() {
            Assert.Operation.Message( $"SceneHandle {this} must be active" ).Valid( handle != null );
            await Addressables.UnloadSceneAsync( handle.Value ).Task;
            handle = null;
        }
        public async Task SafeUnloadAsync() {
            if (handle != null) {
                await Addressables.UnloadSceneAsync( handle.Value ).Task;
                handle = null;
            }
        }

        // Utils
        public override string ToString() {
            return $"SceneHandle: {key}";
        }

    }
}
