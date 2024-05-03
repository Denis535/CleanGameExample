#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {

        // Components
        private CharacterController CharacterController { get; set; } = default!;

        // Awake
        public void Awake() {
            CharacterController = gameObject.RequireComponent<CharacterController>();
        }
        public void OnDestroy() {
        }

        // Move
        public void MovePosition(bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Method 'MovePosition' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            var velocity = GetVelocity( moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
            CharacterController.Move( velocity * GetDeltaTime() );
        }
        public void MoveRotation(bool isLookPressed, Vector3 lookTarget) {
            Assert.Operation.Message( $"Method 'MoveRotation' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (isLookPressed) {
                var rotation = GetRotation( transform.localPosition, lookTarget );
                transform.localRotation = Quaternion.RotateTowards( transform.localRotation, rotation, 3 * 360 * GetDeltaTime() );
            }
        }

        // Helpers
        private static Vector3 GetVelocity(Vector3 move, bool jump, bool crouch, bool accelerate) {
            var velocity = Vector3.zero;
            if (move != default) {
                if (accelerate) {
                    velocity += move * 13;
                } else {
                    velocity += move * 5;
                }
            }
            if (jump) {
                if (accelerate) {
                    velocity += Vector3.up * 13;
                } else {
                    velocity += Vector3.up * 5;
                }
            } else
            if (crouch) {
                if (accelerate) {
                    velocity -= Vector3.up * 13;
                } else {
                    velocity -= Vector3.up * 5;
                }
            }
            return velocity;
        }
        private static Quaternion GetRotation(Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            return Quaternion.LookRotation( direction, Vector3.up );
        }
        // Heleprs
        private static float GetDeltaTime() {
            return Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime;
        }

    }
}
