#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {

        // System
        private bool IsInitialized { get; set; }
        // Globals
        public Camera2 Camera { get; private set; } = default!;

        // Awake
        public void Awake() {
            Camera = this.GetDependencyContainer().Resolve<Camera2>( null );
        }
        public void OnDestroy() {
        }

        // Initialize
        public void Initialize() {
            Assert.Object.Message( $"Player {this} must be awakened" ).Initialized( didAwake );
            Assert.Operation.Message( $"Player {this} must not be initialized" ).Valid( !IsInitialized );
            IsInitialized = true;
        }
        public void Deinitialize() {
            Assert.Object.Message( $"Player {this} must be alive" ).Alive( this );
            Assert.Operation.Message( $"Player {this} must be initialized" ).Valid( IsInitialized );
        }

        // Start
        public void Start() {
            Assert.Operation.Message( $"Player {this} must be initialized" ).Valid( IsInitialized );
        }
        public void Update() {
            Assert.Operation.Message( $"Player {this} must be initialized" ).Valid( IsInitialized );
        }

    }
}
