#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class GameBase3 : GameBase2 {

        private GameState state;
        private bool isPaused;

        public GameInfo Info { get; }
        public GameState State {
            get => state;
            protected set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<GameState>? OnStateChangeEvent;
        public bool IsPaused {
            get => isPaused;
            set {
                if (value != isPaused) {
                    isPaused = value;
                    Time.timeScale = isPaused ? 0f : 1f;
                    OnPauseEvent?.Invoke( isPaused );
                }
            }
        }
        public event Action<bool>? OnPauseEvent;
        protected bool IsDirty { get; set; }

        public GameBase3(IDependencyContainer container, GameInfo info) : base( container ) {
            Info = info;
        }
        public override void Dispose() {
            Time.timeScale = 1f;
            base.Dispose();
        }

        public abstract void OnFixedUpdate();
        public abstract void OnUpdate();

    }

    public record GameInfo(string Name, GameMode Mode, GameLevel Level);
    public enum GameMode {
        None
    }
    public enum GameLevel {
        Level1,
        Level2,
        Level3
    }
    public static class GameLevelExtensions {
        public static bool IsLast(this GameLevel level) {
            return level == GameLevel.Level3;
        }
        public static GameLevel GetNext(this GameLevel level) {
            Assert.Operation.Message( $"Level {level} must be non-last" ).Valid( level != GameLevel.Level3 );
            return level + 1;
        }
    }

    public enum GameState {
        Playing,
        Completed
    }
}
