#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class World : WorldBase {

        // PlayerSpawnPoints
        public PlayerSpawnPoint[] PlayerSpawnPoints { get; private set; } = default!;

        // Awake
        public void Awake() {
            PlayerSpawnPoints = gameObject.RequireComponentsInChildren<PlayerSpawnPoint>();
        }
        public void OnDestroy() {
        }

    }
}
