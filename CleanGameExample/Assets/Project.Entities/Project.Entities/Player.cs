#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {
        public record Arguments(Character Character);

        // Args
        private Arguments Args { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = (Arguments) this.GetArguments();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
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
