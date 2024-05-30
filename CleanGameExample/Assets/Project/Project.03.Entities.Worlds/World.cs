#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class World : MonoBehaviour {

        private PlayerPoint[] playerPoints = default!;
        private EnemyPoint[] enemyPoints = default!;
        private ThingPoint[] thingPoints = default!;

        // Points
        public PlayerPoint[] PlayerPoints => this.Validate().playerPoints;
        public EnemyPoint[] EnemyPoints => this.Validate().enemyPoints;
        public ThingPoint[] ThingPoints => this.Validate().thingPoints;

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
        public void Awake() {
            playerPoints = Object2.RequireObjectsByType<PlayerPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            enemyPoints = Object2.RequireObjectsByType<EnemyPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            thingPoints = Object2.RequireObjectsByType<ThingPoint>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
        }
        public void OnDestroy() {
        }

    }
}
