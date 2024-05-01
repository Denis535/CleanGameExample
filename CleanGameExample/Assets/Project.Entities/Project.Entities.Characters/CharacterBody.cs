#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {

        // Components
        private Rigidbody Rigidbody { get; set; } = default!;
        private Collider Collider { get; set; } = default!;

        // Awake
        public void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Collider = gameObject.RequireComponent<Collider>();
        }
        public void OnDestroy() {
        }

        // FixedUpdate
        public void FixedUpdatePosition(bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Method 'FixedUpdatePosition' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            var position = GetPosition( Rigidbody.position, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
            Rigidbody.MovePosition( position );
        }
        public void FixedUpdateRotation(bool isLookPressed, Vector3 lookTarget) {
            Assert.Operation.Message( $"Method 'FixedUpdateRotation' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            if (isLookPressed && lookTarget != null) {
                var rotation = GetRotation( Rigidbody.rotation, Rigidbody.position, lookTarget );
                Rigidbody.MoveRotation( rotation );
            }
        }

        // Helpers
        private static Vector3 GetPosition(Vector3 position, Vector3 move, bool jump, bool crouch, bool accelerate) {
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
            return position + velocity * Time.fixedDeltaTime;
        }
        private static Quaternion GetRotation(Quaternion rotation, Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation2 = Quaternion.LookRotation( direction, Vector3.up );
            //return Quaternion.RotateTowards( rotation, rotation2, 360f * 4f * Time.fixedDeltaTime );
            return rotation2;
        }

    }
}
