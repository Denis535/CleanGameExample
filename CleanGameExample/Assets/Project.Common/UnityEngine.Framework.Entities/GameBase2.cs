#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase2 : GameBase {

        // State
        public GameState State { get; private set; }
        public bool IsRunning => State is GameState.Running;
        public bool IsStopped => State is GameState.Stopped;
        // IsPaused
        public bool IsPaused { get; private set; }
        // OnStateEvent
        public event Action<GameState, GameState>? OnStateChangeEvent;
        public event Action? OnRunningEvent;
        public event Action? OnStoppedEvent;
        // OnPauseEvent
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

        // SetState
        protected void SetRunning() {
            Assert.Operation.Message( $"Transition from {State} to {GameState.Running} is invalid" ).Valid( State is GameState.None );
            var prev = State;
            State = GameState.Running;
            OnStateChangeEvent?.Invoke( State, prev );
            OnRunningEvent?.Invoke();
        }
        protected void SetStopped() {
            Assert.Operation.Message( $"Transition from {State} to {GameState.Stopped} is invalid" ).Valid( State is GameState.Running );
            var prev = State;
            State = GameState.Stopped;
            OnStateChangeEvent?.Invoke( State, prev );
            OnStoppedEvent?.Invoke();
        }

        // Pause
        public virtual void Pause() {
            Assert.Operation.Message( $"Game must be running" ).Valid( IsRunning );
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            OnPauseEvent?.Invoke();
        }
        public virtual void UnPause() {
            Assert.Operation.Message( $"Game must be running" ).Valid( IsRunning );
            Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
            IsPaused = false;
            OnUnPauseEvent?.Invoke();
        }

    }
    public enum GameState {
        None,
        Running,
        Stopped
    }
}
