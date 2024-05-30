#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase2 : GameBase {

        // IsPaused
        public bool IsPaused { get; private set; }
        // OnPauseEvent
        public event Action? OnPauseEvent;
        public event Action? OnUnPauseEvent;

        // Start
        public abstract void Start();
        public abstract void Update();
        public abstract void LateUpdate();

        // Pause
        public virtual void Pause() {
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            OnPauseEvent?.Invoke();
        }
        public virtual void UnPause() {
            Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
            IsPaused = false;
            OnUnPauseEvent?.Invoke();
        }

    }
}
