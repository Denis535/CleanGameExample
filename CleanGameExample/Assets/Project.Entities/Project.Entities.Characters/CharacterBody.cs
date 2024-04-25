#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {
        public interface IInputActions {
            bool IsMovePressed(out Vector3 moveVector);
            bool IsLookPressed(out Vector3? lookTarget);
            bool IsJumpPressed();
            bool IsCrouchPressed();
            bool IsAcceleratePressed();
        }

        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;

        // Awake
        public void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public void OnDestroy() {
        }

        // Update
        public void UpdatePosition(IInputActions? actions) {
            if (actions != null) {
                var isMovePressed = actions.IsMovePressed( out var moveVector );
                var isJumpPressed = actions.IsJumpPressed();
                var isCrouchPressed = actions.IsCrouchPressed();
                var isAcceleratePressed = actions.IsAcceleratePressed();
                {
                    var position = GetPosition( Rigidbody.position, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
                    Rigidbody.MovePosition( position );
                }
            }
        }
        public void UpdateRotation(IInputActions? actions) {
            if (actions != null) {
                var isLookPressed = actions.IsLookPressed( out var lookTarget );
                if (isLookPressed && lookTarget != null) {
                    var rotation = GetRotation( Rigidbody.rotation, Rigidbody.position, lookTarget.Value );
                    Rigidbody.MoveRotation( rotation );
                }
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
            return position + velocity * Utils.DeltaTime;
        }
        private static Quaternion GetRotation(Quaternion rotation, Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation2 = Quaternion.LookRotation( direction, Vector3.up );
            //return Quaternion.RotateTowards( rotation, rotation2, 360f * 4f * UnityUtils.DeltaTime );
            return rotation2;
        }

    }
}
