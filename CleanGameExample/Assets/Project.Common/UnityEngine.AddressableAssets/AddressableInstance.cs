#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AddressableInstance : MonoBehaviour {

        public AsyncOperationHandle<GameObject> Prefab { get; set; }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
            Addressables.Release( Prefab );
        }

    }
}
