#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // Note: Character consists of character-controller and its internals (head, body, hands, legs, weapon, etc).
    // Note: Character-controller should collide only with other character-controllers and don't affect other colliders or rays.
    // Note: While not moving character-controller can collide only with other character-controllers.
    // Note: While moving character-controller can collide with everything except internals (head, body, hands, legs, weapon, etc).
    [RequireComponent( typeof( CharacterController ) )]
    public class CharacterPhysics : MonoBehaviour {

        private bool fixedUpdateWasInvoked;

        // ExcludeLayers
        private static LayerMask ExcludeLayers_Default => ~Masks.CharacterEntity; // Exclude everything except CharacterEntity layer
        private static LayerMask ExcludeLayers_WhenMoving => Masks.CharacterEntityInternal; // Exclude only CharacterEntityInternal layer

        // CharacterController
        protected CharacterController CharacterController { get; private set; } = default!;
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
        protected virtual void Awake() {
            CharacterController = gameObject.RequireComponent<CharacterController>();
            CharacterController.excludeLayers = ExcludeLayers_Default;
        }
        protected virtual void OnDestroy() {
        }

        // OnEnable
        protected void OnEnable() {
            CharacterController.enabled = true;
        }
        protected void OnDisable() {
            CharacterController.enabled = false;
        }

        // SetMovementInput
        public void SetMovementInput(bool isMovePressed, Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Object.Message( $"PhysicsCharacter {this} must be awakened" ).Ready( didAwake );
            Assert.Object.Message( $"PhysicsCharacter {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"PhysicsCharacter {this} must be enabled" ).Valid( enabled );
            Assert.Operation.Message( $"Method 'SetMovementInput' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
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
            Assert.Object.Message( $"PhysicsCharacter {this} must be awakened" ).Ready( didAwake );
            Assert.Object.Message( $"PhysicsCharacter {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"PhysicsCharacter {this} must be enabled" ).Valid( enabled );
            Assert.Operation.Message( $"Method 'SetLookInput' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            IsLookPressed = isLookPressed;
            LookTarget = lookTarget;
        }

        // PhysicsFixedUpdate
        public virtual void PhysicsFixedUpdate() {
            Assert.Object.Message( $"PhysicsCharacter {this} must be awakened" ).Ready( didAwake );
            Assert.Object.Message( $"PhysicsCharacter {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"PhysicsCharacter {this} must be enabled" ).Valid( enabled );
            Assert.Operation.Message( $"Method 'PhysicsFixedUpdate' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            fixedUpdateWasInvoked = true;
            if (IsMovePressed || IsJumpPressed || IsCrouchPressed || IsAcceleratePressed) {
                Move( GetVelocity( MoveVector, IsJumpPressed, IsCrouchPressed, IsAcceleratePressed ) );
            }
        }

        // PhysicsUpdate
        public virtual void PhysicsUpdate() {
            Assert.Object.Message( $"PhysicsCharacter {this} must be awakened" ).Ready( didAwake );
            Assert.Object.Message( $"PhysicsCharacter {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"PhysicsCharacter {this} must be enabled" ).Valid( enabled );
            Assert.Operation.Message( $"Method 'PhysicsUpdate' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (IsLookPressed) {
                Rotate( GetRotation( transform.localPosition, LookTarget ) );
            }
        }

        // Move
        protected virtual CollisionFlags Move(Vector3 velocity) {
            try {
                CharacterController.excludeLayers = ExcludeLayers_WhenMoving;
                return CharacterController.Move( velocity * Time.fixedDeltaTime );
            } finally {
                CharacterController.excludeLayers = ExcludeLayers_Default;
            }
        }

        // Rotate
        protected virtual void Rotate(Quaternion rotation) {
            transform.localRotation = Quaternion.RotateTowards( transform.localRotation, rotation, 3 * 360 * Time.deltaTime );
        }

        // OnControllerColliderHit
        protected virtual void OnControllerColliderHit(ControllerColliderHit hit) {
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
