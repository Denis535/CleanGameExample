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

    public class SceneHandle {

        private readonly string key;
        private AsyncOperationHandle<SceneInstance>? handle;

        public bool IsActive => handle != null;
        public bool IsValid => handle!.Value.IsValid();
        public bool IsSucceeded => handle!.Value.IsSucceeded();
        public bool IsFailed => handle!.Value.IsFailed();
        public Scene Scene => handle!.Value.Result.Scene;

        public SceneHandle(string key) {
            this.key = key;
        }

        public async Task LoadAsync(LoadSceneMode mode) {
            Assert.Operation.Message( $"SceneHandle {this} must be non-active" ).Valid( handle == null );
            try {
                handle = Addressables2.LoadSceneAsync( key, mode, false );
                var sceneInstance = await handle.Value.Task;
                if (handle.Value.IsSucceeded()) {
                    await sceneInstance.ActivateAsync();
                    SceneManager.SetActiveScene( sceneInstance.Scene );
                    return;
                }
                throw handle.Value.OperationException;
            } finally {
            }
        }
        public async Task UnloadAsync() {
            Assert.Operation.Message( $"SceneHandle {this} must be active" ).Valid( handle != null );
            try {
                await Addressables2.UnloadSceneAsync( handle.Value ).Task;
            } finally {
                handle = null;
            }
        }

        public override string ToString() {
            return $"SceneHandle: {key}";
        }

    }
}
