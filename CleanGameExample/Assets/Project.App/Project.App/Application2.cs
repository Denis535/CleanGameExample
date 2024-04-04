#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase {

        // Globals
        public Game? Game { get; private set; }

        // Awake
        public new void Awake() {
            base.Awake();
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // RunGame
        public void RunGame() {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Game = this.GetDependencyContainer().RequireDependency<Game>( null );
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game = null;
        }

    }
}
