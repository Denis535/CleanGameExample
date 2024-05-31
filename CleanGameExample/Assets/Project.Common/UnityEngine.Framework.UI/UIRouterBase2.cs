#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class UIRouterBase2 : UIRouterBase {

        // State
        public UIRouterState State { get; private set; }
        public bool IsMainSceneLoading => State == UIRouterState.MainSceneLoading;
        public bool IsMainSceneLoaded => State == UIRouterState.MainSceneLoaded;
        public bool IsGameSceneLoading => State == UIRouterState.GameSceneLoading;
        public bool IsGameSceneLoaded => State == UIRouterState.GameSceneLoaded;
        public bool IsQuitting => State == UIRouterState.Quitting;
        public bool IsQuited => State == UIRouterState.Quited;
        // OnStateEvent
        public event Action<UIRouterState>? OnStateChangeEvent;
        public event Action? OnMainSceneLoadingEvent;
        public event Action? OnMainSceneLoadedEvent;
        public event Action? OnGameSceneLoadingEvent;
        public event Action? OnGameSceneLoadedEvent;
        public event Action? OnQuittingEvent;
        public event Action? OnQuitedEvent;

        // Constructor
        public UIRouterBase2() {
            Application.wantsToQuit += OnQuit;
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
        protected void SetQuitting() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.Quitting} is invalid" ).Valid( State is UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoaded );
            State = UIRouterState.Quitting;
            OnStateChangeEvent?.Invoke( State );
            OnQuittingEvent?.Invoke();
        }
        protected void SetQuited() {
            Assert.Operation.Message( $"Transition from {State} to {UIRouterState.Quited} is invalid" ).Valid( State is UIRouterState.Quitting );
            State = UIRouterState.Quited;
            OnStateChangeEvent?.Invoke( State );
            OnQuitedEvent?.Invoke();
        }

        // Quit
        public virtual void Quit() {
            if (OnQuit()) {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            }
        }
        protected virtual bool OnQuit() {
            return true;
        }

    }
    public enum UIRouterState {
        None,
        // MainScene
        MainSceneLoading,
        MainSceneLoaded,
        // GameScene
        GameSceneLoading,
        GameSceneLoaded,
        // Quit
        Quitting,
        Quited,
    }
}
