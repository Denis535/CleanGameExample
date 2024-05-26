#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class World : WorldBase {

        private PlayerPoint[] playerPoints = default!;
        private EnemyPoint[] enemyPoints = default!;
        private LootPoint[] lootPoints = default!;

        // Points
        public PlayerPoint[] PlayerPoints => this.Validate().playerPoints;
        public EnemyPoint[] EnemyPoints => this.Validate().enemyPoints;
        public LootPoint[] LootPoints => this.Validate().lootPoints;

#if UNITY_EDITOR
        public void OnValidate() {
            if (!Application.isPlaying) {
                foreach (var gameObject in gameObject.scene.GetRootGameObjects()) {
                    if (gameObject != base.gameObject) {
                        if (gameObject.isStatic) {
                            gameObject.transform.parent = base.transform;
                        } else {
                            gameObject.transform.parent = null;
                        }
                    }
                }
            }
        }
#endif

        // Awake
        public override void Awake() {
            playerPoints = Object2.RequireObjectsByType<PlayerPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            enemyPoints = Object2.RequireObjectsByType<EnemyPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            lootPoints = Object2.RequireObjectsByType<LootPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
        }
        public override void OnDestroy() {
        }

    }
}
