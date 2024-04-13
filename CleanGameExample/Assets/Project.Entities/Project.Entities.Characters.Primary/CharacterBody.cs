#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {

        // Input
        public Vector3 MoveInput { get; set; }
        public Vector3 LookInput { get; set; }
        public bool JumpInput { get; set; }
        public bool CrouchInput { get; set; }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }
        public void FixedUpdate() {
        }

    }
}
