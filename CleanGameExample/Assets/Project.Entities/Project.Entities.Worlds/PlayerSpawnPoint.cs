#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class PlayerSpawnPoint : EntityBase {

        // OnValidate
        public void OnValidate() {
            gameObject.name = "PlayerSpawnPoint";
        }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

    }
}
