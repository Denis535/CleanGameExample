#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase2 : GameBase {

        private GameState state;

        // State
        public GameState State {
            get => state;
            protected set {
                var prev = state;
                state = GetState( value, prev );
                OnStateChangeEvent?.Invoke( state, prev );
            }
        }
        public event Action<GameState, GameState>? OnStateChangeEvent;
        // IsPaused
        public bool IsPaused { get; private set; }
        public event Action? OnPauseEvent;
        public event Action? OnUnPauseEvent;

        // Constructor
        public GameBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void Update();
        public abstract void LateUpdate();

        // Pause
        public virtual void Pause() {
            Assert.Operation.Message( $"Game must be running" ).Valid( State is GameState.Running );
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            OnPauseEvent?.Invoke();
        }
        public virtual void UnPause() {
            Assert.Operation.Message( $"Game must be running" ).Valid( State is GameState.Running );
            Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
            IsPaused = false;
            OnUnPauseEvent?.Invoke();
        }

        // Helpers
        private static GameState GetState(GameState state, GameState prev) {
            switch (state) {
                case GameState.Running:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is GameState.None );
                    return state;
                case GameState.Stopped:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is GameState.Running );
                    return state;
                default:
                    throw Exceptions.Internal.NotSupported( $"Transition from {prev} to {state} is not supported" );
            }
        }

    }
    public enum GameState {
        None,
        Running,
        Stopped
    }
}
