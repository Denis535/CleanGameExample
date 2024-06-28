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
    public class MoveableBody : MonoBehaviour {

        private bool fixedUpdateWasInvoked;

        // ExcludeLayers
        private static LayerMask ExcludeLayers_Default => ~Masks.CharacterEntity; // Exclude everything except CharacterEntity layer
        private static LayerMask ExcludeLayers_WhenMoving => Masks.CharacterEntityInternal; // Exclude only CharacterEntityInternal layer

        // Collider
        private CharacterController Collider { get; set; } = default!;
        // Input
        public Vector3 Vector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }
        // Input
        public Vector3? Target { get; private set; }

        // Awake
        private void Awake() {
            Collider = gameObject.RequireComponent<CharacterController>();
            Collider.excludeLayers = ExcludeLayers_Default;
        }
        private void OnDestroy() {
        }

        // OnEnable
        private void OnEnable() {
            Collider.enabled = true;
        }
        private void OnDisable() {
            Collider.enabled = false;
        }

        // FixedUpdate
        public void FixedUpdate2() {
            Assert.Operation.Message( $"Method 'FixedUpdate' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            fixedUpdateWasInvoked = true;
            if (enabled) {
                Move( Collider, Vector, IsJumpPressed, IsCrouchPressed, IsAcceleratePressed );
            }
        }

        // Update
        public void Update2() {
            Assert.Operation.Message( $"Method 'Update' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            if (enabled) {
            }
        }

        // Move
        public void Move(Vector3 vector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Method 'Move' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( enabled );
            if (fixedUpdateWasInvoked) {
                fixedUpdateWasInvoked = false;
                Vector = vector;
                IsJumpPressed = isJumpPressed;
                IsCrouchPressed = isCrouchPressed;
                IsAcceleratePressed = isAcceleratePressed;
            } else {
                Vector = Vector3.Max( Vector, vector );
                IsJumpPressed |= isJumpPressed;
                IsCrouchPressed |= isCrouchPressed;
                IsAcceleratePressed |= isAcceleratePressed;
            }
        }

        // RotateAt
        public void RotateAt(Vector3? target) {
            Assert.Operation.Message( $"Method 'RotateAt' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( enabled );
            Target = target;
            if (Target != null) {
                SetRotation( Collider, Target.Value );
            }
        }

        // OnControllerColliderHit
        private void OnControllerColliderHit(ControllerColliderHit hit) {
            hit.rigidbody?.WakeUp();
        }

        // Helpers
        private static CollisionFlags Move(CharacterController collider, Vector3 move, bool jump, bool crouch, bool accelerate) {
            var velocity = Vector3.zero;
            if (move != Vector3.zero) {
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
            try {
                collider.excludeLayers = ExcludeLayers_WhenMoving;
                return collider.Move( velocity * Time.fixedDeltaTime );
            } finally {
                collider.excludeLayers = ExcludeLayers_Default;
            }
        }
        private static void SetRotation(CharacterController collider, Vector3 target) {
            var position = collider.transform.position;
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation = Quaternion.LookRotation( direction, Vector3.up );
            collider.transform.localRotation = Quaternion.RotateTowards( collider.transform.localRotation, rotation, 3 * 360 * Time.deltaTime );
        }

    }
}
