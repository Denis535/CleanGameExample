#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyCharacter : Character {

        // Awake
        public override void Awake() {
            base.Awake();
        }
        public override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        public override void Start() {
        }
        public override void FixedUpdate() {
            PhysicsFixedUpdate();
        }
        public override void Update() {
        }

    }
}
