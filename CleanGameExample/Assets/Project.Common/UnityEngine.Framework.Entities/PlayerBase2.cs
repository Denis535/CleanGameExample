#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase2 : PlayerBase {

        private PlayerState state;

        // Container
        protected IDependencyContainer Container { get; }
        // State
        public PlayerState State {
            get => state;
            protected set {
                var prev = state;
                state = value;
                OnStateChangeEvent?.Invoke( state, prev );
            }
        }
        public event Action<PlayerState, PlayerState>? OnStateChangeEvent;

        // Constructor
        public PlayerBase2(IDependencyContainer container) {
            Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public enum PlayerState {
        Playing,
        Winner,
        Looser
    }
}
