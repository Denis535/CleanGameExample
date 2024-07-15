#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase3 {

        private PlayerCharacter? character;
        private Camera2? camera;

        internal InputActions_Player Input { get; private set; }
        public PlayerCharacter? Character {
            get => character;
            internal set {
                Input.Disable();
                if (character != null) {
                    character.Input = null;
                }
                if (camera != null) {
                    camera.Target = null;
                }
                character = value;
                if (character != null) {
                    character.Input = new PlayableCharacterInput( this );
                }
                if (camera != null) {
                    camera.Target = character;
                }
            }
        }
        public Camera2? Camera {
            get => camera;
            internal set {
                Input.Disable();
                if (camera != null) {
                    camera.Input = null;
                    camera.Target = null;
                }
                camera = value;
                if (camera != null) {
                    camera.Input = new CameraInput( this );
                    camera.Target = Character;
                }
            }
        }

        public Player(IDependencyContainer container, PlayerInfo info) : base( container, info ) {
            Input = new InputActions_Player();
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            Input.SetEnabled( Character != null && Camera != null && Cursor.lockState == CursorLockMode.Locked && Time.timeScale != 0f );
        }
        public void OnLateUpdate() {
        }

    }
    internal class PlayableCharacterInput : IPlayableCharacterInput {

        private Player Player { get; }
        private InputActions_Player.CharacterActions Input => Player.Input.Character;
        private Character Character => Player.Character!;
        private Camera2 Camera => Player.Camera!;
        private Camera2.RaycastHit? Hit => Camera.Hit;
        private Vector3 Target => Camera.Hit?.Point ?? Camera.transform.TransformPoint( Vector3.forward * 128f );

        public PlayableCharacterInput(Player player) {
            Player = player;
        }

        public Vector3 GetMoveVector() {
            if (Input.Move.IsPressed()) {
                var vector = Input.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                vector = Camera.transform.TransformDirection( vector );
                vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public Vector3? GetBodyTarget() {
            if (Input.Aim.IsPressed() || Input.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Move.IsPressed()) {
                var vector = Input.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                if (vector != Vector3.zero) {
                    vector = Camera.transform.TransformDirection( vector );
                    vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                    return Character.transform.position + vector;
                }
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            if (Input.Aim.IsPressed() || Input.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Move.IsPressed()) {
                return Target;
            }
            return Target;
        }
        public Vector3? GetWeaponTarget() {
            if (Input.Aim.IsPressed() || Input.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public bool IsJumpPressed() {
            return Input.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            return Input.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            return Input.Accelerate.IsPressed();
        }
        public bool IsFirePressed() {
            return Input.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            return Input.Aim.IsPressed();
        }
        public bool IsInteractPressed(out MonoBehaviour? interactable) {
            interactable = (MonoBehaviour?) Hit?.Enemy ?? Hit?.Thing;
            return Input.Interact.WasPressedThisFrame();
        }

    }
    internal class CameraInput : ICameraInput {

        private Player Player { get; }
        private InputActions_Player.CameraActions Input => Player.Input.Camera;

        public CameraInput(Player player) {
            Player = player;
        }

        public Vector2 GetLookDelta() {
            return Input.Look.ReadValue<Vector2>();
        }
        public float GetZoomDelta() {
            return Input.Zoom.ReadValue<Vector2>().y;
        }

    }
}
