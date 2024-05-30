#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class UIScreenBase2 : UIScreenBase {

        // Document
        protected UIDocument Document { get; private set; } = default!;

        // Awake
        public override void Awake() {
            Document = gameObject.RequireComponentInChildren<UIDocument>();
        }
        public override void OnDestroy() {
        }

        // Start
        public abstract void Start();
        public abstract void Update();
        public abstract void LateUpdate();

        // Helpers
        protected static bool IsMainScreen(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return true;
            }
            return false;
        }
        protected static bool IsGameScreen(UIRouterState state) {
            if (state is UIRouterState.GameSceneLoaded) {
                return true;
            }
            return false;
        }

    }
}
