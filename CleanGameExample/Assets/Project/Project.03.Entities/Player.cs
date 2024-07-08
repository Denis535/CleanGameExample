#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Characters;
    using Project.Entities.Things;
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

        public (Vector3 Point, float Distance, GameObject Object)? Hit { get; private set; }
        public EnemyCharacter? Enemy {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 16f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    return @object.GetComponent<EnemyCharacter>();
                }
                return null;
            }
        }
        public Thing? Thing {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 2.5f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    return @object.GetComponent<Thing>();
                }
                return null;
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
            {
                Input.SetEnabled( Cursor.lockState == CursorLockMode.Locked && Time.timeScale != 0f && Character != null && Camera != null );
            }
            if (Character != null) {
                
            }
            if (Camera != null) {
                Hit = Raycast( Camera.Ray, Character?.transform );
            }
        }

        private static (Vector3 Point, float Distance, GameObject Object)? Raycast(Ray ray, Transform? ignore) {
            var mask = ~(Masks.Entity_Approximate | Masks.Trivial);
            var hit = Utils.RaycastAll( ray, 128, mask, QueryTriggerInteraction.Ignore ).Where( i => i.transform.root != ignore ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                return (hit.point, hit.distance, hit.collider.gameObject);
            } else {
                return null;
            }
        }

    }
    internal class PlayableCharacterInput : IPlayableCharacterInput {

        private Player Player { get; }
        private InputActions_Player.CharacterActions Input => Player.Input.Character;
        private Character Character => Player.Character!;
        private Vector3 Target => Player.Hit?.Point ?? Camera.main.transform.TransformPoint( Vector3.forward * 128f );
        private EnemyCharacter? Enemy => Player.Enemy;
        private Thing? Thing => Player.Thing;

        public PlayableCharacterInput(Player player) {
            Player = player;
        }

        public Vector3 GetMoveVector() {
            if (Input.Move.IsPressed()) {
                var vector = Input.Move.ReadValue<Vector2>().Chain( i => new Vector3( i.x, 0, i.y ) );
                vector = Camera.main.transform.TransformDirection( vector );
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
                    vector = Camera.main.transform.TransformDirection( vector );
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
            interactable = (MonoBehaviour?) Enemy ?? Thing;
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
