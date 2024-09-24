#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Actors;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : PlayerBase3 {

        private PlayerState state;
        private PlayerCharacter? character;
        private Camera2? camera;

        private InputActions_Character CharacterInput { get; set; }
        private InputActions_Camera CameraInput { get; set; }

        public PlayerState State {
            get => state;
            internal set {
                Assert.Operation.Message( $"Transition from {State} to {value} is invalid" ).Valid( value != State );
                state = value;
                OnStateChangeEvent?.Invoke( State );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;

        public PlayerCharacter? Character {
            get => character;
            internal set {
                CharacterInput.Disable();
                CameraInput.Disable();
                if (Character != null) {
                    Character.Input = null;
                }
                if (Camera != null) {
                    Camera.Input = null;
                    Camera.Target = null;
                }
                character = value;
                if (Character != null && Camera != null) {
                    Character.Input = new PlayableCharacterInput( CharacterInput, Character, Camera );
                    Camera.Input = new CameraInput( CameraInput );
                    Camera.Target = Character;
                }
            }
        }
        public Camera2? Camera {
            get => camera;
            internal set {
                CharacterInput.Disable();
                CameraInput.Disable();
                if (Character != null) {
                    Character.Input = null;
                }
                if (Camera != null) {
                    Camera.Input = null;
                    Camera.Target = null;
                }
                camera = value;
                if (Character != null && Camera != null) {
                    Character.Input = new PlayableCharacterInput( CharacterInput, Character, Camera );
                    Camera.Input = new CameraInput( CameraInput );
                    Camera.Target = Character;
                }
            }
        }

        public Player(IDependencyContainer container, PlayerInfo info) : base( container, info ) {
            CharacterInput = new InputActions_Character();
            CameraInput = new InputActions_Camera();
        }
        public override void Dispose() {
            CharacterInput.Dispose();
            CameraInput.Dispose();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            if (Character != null && Character.IsAlive && Camera != null && Cursor.lockState == CursorLockMode.Locked && Time.timeScale != 0f) {
                CharacterInput.Enable();
            } else {
                CharacterInput.Disable();
            }
            if (Character != null && Camera != null && Cursor.lockState == CursorLockMode.Locked && Time.timeScale != 0f) {
                CameraInput.Enable();
            } else {
                CameraInput.Disable();
            }
        }
        public void OnLateUpdate() {
        }

    }
    public enum PlayerState {
        Playing,
        Winner,
        Loser
    }
    internal class CameraInput : ICameraInput {

        private InputActions_Camera Input { get; }
        //public bool IsEnabled {
        //    get => Input.Camera.enabled;
        //    set {
        //        if (value) Input.Enable(); else Input.Disable();
        //    }
        //}

        public CameraInput(InputActions_Camera input) {
            Input = input;
        }

        public Vector2 GetLookDelta() {
            return Input.Camera.Look.ReadValue<Vector2>();
        }
        public float GetZoomDelta() {
            return Input.Camera.Zoom.ReadValue<Vector2>().y;
        }

    }
    internal class PlayableCharacterInput : IPlayableCharacterInput {

        private InputActions_Character Input { get; }
        //public bool IsEnabled {
        //    get => Input.Character.enabled;
        //    set {
        //        if (value) Input.Enable(); else Input.Disable();
        //    }
        //}
        private CharacterBase Character { get; }
        private Camera2 Camera { get; }
        private Camera2.RaycastHit? Hit => Camera.Hit;
        private Vector3 Target => Camera.Hit?.Point ?? Camera.transform.TransformPoint( Vector3.forward * 128f );

        public PlayableCharacterInput(InputActions_Character input, CharacterBase character, Camera2 camera) {
            Input = input;
            Character = character;
            Camera = camera;
        }

        public Vector3 GetMoveVector() {
            if (Input.Character.Move.IsPressed()) {
                var vector = Input.Character.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                vector = Camera.transform.TransformDirection( vector );
                vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public Vector3? GetBodyTarget() {
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Character.Move.IsPressed()) {
                var vector = Input.Character.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                if (vector != Vector3.zero) {
                    vector = Camera.transform.TransformDirection( vector );
                    vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                    return Character.transform.position + vector;
                }
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Character.Move.IsPressed()) {
                return Target;
            }
            return Target;
        }
        public Vector3? GetWeaponTarget() {
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Character.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public bool IsJumpPressed() {
            return Input.Character.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            return Input.Character.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            return Input.Character.Accelerate.IsPressed();
        }
        public bool IsFirePressed() {
            return Input.Character.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            return Input.Character.Aim.IsPressed();
        }
        public bool IsInteractPressed(out MonoBehaviour? interactable) {
            interactable = (MonoBehaviour?) Hit?.Enemy ?? Hit?.Thing;
            return Input.Character.Interact.WasPressedThisFrame();
        }

    }
}
