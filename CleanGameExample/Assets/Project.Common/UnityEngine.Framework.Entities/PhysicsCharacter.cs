#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // Note: Character consists of character-controller and its internals (head, body, hands, legs, weapon, etc).
    // Note: Character-controller should collide only with other character-controllers and don't affect other colliders or rays.
    // Note: While not moving character-controller can collide only with other character-controllers.
    // Note: While moving character-controller can collide with everything except internals (head, body, hands, legs, weapon, etc).
    public abstract class PhysicsCharacter : EntityBase {

        private bool fixedUpdateWasInvoked;

        // ExcludeLayersMask
        private static LayerMask ExcludeLayersMask_Inactive => ~Masks.CharacterEntity; // Exclude everything except CharacterEntity layer
        private static LayerMask ExcludeLayersMask_Active => Masks.CharacterEntityInternal; // Exclude CharacterEntityInternal layer

        // CharacterController
        private CharacterController CharacterController { get; set; } = default!;
        // Input
        public bool IsMovePressed { get; private set; }
        public Vector3 MoveVector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }
        // Input
        public bool IsLookPressed { get; private set; }
        public Vector3 LookTarget { get; private set; }

        // Awake
        public override void Awake() {
            CharacterController = gameObject.RequireComponent<CharacterController>();
            CharacterController.excludeLayers = ExcludeLayersMask_Inactive;
        }
        public override void OnDestroy() {
        }

        // SetMovementInput
        public void SetMovementInput(bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Method 'MoveRotation' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (fixedUpdateWasInvoked) {
                fixedUpdateWasInvoked = false;
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

        // SetLookInput
        public void SetLookInput(bool isLookPressed, Vector3 lookTarget) {
            Assert.Operation.Message( $"Method 'MoveRotation' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            IsLookPressed = isLookPressed;
            LookTarget = lookTarget;
        }

        // PhysicsFixedUpdate
        public void PhysicsFixedUpdate() {
            Assert.Operation.Message( $"Method 'MovePosition' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            fixedUpdateWasInvoked = true;
            if (IsMovePressed || IsJumpPressed || IsCrouchPressed || IsAcceleratePressed) {
                var velocity = GetVelocity( MoveVector, IsJumpPressed, IsCrouchPressed, IsAcceleratePressed );
                Move( velocity );
            }
        }

        // PhysicsUpdate
        public void PhysicsUpdate() {
            Assert.Operation.Message( $"Method 'MoveRotation' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (IsLookPressed) {
                var rotation = GetRotation( transform.localPosition, LookTarget );
                Rotate( rotation );
            }
        }

        // Move
        protected virtual void Move(Vector3 velocity) {
            CharacterController.excludeLayers = ExcludeLayersMask_Active;
            CharacterController.Move( velocity * Time.fixedDeltaTime );
            CharacterController.excludeLayers = ExcludeLayersMask_Inactive;
        }

        // Rotate
        protected virtual void Rotate(Quaternion rotation) {
            transform.localRotation = Quaternion.RotateTowards( transform.localRotation, rotation, 3 * 360 * Time.deltaTime );
        }

        // OnControllerColliderHit
        public void OnControllerColliderHit(ControllerColliderHit hit) {
            hit.rigidbody?.WakeUp();
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

    }
}
