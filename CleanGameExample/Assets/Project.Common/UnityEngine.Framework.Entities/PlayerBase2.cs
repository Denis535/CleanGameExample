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
        public event Action? OnWinEvent;
        public event Action? OnLoseEvent;

        // Constructor
        public PlayerBase2(string name) {
            Name = name;
        }

        // Start
        public abstract void Start();
        public abstract void Update();
        public abstract void LateUpdate();

        // OnState
        protected void OnWin() {
            Assert.Operation.Message( $"Transition from {State} to {PlayerState.Win} is invalid" ).Valid( State is PlayerState.None );
            State = PlayerState.Win;
            OnWinEvent?.Invoke();
        }
        protected void OnLose() {
            Assert.Operation.Message( $"Transition from {State} to {PlayerState.Lose} is invalid" ).Valid( State is PlayerState.None );
            State = PlayerState.Lose;
            OnLoseEvent?.Invoke();
        }

    }
    public enum PlayerState {
        None,
        Win,
        Lose
    }
}
