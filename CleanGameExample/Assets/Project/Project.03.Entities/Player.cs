#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Project.Entities.Characters;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public abstract class PlayerBase3 : PlayerBase2, IPlayer {

        private PlayerState state;

        // Name
        public abstract string Name { get; }
        public abstract PlayerKind Kind { get; }
        // State
        public virtual PlayerState State {
            get => state;
            internal set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;
        // Framework
        public abstract Camera2 Camera { get; }
        public abstract PlayerCharacter? Character { get; internal set; }

        // Constructor
        public PlayerBase3(IDependencyContainer container) : base( container ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void Update();

        // Input
        public abstract bool IsMovePressed(out Vector3 moveVector);
        public abstract Vector3? GetLookTarget();
        public abstract Vector3? GetHeadTarget();
        public abstract Vector3? GetAimTarget();
        public abstract bool IsJumpPressed();
        public abstract bool IsCrouchPressed();
        public abstract bool IsAcceleratePressed();
        public abstract bool IsFirePressed();
        public abstract bool IsAimPressed();
        public abstract bool IsInteractPressed(out MonoBehaviour? interactable);

    }
    public class Player : PlayerBase3 {

        // Name
        public override string Name { get; }
        public override PlayerKind Kind { get; }
        // State
        public override PlayerState State { get => base.State; internal set => base.State = value; }
        // Framework
        public override Camera2 Camera { get; }
        public override PlayerCharacter? Character { get; internal set; }
        // Input
        private InputActions_Player Input { get; }
        // Hit
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
        public IThing? Thing {
            get {
                if (Hit != null && Vector3.Distance( Character!.transform.position, Hit.Value.Point ) <= 2.5f) {
                    var @object = Hit.Value.Object.transform.root.gameObject;
                    return @object.GetComponent<IThing>();
                }
                return null;
            }
        }

        // Constructor
        public Player(IDependencyContainer container, string name, PlayerKind kind, Camera2 camera) : base( container ) {
            Name = name;
            Kind = kind;
            Camera = camera;
            Input = new InputActions_Player();
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

        // Update
        public override void Update() {
            {
                Input.SetEnabled( Character != null && Time.timeScale != 0f && Cursor.lockState == CursorLockMode.Locked );
            }
            if (Camera != null && Character != null) {
                Camera.Rotate( Input.Player.Look.ReadValue<Vector2>() );
                Camera.Zoom( Input.Player.Zoom.ReadValue<Vector2>().y );
                Camera.Apply( Character );
            }
            if (Camera != null) {
                Hit = Raycast( Camera.transform, Character?.transform );
            }
        }

        // Input
        public override bool IsMovePressed(out Vector3 moveVector) {
            Assert.Operation.Message( $"Method 'IsMovePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            if (Input.Player.Move.IsPressed()) {
                var vector = (Vector3) Input.Player.Move.ReadValue<Vector2>();
                moveVector = UnityEngine.Camera.main.transform.TransformDirection( vector.x, 0, vector.y ).Chain( i => new Vector3( i.x, 0, i.z ).normalized * vector.magnitude );
                return true;
            } else {
                moveVector = default;
                return false;
            }
        }
        public override Vector3? GetLookTarget() {
            if (IsAimPressed() || IsFirePressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            if (IsMovePressed( out var moveVector )) {
                return Character!.transform.position + moveVector;
            }
            return null;
        }
        public override Vector3? GetHeadTarget() {
            if (IsAimPressed() || IsFirePressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            if (IsMovePressed( out _ )) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
        }
        public override Vector3? GetAimTarget() {
            if (IsAimPressed() || IsFirePressed()) {
                return Hit?.Point ?? UnityEngine.Camera.main.transform.TransformPoint( Vector3.forward * 128f );
            }
            if (IsMovePressed( out _ )) {
                return null;
            }
            return null;
        }
        public override bool IsJumpPressed() {
            Assert.Operation.Message( $"Method 'IsJumpPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Player.Jump.IsPressed();
        }
        public override bool IsCrouchPressed() {
            Assert.Operation.Message( $"Method 'IsCrouchPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Player.Crouch.IsPressed();
        }
        public override bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Method 'IsAcceleratePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Player.Accelerate.IsPressed();
        }
        public override bool IsFirePressed() {
            Assert.Operation.Message( $"Method 'IsFirePressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Player.Fire.IsPressed();
        }
        public override bool IsAimPressed() {
            Assert.Operation.Message( $"Method 'IsAimPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            return Input.Player.Aim.IsPressed();
        }
        public override bool IsInteractPressed(out MonoBehaviour? interactable) {
            Assert.Operation.Message( $"Method 'IsInteractPressed' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            interactable = (MonoBehaviour?) Enemy ?? (MonoBehaviour?) Thing;
            return Input.Player.Interact.WasPressedThisFrame();
        }

        // Heleprs
        private static (Vector3 Point, float Distance, GameObject Object)? Raycast(Transform ray, Transform? ignore) {
            var hit = Utils.RaycastAll( ray.position, ray.forward, 128 ).Where( i => i.transform.root != ignore ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.transform) {
                return (hit.point, hit.distance, hit.collider.gameObject);
            } else {
                return null;
            }
        }

    }
    // PlayerKind
    public enum PlayerKind {
        Gray,
        Red,
        Green,
        Blue
    }
    // PlayerState
    public enum PlayerState {
        Playing,
        Winner,
        Loser
    }
}
