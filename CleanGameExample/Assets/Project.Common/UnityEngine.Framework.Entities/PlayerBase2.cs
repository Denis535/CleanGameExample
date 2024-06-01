#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase2 : PlayerBase {

        public PlayerState state;

        // Name
        public string Name { get; }
        // State
        public PlayerState State {
            get => state;
            protected set {
                var prev = state;
                state = GetState( value, prev );
                OnStateChangeEvent?.Invoke( state, prev );
            }
        }
        public event Action<PlayerState, PlayerState>? OnStateChangeEvent;

        // Constructor
        public PlayerBase2(string name) {
            Name = name;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void Update();
        public abstract void LateUpdate();

        // Helpers
        private static PlayerState GetState(PlayerState state, PlayerState prev) {
            switch (state) {
                case PlayerState.Win:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is PlayerState.None );
                    return state;
                case PlayerState.Lose:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is PlayerState.None );
                    return state;
                default:
                    throw Exceptions.Internal.NotSupported( $"Transition from {prev} to {state} is not supported" );
            }
        }

    }
    public enum PlayerState {
        None,
        Win,
        Lose
    }
}
