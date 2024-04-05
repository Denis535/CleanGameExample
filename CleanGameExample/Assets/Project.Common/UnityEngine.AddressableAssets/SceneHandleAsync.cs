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

    public class SceneHandleAsync {

        private AsyncOperationHandle<SceneInstance>? handle;

        public string Key { get; }
        public bool IsActive => handle != null;
        public bool IsValid {
            get {
                Assert.Operation.Message( $"Handle {this} must be active" ).Valid( handle != null );
                return handle.Value.IsValid();
            }
        }
        public bool IsSucceeded {
            get {
                Assert.Operation.Message( $"Handle {this} must be active" ).Valid( handle != null );
                return handle.Value.IsSucceeded();
            }
        }
        public bool IsFailed {
            get {
                Assert.Operation.Message( $"Handle {this} must be active" ).Valid( handle != null );
                return handle.Value.IsFailed();
            }
        }
        public Task<Scene> SceneAsync {
            get {
                Assert.Operation.Message( $"Handle {this} must be active" ).Valid( handle != null );
                return handle.Value.Task.ContinueWith( i => i.Result.Scene );
            }
        }

        // Constructor
        public SceneHandleAsync(string key) {
            Key = key;
        }

        // LoadAsync
        public async Task<Scene> LoadAsync(LoadSceneMode loadMode, bool activateOnLoad) {
            Assert.Operation.Message( $"Handle {this} must be non-active" ).Valid( handle == null );
            handle = Addressables.LoadSceneAsync( Key, loadMode, activateOnLoad );
            if (handle.Value.IsValid() && handle.Value.IsFailed()) {
                throw handle.Value.OperationException;
            }
            var sceneInstance = await handle.Value.Task;
            if (handle.Value.IsValid() && handle.Value.IsSucceeded()) {
                return sceneInstance.Scene;
            }
            throw handle.Value.OperationException;
        }
        public async Task<Scene> ActivateAsync() {
            Assert.Operation.Message( $"Handle {this} must be active" ).Valid( handle != null );
            var sceneInstance = await handle.Value.Task;
            await sceneInstance.ActivateAsync();
            return sceneInstance.Scene;
        }
        public async Task UnloadAsync() {
            if (handle != null) {
                await Addressables.UnloadSceneAsync( handle.Value ).Task;
                handle = null;
            }
        }

        // Utils
        public override string ToString() {
            return $"AsyncSceneHandle: {Key}";
        }

    }
}
