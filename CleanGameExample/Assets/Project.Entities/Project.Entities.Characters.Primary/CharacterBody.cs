#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Input
        public Vector3 MoveInput { get; set; }
        public Vector3? TargetInput { get; set; }
        public bool JumpInput { get; set; }
        public bool CrouchInput { get; set; }
        public bool AccelerationInput { get; set; }

        // Awake
        public void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            Rigidbody.MovePosition( GetPosition( Rigidbody.position, MoveInput, JumpInput, CrouchInput, AccelerationInput ) );
            if (TargetInput.HasValue) {
                Rigidbody.rotation = GetRotation( Rigidbody.rotation, Rigidbody.position, TargetInput.Value );
            }
        }
        public void Update() {
        }

        // Helpers
        private static Vector3 GetPosition(Vector3 position, Vector3 move, bool jump, bool crouch, bool acceleration) {
            var velocity = Vector3.zero;
            if (move != default) {
                if (acceleration) {
                    velocity += move * 13;
                } else {
                    velocity += move * 8;
                }
            }
            if (jump) {
                if (acceleration) {
                    velocity += Vector3.up * 13;
                } else {
                    velocity += Vector3.up * 8;
                }
            } else
            if (crouch) {
                if (acceleration) {
                    velocity -= Vector3.up * 13;
                } else {
                    velocity -= Vector3.up * 8;
                }
            }
            return position + velocity * Time.fixedDeltaTime;
        }
        private static Quaternion GetRotation(Quaternion rotation, Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation2 = Quaternion.LookRotation( direction, Vector3.up );
            return Quaternion.RotateTowards( rotation, rotation2, 360f * 4f * Time.fixedDeltaTime );
        }

    }
}
