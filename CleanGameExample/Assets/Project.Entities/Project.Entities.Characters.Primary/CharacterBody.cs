#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {
        public interface IInputActions {
            Vector3? GetMoveVector();
            Vector3? GetLookTarget();
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
            var moveVector = actions?.GetMoveVector() ?? Vector3.zero;
            var isJumpPressed = actions?.IsJumpPressed() ?? false;
            var isCrouchPressed = actions?.IsCrouchPressed() ?? false;
            var isAcceleratePressed = actions?.IsAcceleratePressed() ?? false;
            {
                var position = GetPosition( Rigidbody.position, moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
                Rigidbody.MovePosition( position );
            }
        }
        public void UpdateRotation(IInputActions? actions) {
            var lookTarget = actions?.GetLookTarget();
            if (lookTarget.HasValue) {
                var rotation = GetRotation( Rigidbody.rotation, Rigidbody.position, lookTarget.Value );
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
            return position + velocity * UnityUtils.DeltaTime;
        }
        private static Quaternion GetRotation(Quaternion rotation, Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation2 = Quaternion.LookRotation( direction, Vector3.up );
            //return Quaternion.RotateTowards( rotation, rotation2, 360f * 4f * UnityUtils.DeltaTime );
            return rotation2;
        }

    }
}
