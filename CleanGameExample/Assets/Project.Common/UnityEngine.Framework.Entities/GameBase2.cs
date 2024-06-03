#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase2 : GameBase {

        private GameState state;
        private bool isPaused;

        // Container
        protected IDependencyContainer Container { get; }
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
        public virtual bool IsPaused {
            get => isPaused;
            set {
                if (value) {
                    Assert.Operation.Message( $"State {State} is invalid" ).Valid( State is GameState.Playing or GameState.PostPlaying );
                    Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
                    isPaused = true;
                    OnPauseEvent?.Invoke( true );
                } else {
                    Assert.Operation.Message( $"State {State} is invalid" ).Valid( State is GameState.Playing or GameState.PostPlaying );
                    Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
                    isPaused = false;
                    OnPauseEvent?.Invoke( false );
                }
            }
        }
        public event Action<bool>? OnPauseEvent;

        // Constructor
        public GameBase2(IDependencyContainer container) {
            Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void FixedUpdate();
        public abstract void Update();
        public abstract void LateUpdate();

        // Helpers
        private static GameState GetState(GameState state, GameState prev) {
            switch (state) {
                case GameState.PrePlaying:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is GameState.None );
                    return state;
                case GameState.Playing:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is GameState.PrePlaying );
                    return state;
                case GameState.PostPlaying:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is GameState.Playing );
                    return state;
                case GameState.Stopped:
                    Assert.Operation.Message( $"Transition from {prev} to {state} is invalid" ).Valid( prev is GameState.PrePlaying or GameState.Playing or GameState.PostPlaying );
                    return state;
                default:
                    throw Exceptions.Internal.NotSupported( $"Transition from {prev} to {state} is not supported" );
            }
        }

    }
    public enum GameState {
        None,
        PrePlaying,
        Playing,
        PostPlaying,
        Stopped
    }
}
