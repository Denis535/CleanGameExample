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
        public Vector3 MoveVector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }
        // Input
        public Quaternion? LookRotation { get; private set; }

        // Awake
        protected void Awake() {
            Collider = gameObject.RequireComponent<CharacterController>();
            Collider.excludeLayers = ExcludeLayers_Default;
        }
        private void OnDestroy() {
        }

        // OnEnable
        protected void OnEnable() {
            Collider.enabled = true;
        }
        protected void OnDisable() {
            Collider.enabled = false;
        }

        // FixedUpdate
        public void FixedUpdate2() {
            Assert.Operation.Message( $"Method 'FixedUpdate' must be invoked only within fixed update" ).Valid( Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            fixedUpdateWasInvoked = true;
            if (enabled) {
                var velocity = Vector3.zero;
                if (MoveVector != Vector3.zero) {
                    if (IsAcceleratePressed) {
                        velocity += MoveVector * 13;
                    } else {
                        velocity += MoveVector * 5;
                    }
                }
                if (IsJumpPressed) {
                    if (IsAcceleratePressed) {
                        velocity += Vector3.up * 13;
                    } else {
                        velocity += Vector3.up * 5;
                    }
                } else
                if (IsCrouchPressed) {
                    if (IsAcceleratePressed) {
                        velocity -= Vector3.up * 13;
                    } else {
                        velocity -= Vector3.up * 5;
                    }
                }
                Collider.excludeLayers = ExcludeLayers_WhenMoving;
                var flags = Collider.Move( velocity * Time.fixedDeltaTime );
                Collider.excludeLayers = ExcludeLayers_Default;
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
        public void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Method 'Move' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( enabled );
            if (fixedUpdateWasInvoked) {
                fixedUpdateWasInvoked = false;
                MoveVector = moveVector;
                IsJumpPressed = isJumpPressed;
                IsCrouchPressed = isCrouchPressed;
                IsAcceleratePressed = isAcceleratePressed;
            } else {
                MoveVector = Vector3.Max( MoveVector, moveVector );
                IsJumpPressed |= isJumpPressed;
                IsCrouchPressed |= isCrouchPressed;
                IsAcceleratePressed |= isAcceleratePressed;
            }
        }

        // LookAt
        public void LookAt(Quaternion? rotation) {
            Assert.Operation.Message( $"Method 'LookAt' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( enabled );
            LookRotation = rotation;
            if (LookRotation != null) {
                transform.localRotation = Quaternion.RotateTowards( transform.localRotation, LookRotation.Value, 3 * 360 * Time.deltaTime );
            }
        }

        // LookAt
        public void LookAt(Vector3? target) {
            Assert.Operation.Message( $"Method 'LookAt' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( enabled );
            if (target != null) {
                LookAt( GetRotation( transform.position, target.Value ) );
            } else {
                LookAt( (Quaternion?) null );
            }
        }

        // OnControllerColliderHit
        protected void OnControllerColliderHit(ControllerColliderHit hit) {
            hit.rigidbody?.WakeUp();
        }

        // Helpers
        private static Vector3 GetDirection(Vector3 position, Vector3 target) {
            var direction = target - position;
            direction = new Vector3( direction.x, 0, direction.z );
            direction = direction.normalized;
            return direction;
        }
        private static Quaternion GetRotation(Vector3 position, Vector3 target) {
            var direction = GetDirection( position, target );
            return Quaternion.LookRotation( direction, Vector3.up );
        }

    }
}
