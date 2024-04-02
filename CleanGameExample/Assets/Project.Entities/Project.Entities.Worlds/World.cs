#nullable enable
namespace Project.Entities.Worlds {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class World : WorldBase {

        // PlayerStarts
        public PlayerStart[] PlayerStarts { get; private set; } = default!;

        // Awake
        public void Awake() {
            PlayerStarts = gameObject.RequireComponentsInChildren<PlayerStart>();
        }
        public void OnDestroy() {
        }

    }
}
