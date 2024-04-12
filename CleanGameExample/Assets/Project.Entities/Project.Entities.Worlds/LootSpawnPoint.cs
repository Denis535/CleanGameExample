#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LootSpawnPoint : MonoBehaviour {

        // OnValidate
        public void OnValidate() {
            gameObject.name = GetType().Name;
        }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

    }
}
