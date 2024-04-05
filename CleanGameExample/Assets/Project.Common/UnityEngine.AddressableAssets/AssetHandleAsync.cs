#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AssetHandleAsync<T> where T : notnull, UnityEngine.Object {

        private AsyncOperationHandle<T>? handle;

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
        public Task<T> AssetAsync {
            get {
                Assert.Operation.Message( $"Handle {this} must be active" ).Valid( handle != null );
                return handle!.Value.Task;
            }
        }

        // Constructor
        public AssetHandleAsync(string key) {
            Key = key;
        }

        // LoadAsync
        public async Task<T> LoadAsync() {
            // todo: CancellationToken
            // what if release was called?
            if (handle == null) {
                handle = Addressables.LoadAssetAsync<T>( Key );
            }
            if (handle.Value.Status is AsyncOperationStatus.None or AsyncOperationStatus.Succeeded) {
                var result = await handle.Value.Task;
                if (handle.Value.Status is AsyncOperationStatus.Succeeded) {
                    return result;
                }
            }
            throw handle.Value.OperationException;
        }
        public void Release() {
            if (handle != null) {
                Addressables.Release( handle.Value );
                handle = null;
            }
        }

        // Utils
        public override string ToString() {
            return $"AsyncAssetHandle: {Key}";
        }

    }
    public static class AssetHandleAsyncExtensions {

        public static void Release<T>(this IEnumerable<AssetHandleAsync<T>> collection) where T : notnull, UnityEngine.Object {
            foreach (var item in collection) {
                item.Release();
            }
        }

    }
}
