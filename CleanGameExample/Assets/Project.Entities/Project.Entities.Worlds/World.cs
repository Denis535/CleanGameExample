#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using Object = UnityEngine.Object;

    public class World : WorldBase {

        // PlayerSpawnPoints
        public PlayerSpawnPoint[] PlayerSpawnPoints { get; private set; } = default!;
        // EnemySpawnPoints
        public EnemySpawnPoint[] EnemySpawnPoints { get; private set; } = default!;
        // LootSpawnPoints
        public LootSpawnPoint[] LootSpawnPoints { get; private set; } = default!;

        // Awake
        public void Awake() {
            PlayerSpawnPoints = Object2.RequireObjectsByType<PlayerSpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            EnemySpawnPoints = Object.FindObjectsByType<EnemySpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            LootSpawnPoints = Object.FindObjectsByType<LootSpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
        }
        public void OnDestroy() {
        }

    }
}
