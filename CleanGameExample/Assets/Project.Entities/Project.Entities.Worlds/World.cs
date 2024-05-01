#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class World : WorldBase {

        // SpawnPoints
        public PlayerSpawnPoint[] PlayerSpawnPoints { get; private set; } = default!;
        public EnemySpawnPoint[] EnemySpawnPoints { get; private set; } = default!;
        public LootSpawnPoint[] LootSpawnPoints { get; private set; } = default!;

        // Awake
        public void Awake() {
            PlayerSpawnPoints = Object2.RequireObjectsByType<PlayerSpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            EnemySpawnPoints = Object2.RequireObjectsByType<EnemySpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            LootSpawnPoints = Object2.RequireObjectsByType<LootSpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
        }
        public void OnDestroy() {
        }

    }
}
