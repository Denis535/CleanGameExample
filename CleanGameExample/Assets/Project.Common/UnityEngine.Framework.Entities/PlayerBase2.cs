#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase2 : PlayerBase {

        // Name
        public string Name { get; }
        // State
        public PlayerState State { get; private set; }
        public bool IsWin => State is PlayerState.Win;
        public bool IsLose => State is PlayerState.Lose;
        // OnStateEvent
        public event Action<PlayerState>? OnStateChangeEvent;
        public event Action? OnWinEvent;
        public event Action? OnLoseEvent;

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

        // SetState
        protected void SetWin() {
            Assert.Operation.Message( $"Transition from {State} to {PlayerState.Win} is invalid" ).Valid( State is PlayerState.None );
            State = PlayerState.Win;
            OnStateChangeEvent?.Invoke( State );
            OnWinEvent?.Invoke();
        }
        protected void SetLose() {
            Assert.Operation.Message( $"Transition from {State} to {PlayerState.Lose} is invalid" ).Valid( State is PlayerState.None );
            State = PlayerState.Lose;
            OnStateChangeEvent?.Invoke( State );
            OnLoseEvent?.Invoke();
        }

    }
    public enum PlayerState {
        None,
        Win,
        Lose
    }
}
