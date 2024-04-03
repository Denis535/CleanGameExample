#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {
        public record Arguments(Character Character);

        // Args
        private Arguments Args { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = InitializationContext.GetArguments<Player, Arguments>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

        // Spawn
        public void Spawn(PlayerSpawnPoint point) {
            
        }

    }
    // Character
    public enum Character {
        Gray,
        Red,
        Green,
        Blue
    }
}
