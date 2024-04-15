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
        // Input
        public Vector3 MoveInput { get; set; }
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
            Body.TargetInput = null;
            if (MoveInput != default) {
                View.TargetInput = Body.TargetInput = transform.position + MoveInput * 1024 + Vector3.up * 1.75f;
            }
            if (FireInput || AimInput || InteractInput) {
                if (Camera != null) {
                    View.TargetInput = Body.TargetInput = GetTarget( Camera, out _ );
                }
            }
            View.FireInput = FireInput;
            View.AimInput = AimInput;
            View.InteractInput = InteractInput;
            View.MoveInput = Body.MoveInput = MoveInput;
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

        // Helpers
        private static Vector3? GetTarget(Transform camera, out GameObject? @object) {
            var mask = ~0;
            if (Physics.Raycast( camera.position, camera.forward, out var hit, 256, mask, QueryTriggerInteraction.Ignore )) {
                @object = hit.transform.gameObject;
                return hit.point;
            } else {
                @object = null;
                return camera.TransformPoint( Vector3.forward * 1024 );
            }
        }

    }
}
