#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class World : WorldBase {

        private PlayerSpawnPoint[] playerSpawnPoints = default!;
        private EnemySpawnPoint[] enemySpawnPoints = default!;
        private LootSpawnPoint[] lootSpawnPoints = default!;

        // SpawnPoints
        public PlayerSpawnPoint[] PlayerSpawnPoints => this.Validate().playerSpawnPoints ?? throw Exceptions.Internal.NullReference( $"Reference 'PlayerSpawnPoints' is null" );
        public EnemySpawnPoint[] EnemySpawnPoints => this.Validate().enemySpawnPoints ?? throw Exceptions.Internal.NullReference( $"Reference 'EnemySpawnPoints' is null" );
        public LootSpawnPoint[] LootSpawnPoints => this.Validate().lootSpawnPoints ?? throw Exceptions.Internal.NullReference( $"Reference 'LootSpawnPoints' is null" );

        // Awake
        public override void Awake() {
            playerSpawnPoints = Object2.RequireObjectsByType<PlayerSpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            enemySpawnPoints = Object2.RequireObjectsByType<EnemySpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            lootSpawnPoints = Object2.RequireObjectsByType<LootSpawnPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
        }
        public override void OnDestroy() {
        }

    }
}
