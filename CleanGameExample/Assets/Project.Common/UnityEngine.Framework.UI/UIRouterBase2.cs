#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    public abstract class UIRouterBase2 : UIRouterBase {

        // State
        public UIRouterState State { get; private set; }
        public bool IsMainSceneLoading => State == UIRouterState.MainSceneLoading;
        public bool IsMainSceneLoaded => State == UIRouterState.MainSceneLoaded;
        public bool IsGameSceneLoading => State == UIRouterState.GameSceneLoading;
        public bool IsGameSceneLoaded => State == UIRouterState.GameSceneLoaded;
        public bool IsUnloading => State == UIRouterState.Unloading;
        public bool IsUnloaded => State == UIRouterState.Unloaded;
        // OnStateEvent
        public event Action<UIRouterState>? OnStateChangeEvent;
        public event Action? OnMainSceneLoadingEvent;
        public event Action? OnMainSceneLoadedEvent;
        public event Action? OnGameSceneLoadingEvent;
        public event Action? OnGameSceneLoadedEvent;
        public event Action? OnUnloadingEvent;
        public event Action? OnUnloadedEvent;
        // OnQuitEvent
        public event Action? OnQuitEvent;

        // Constructor
        public UIRouterBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetState
        protected void SetMainSceneLoading() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.MainSceneLoading} is invalid" ).Valid( State is UIRouterState.None or UIRouterState.GameSceneLoaded );
            State = UIRouterState.MainSceneLoading;
            OnStateChangeEvent?.Invoke( State );
            OnMainSceneLoadingEvent?.Invoke();
        }
        protected void SetMainSceneLoaded() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.MainSceneLoaded} is invalid" ).Valid( State is UIRouterState.MainSceneLoading );
            State = UIRouterState.MainSceneLoaded;
            OnStateChangeEvent?.Invoke( State );
            OnMainSceneLoadedEvent?.Invoke();
        }
        protected void SetGameSceneLoading() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.GameSceneLoading} is invalid" ).Valid( State is UIRouterState.None or UIRouterState.MainSceneLoaded );
            State = UIRouterState.GameSceneLoading;
            OnStateChangeEvent?.Invoke( State );
            OnGameSceneLoadingEvent?.Invoke();
        }
        protected void SetGameSceneLoaded() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.GameSceneLoaded} is invalid" ).Valid( State is UIRouterState.GameSceneLoading );
            State = UIRouterState.GameSceneLoaded;
            OnStateChangeEvent?.Invoke( State );
            OnGameSceneLoadedEvent?.Invoke();
        }
        protected void SetUnloading() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.Unloading} is invalid" ).Valid( State is UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoaded );
            State = UIRouterState.Unloading;
            OnStateChangeEvent?.Invoke( State );
            OnUnloadingEvent?.Invoke();
        }
        protected void SetUnloaded() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.Unloaded} is invalid" ).Valid( State is UIRouterState.Unloading );
            State = UIRouterState.Unloaded;
            OnStateChangeEvent?.Invoke( State );
            OnUnloadedEvent?.Invoke();
        }

        // Quit
        public virtual void Quit() {
            OnQuitEvent?.Invoke();
        }

    }
    public enum UIRouterState {
        None,
        // MainSceneLoading
        MainSceneLoading,
        MainSceneLoaded,
        // GameSceneLoading
        GameSceneLoading,
        GameSceneLoaded,
        // Unloading
        Unloading,
        Unloaded,
    }
}
