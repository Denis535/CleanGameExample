#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // Note: Character consists of approximate capsule-collider and exact colliders (body, head, hands, legs, weapon, etc).
    [RequireComponent( typeof( CharacterController ) )]
    public class MoveableBody : MonoBehaviour {

        private bool fixedUpdateWasInvoked;

        private static LayerMask ExcludeLayers_Default => ~(Masks.Entity_Approximate);
        private static LayerMask ExcludeLayers_WhenMoving => (Masks.Entity_Exact | Masks.Trivial);

        private CharacterController Collider { get; set; } = default!;
        public Vector3 MoveVector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }
        public Quaternion? LookRotation { get; private set; }

        protected void Awake() {
            Collider = gameObject.RequireComponent<CharacterController>();
            Collider.excludeLayers = ExcludeLayers_Default;
        }
        protected void OnDestroy() {
        }

        protected void OnEnable() {
            Collider.enabled = true;
        }
        protected void OnDisable() {
            Collider.enabled = false;
        }

        protected void FixedUpdate() {
            fixedUpdateWasInvoked = true;
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
        protected void Update() {
        }

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

        protected void OnControllerColliderHit(ControllerColliderHit hit) {
            hit.rigidbody?.WakeUp();
        }

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
