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
        public Vector3? MoveVector { get; private set; }
        public Vector3? LookTarget { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }

        // Awake
        public void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (MoveVector.HasValue) {
                Rigidbody.MovePosition( GetPosition( Rigidbody.position, MoveVector.Value, IsJumpPressed, IsCrouchPressed, IsAcceleratePressed ) );
            }
            if (LookTarget.HasValue) {
                Rigidbody.MoveRotation( GetRotation( Rigidbody.rotation, Rigidbody.position, LookTarget.Value ) );
            }
        }

        // Input
        public void Move(Vector3? vector) {
            MoveVector = vector;
        }
        public void LookAt(Vector3? target) {
            LookTarget = target;
        }
        public void Jump(bool isPressed) {
            IsJumpPressed = isPressed;
        }
        public void Crouch(bool isPressed) {
            IsCrouchPressed = isPressed;
        }
        public void Accelerate(bool isPressed) {
            IsAcceleratePressed = isPressed;
        }

        // Helpers
        private static Vector3 GetPosition(Vector3 position, Vector3 move, bool jump, bool crouch, bool accelerate) {
            var velocity = Vector3.zero;
            if (move != default) {
                if (accelerate) {
                    velocity += move * 13;
                } else {
                    velocity += move * 8;
                }
            }
            if (jump) {
                if (accelerate) {
                    velocity += Vector3.up * 13;
                } else {
                    velocity += Vector3.up * 8;
                }
            } else
            if (crouch) {
                if (accelerate) {
                    velocity -= Vector3.up * 13;
                } else {
                    velocity -= Vector3.up * 8;
                }
            }
            return position + velocity * Time.deltaTime;
        }
        private static Quaternion GetRotation(Quaternion rotation, Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation2 = Quaternion.LookRotation( direction, Vector3.up );
            //return Quaternion.RotateTowards( rotation, rotation2, 360f * 4f * Time.deltaTime );
            return rotation2;
        }

    }
}
