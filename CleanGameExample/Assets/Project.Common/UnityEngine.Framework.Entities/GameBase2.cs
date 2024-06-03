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
                state = value;
                OnStateChangeEvent?.Invoke( state, prev );
            }
        }
        public event Action<GameState, GameState>? OnStateChangeEvent;
        // IsPaused
        public virtual bool IsPaused {
            get => isPaused;
            set {
                if (value) {
                    Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
                    isPaused = true;
                    OnPauseEvent?.Invoke( true );
                } else {
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

    }
    public enum GameState {
        Playing,
        Completed
    }
}
