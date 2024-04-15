#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( CharacterBody ) )]
    [RequireComponent( typeof( CharacterView ) )]
    public class Character : EntityBase {

        // View
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Camera
        public Transform? Camera { get; set; }
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
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (MoveDirectionInput != default) {
                View.Target = transform.position + MoveDirectionInput * 1024;
            }
            View.FireInput = FireInput;
            View.AimInput = AimInput;
            View.InteractInput = InteractInput;
            View.MoveDirectionInput = Body.MoveDirectionInput = MoveDirectionInput;
            View.JumpInput = Body.JumpInput = JumpInput;
            View.CrouchInput = Body.CrouchInput = CrouchInput;
            View.AccelerationInput = Body.AccelerationInput = AccelerationInput;
        }

        // OnDrawGizmos
        public void OnDrawGizmos() {
            //if (Target != null) {
            //    Gizmos.color = Color.red;
            //    Gizmos.DrawSphere( Target.Value, 0.1f );
            //}
        }

    }
}
