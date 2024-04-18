#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {
        public interface IContext {
            Vector3? GetMoveVector(CharacterBody character);
            Vector3? GetLookTarget(CharacterBody character);
            bool IsJumpPressed(CharacterBody character, out float duration);
            bool IsCrouchPressed(CharacterBody character);
            bool IsAcceleratePressed(CharacterBody character);
        }
        public record Arguments(IContext Context);

        // Args
        private Arguments Args { get; set; } = default!;
        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<CharacterBody, Arguments>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (Args.Context.GetMoveVector( this ).HasValue) {
                var position = GetPosition( Rigidbody.position, Args.Context.GetMoveVector( this )!.Value, Args.Context.IsJumpPressed( this, out _ ), Args.Context.IsCrouchPressed( this ), Args.Context.IsAcceleratePressed( this ) );
                Rigidbody.MovePosition( position );
            }
            if (Args.Context.GetLookTarget( this ).HasValue) {
                var rotation = GetRotation( Rigidbody.rotation, Rigidbody.position, Args.Context.GetLookTarget( this )!.Value );
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
                    velocity += move * 8;
                }
            }
            if (jump) {
                if (accelerate) {
                    velocity += Vector3.up * 13;
                } else {
                    velocity += Vector3.up * 8;
                }
            } else
            if (crouch) {
                if (accelerate) {
                    velocity -= Vector3.up * 13;
                } else {
                    velocity -= Vector3.up * 8;
                }
            }
            return position + velocity * Time.deltaTime;
        }
        private static Quaternion GetRotation(Quaternion rotation, Vector3 position, Vector3 target) {
            var direction = new Vector3( target.x - position.x, 0, target.z - position.z );
            var rotation2 = Quaternion.LookRotation( direction, Vector3.up );
            //return Quaternion.RotateTowards( rotation, rotation2, 360f * 4f * Time.deltaTime );
            return rotation2;
        }

    }
}
