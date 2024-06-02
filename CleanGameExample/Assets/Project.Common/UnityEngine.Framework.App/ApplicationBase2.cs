#nullable enable
namespace UnityEngine.Framework.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class ApplicationBase2<TGame> : ApplicationBase where TGame : GameBase2 {

        // Container
        protected IDependencyContainer Container { get; }
        // Entities
        public TGame? Game { get; protected set; }
        public event Action<TGame>? OnGameCreate;
        public event Action<TGame>? OnGameDestroy;

        // Constructor
        public ApplicationBase2(IDependencyContainer container) {
            Container = container;
        }

        // CreateGame
        protected virtual void CreateGame(TGame game) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Game = game;
            OnGameCreate?.Invoke( Game );
        }
        protected virtual void DestroyGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            OnGameDestroy?.Invoke( Game );
            Game.Dispose();
            Game = null;
        }

    }
}
