#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class UIScreenBase2 : UIScreenBase {

        // Document
        protected UIDocument Document { get; }
        // AudioSource
        protected AudioSource AudioSource { get; }

        // Constructor
        public UIScreenBase2(UIDocument document, AudioSource audioSource) {
            Document = document;
            AudioSource = audioSource;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void Update();
        public abstract void LateUpdate();

        // Helpers
        protected static bool IsMainScreen(UIRouterBase2 router) {
            if (router.State is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return true;
            }
            return false;
        }
        protected static bool IsGameScreen(UIRouterBase2 router) {
            if (router.State is UIRouterState.GameSceneLoaded) {
                return true;
            }
            return false;
        }

    }
}
