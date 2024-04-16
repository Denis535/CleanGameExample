#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( CharacterBody ) )]
    [RequireComponent( typeof( CharacterView ) )]
    public class Character : EntityBase {
        public record Arguments(Camera2 Camera);

        // Args
        private Arguments Args { get; set; } = default!;
        // View
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Input
        public bool IsFirePressed { get; private set; }
        public bool IsAimPressed { get; private set; }
        public bool IsInteractPressed { get; private set; }
        // Input
        public Vector3? MoveVector => Body.MoveVector;
        public Vector3? LookTarget => Body.LookTarget;
        public bool IsJumpPressed => Body.IsJumpPressed;
        public bool IsCrouchPressed => Body.IsCrouchPressed;
        public bool IsAcceleratePressed => Body.IsAcceleratePressed;

        // Awake
        public void Awake() {
            Args = Context.Get<Character, Arguments>();
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
            Args.Camera.SetTarget( transform );
        }

        // Input
        public void Fire(bool isPressed, bool thisFrame) {
            IsFirePressed = isPressed;
        }
        public void Aim(bool isPressed, bool thisFrame) {
            IsAimPressed = isPressed;
        }
        public void Interact(bool isPressed, bool thisFrame) {
            IsInteractPressed = thisFrame;
        }
        // Input
        public void Move(Vector3? vector, bool isPressed, bool thisFrame) {
            if (vector.HasValue) {
                Body.Move( GetMoveVector( vector.Value, Args.Camera.transform ) );
            } else {
                Body.Move( null );
            }
        }
        public void LookAt(Vector3? target, bool thisFrame) {
            Body.LookAt( target );
        }
        public void Jump(bool isPressed, bool thisFrame) {
            Body.Jump( isPressed );
        }
        public void Crouch(bool isPressed, bool thisFrame) {
            Body.Crouch( isPressed );
        }
        public void Accelerate(bool isPressed, bool thisFrame) {
            Body.Accelerate( isPressed );
        }

        // Helpers
        private static Vector3 GetMoveVector(Vector2 vector, Transform camera) {
            var result = camera.TransformDirection( vector.x, 0, vector.y );
            return new Vector3( result.x, 0, result.z ).normalized * vector.magnitude;
        }

    }
}
