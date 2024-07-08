#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class PlayerBase3 : PlayerBase2 {

        private PlayerState state;

        public PlayerInfo Info { get; }
        public PlayerState State {
            get => state;
            set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;

        public PlayerBase3(IDependencyContainer container, PlayerInfo info) : base( container ) {
            Info = info;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }

    public record PlayerInfo(string Name, PlayerCharacterType CharacterType);
    public enum PlayerCharacterType {
        Gray,
        Red,
        Green,
        Blue
    }

    public enum PlayerState {
        Playing,
        Winner,
        Loser
    }
}
