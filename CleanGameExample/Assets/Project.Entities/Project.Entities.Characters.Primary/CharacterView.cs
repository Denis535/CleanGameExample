#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterView : EntityViewBase {

        // Input
        public bool FireInput { get; set; }
        public bool AimInput { get; set; }
        public bool InteractInput { get; set; }
        public Vector3 LookDirectionInput { get; set; }
        public Vector3 MoveDirectionInput { get; set; }
        public bool JumpInput { get; set; }
        public bool CrouchInput { get; set; }
        public bool AccelerationInput { get; set; }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (LookDirectionInput != default) {
                transform.forward = new Vector3( LookDirectionInput.x, 0, LookDirectionInput.z ).normalized;
            }
        }

    }
}
