#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterView : EntityViewBase {

        // Target
        //public Vector3? Target { get; set; }
        // Input
        public bool FireInput { get; set; }
        public bool AimInput { get; set; }
        public bool InteractInput { get; set; }
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
            //if (Target.HasValue) {
            //    transform.LookAt( new Vector3( Target.Value.x, transform.position.y, Target.Value.z ), Vector3.up );
            //}
        }

    }
}
