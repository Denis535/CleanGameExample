#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase2 : GameBase {

        // State
        public GameState State { get; private set; }
        public bool IRunning => State is GameState.Running;
        public bool IsStopped => State is GameState.Stopped;
        // IsPaused
        public bool IsPaused { get; private set; }
        // OnStateEvent
        public event Action? OnRunningEvent;
        public event Action? OnStoppedEvent;
        // OnPauseEvent
        public event Action? OnPauseEvent;
        public event Action? OnUnPauseEvent;

        // Start
        public abstract void Start();
        public abstract void Update();
        public abstract void LateUpdate();

        // SetState
        protected void SetRunning() {
            Assert.Operation.Message( $"Transition from {State} to {GameState.Running} is invalid" ).Valid( State is GameState.None );
            State = GameState.Running;
            OnRunningEvent?.Invoke();
        }
        protected void SetStopped() {
            Assert.Operation.Message( $"Transition from {State} to {GameState.Stopped} is invalid" ).Valid( State is GameState.Running );
            State = GameState.Stopped;
            OnStoppedEvent?.Invoke();
        }

        // Pause
        public virtual void Pause() {
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            OnPauseEvent?.Invoke();
        }
        public virtual void UnPause() {
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
