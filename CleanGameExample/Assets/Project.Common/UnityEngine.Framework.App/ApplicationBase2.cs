#nullable enable
namespace UnityEngine.Framework.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class ApplicationBase2 : ApplicationBase {

        private GameBase2? game;

        // Game
        public GameBase2? Game {
            get => game;
            protected set {
                if (value != null) {
                    Assert.Operation.Message( $"Game must be null" ).Valid( game == null );
                    game = value;
                    OnGameCreated?.Invoke( game );
                } else {
                    Assert.Operation.Message( $"Game must be non-null" ).Valid( game != null );
                    OnGameDestroyed?.Invoke( game );
                    game = null;
                }
            }
        }
        // OnGameEvent
        public event Action<GameBase2>? OnGameCreated;
        public event Action<GameBase2>? OnGameDestroyed;

        // Constructor
        public ApplicationBase2() {
        }

    }
}
