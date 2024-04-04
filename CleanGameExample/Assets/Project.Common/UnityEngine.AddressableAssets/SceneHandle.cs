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
        public SceneInstance SceneInstance => handle!.Value.Result;
        public Scene Scene => handle!.Value.Result.Scene;

        public SceneHandle(string key) {
            this.key = key;
        }

        public async Task<Scene> LoadAsync(LoadSceneMode mode) {
            Assert.Operation.Message( $"SceneHandle {this} must be non-active" ).Valid( handle == null );
            handle = Addressables.LoadSceneAsync( key, mode, false );
            if (handle.Value.IsValid() && handle.Value.IsFailed()) {
                throw handle.Value.OperationException;
            }
            var sceneInstance = await handle.Value.Task;
            if (handle.Value.IsValid() && handle.Value.IsFailed()) {
                throw handle.Value.OperationException;
            }
            await sceneInstance.ActivateAsync();
            return sceneInstance.Scene;
        }
        public async Task UnloadAsync() {
            Assert.Operation.Message( $"SceneHandle {this} must be active" ).Valid( handle != null );
            var tmp_handle = handle;
            handle = null;
            await Addressables.UnloadSceneAsync( tmp_handle.Value ).Task;
        }
        public async Task SafeUnloadAsync() {
            if (handle != null) {
                var tmp_handle = handle;
                handle = null;
                await Addressables.UnloadSceneAsync( tmp_handle.Value ).Task;
            }
        }

        public override string ToString() {
            return $"SceneHandle: {key}";
        }

    }
}
