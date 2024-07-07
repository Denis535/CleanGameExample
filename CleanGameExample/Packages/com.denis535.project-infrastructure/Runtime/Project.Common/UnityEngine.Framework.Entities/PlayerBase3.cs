#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase3 : PlayerBase2, ICharacterInput, ICameraInput {

        private PlayerState state;

        public string Name { get; }
        public PlayerKind Kind { get; }
        public PlayerState State {
            get => state;
            set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;

        public PlayerBase3(IDependencyContainer container, string name, PlayerKind kind) : base( container ) {
            Name = name;
            Kind = kind;
        }
        public override void Dispose() {
            base.Dispose();
        }

        public abstract void OnFixedUpdate();
        public abstract void OnUpdate();

        public abstract Vector3 GetMoveVector();
        public abstract Vector3? GetBodyTarget();
        public abstract Vector3? GetHeadTarget();
        public abstract Vector3? GetWeaponTarget();
        public abstract bool IsJumpPressed();
        public abstract bool IsCrouchPressed();
        public abstract bool IsAcceleratePressed();
        public abstract bool IsFirePressed();
        public abstract bool IsAimPressed();
        public abstract bool IsInteractPressed(out MonoBehaviour? interactable);

        public abstract Vector2 GetLookDelta();
        public abstract float GetZoomDelta();

    }
    public enum PlayerKind {
        Gray,
        Red,
        Green,
        Blue
    }
    public enum PlayerState {
        Playing,
        Winner,
        Loser
    }
}
