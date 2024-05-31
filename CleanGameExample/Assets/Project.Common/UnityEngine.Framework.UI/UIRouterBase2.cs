#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class UIRouterBase2 : UIRouterBase {

        // Container
        protected IDependencyContainer Container { get; }
        // State
        public UIRouterState State { get; private set; }
        public bool IsMainSceneLoading => State == UIRouterState.MainSceneLoading;
        public bool IsMainSceneLoaded => State == UIRouterState.MainSceneLoaded;
        public bool IsGameSceneLoading => State == UIRouterState.GameSceneLoading;
        public bool IsGameSceneLoaded => State == UIRouterState.GameSceneLoaded;
        public bool IsQuitting => State == UIRouterState.Quitting;
        public bool IsQuited => State == UIRouterState.Quited;
        // OnStateEvent
        public event Action<UIRouterState, UIRouterState>? OnStateChangeEvent;
        public event Action? OnMainSceneLoadingEvent;
        public event Action? OnMainSceneLoadedEvent;
        public event Action? OnGameSceneLoadingEvent;
        public event Action? OnGameSceneLoadedEvent;
        public event Action? OnQuittingEvent;
        public event Action? OnQuitedEvent;

        // Constructor
        public UIRouterBase2(IDependencyContainer container) {
            Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetState
        protected void SetMainSceneLoading() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.MainSceneLoading} is invalid" ).Valid( State is UIRouterState.None or UIRouterState.GameSceneLoaded );
            var prev = State;
            State = UIRouterState.MainSceneLoading;
            OnStateChangeEvent?.Invoke( State, prev );
            OnMainSceneLoadingEvent?.Invoke();
        }
        protected void SetMainSceneLoaded() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.MainSceneLoaded} is invalid" ).Valid( State is UIRouterState.MainSceneLoading );
            var prev = State;
            State = UIRouterState.MainSceneLoaded;
            OnStateChangeEvent?.Invoke( State, prev );
            OnMainSceneLoadedEvent?.Invoke();
        }
        protected void SetGameSceneLoading() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.GameSceneLoading} is invalid" ).Valid( State is UIRouterState.None or UIRouterState.MainSceneLoaded );
            var prev = State;
            State = UIRouterState.GameSceneLoading;
            OnStateChangeEvent?.Invoke( State, prev );
            OnGameSceneLoadingEvent?.Invoke();
        }
        protected void SetGameSceneLoaded() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.GameSceneLoaded} is invalid" ).Valid( State is UIRouterState.GameSceneLoading );
            var prev = State;
            State = UIRouterState.GameSceneLoaded;
            OnStateChangeEvent?.Invoke( State, prev );
            OnGameSceneLoadedEvent?.Invoke();
        }
        protected void SetQuitting() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.Quitting} is invalid" ).Valid( State is UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoaded );
            var prev = State;
            State = UIRouterState.Quitting;
            OnStateChangeEvent?.Invoke( State, prev );
            OnQuittingEvent?.Invoke();
        }
        protected void SetQuited() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.Quited} is invalid" ).Valid( State is UIRouterState.Quitting );
            var prev = State;
            State = UIRouterState.Quited;
            OnStateChangeEvent?.Invoke( State, prev );
            OnQuitedEvent?.Invoke();
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
        // Quitting
        Quitting,
        Quited,
    }
}
