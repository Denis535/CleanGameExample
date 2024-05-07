#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {

        // GameObject
        protected override GameObject GameObject { get; }
        // CharacterController
        private CharacterController CharacterController { get; }
        // Input
        public bool IsMovePressed { get; private set; }
        public Vector3 MoveVector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }
        // Input
        public bool IsLookPressed { get; private set; }
        public Vector3 LookTarget { get; private set; }

        // Constructor
        public CharacterBody(GameObject gameObject) {
            GameObject = gameObject;
            CharacterController = gameObject.RequireComponent<CharacterController>();
        }
        public override void Dispose() {
        }

        // SetUp
        public void SetUp(ref bool initialize, bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            if (initialize) {
                initialize = false;
                IsMovePressed = isMovePressed;
                MoveVector = moveVector;
                IsJumpPressed = isJumpPressed;
                IsCrouchPressed = isCrouchPressed;
                IsAcceleratePressed = isAcceleratePressed;
            } else {
                IsMovePressed |= isMovePressed;
                MoveVector = Vector3.Max( MoveVector, moveVector );
                IsJumpPressed |= isJumpPressed;
                IsCrouchPressed |= isCrouchPressed;
                IsAcceleratePressed |= isAcceleratePressed;
            }
        }
        public void SetUp(bool isLookPressed, Vector3 lookTarget) {
            IsLookPressed = isLookPressed;
            LookTarget = lookTarget;
        }

        // Update
        public void UpdatePosition() {
            Assert.Operation.Message( $"Method 'MovePosition' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            if (IsMovePressed || IsJumpPressed || IsCrouchPressed || IsAcceleratePressed) {
                var velocity = GetVelocity( MoveVector, IsJumpPressed, IsCrouchPressed, IsAcceleratePressed );
                CharacterController.Move( velocity * GetDeltaTime() );
            }
        }
        public void UpdateRotation() {
            Assert.Operation.Message( $"Method 'MoveRotation' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (IsLookPressed) {
                var rotation = Transform.localRotation;
                var rotation2 = GetRotation( Transform.localPosition, LookTarget );
                Transform.localRotation = Quaternion.RotateTowards( rotation, rotation2, 3 * 360 * GetDeltaTime() );
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