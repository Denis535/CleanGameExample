#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;

    public class GameWidget : UIWidgetBase<GameWidgetView> {

        // Globals
        private UIFactory Factory { get; }
        private Application2 Application { get; }
        // Actions
        private InputActions Actions { get; }

        // Constructor
        public GameWidget() {
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            Application = this.GetDependencyContainer().Resolve<Application2>( null );
            View = CreateView( this, Factory );
            Actions = new InputActions();
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            Actions.Enable();
        }
        public override void OnDetach(object? argument) {
            Actions.Disable();
        }

        // OnDescendantWidgetAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant) {
            base.OnBeforeDescendantAttach( descendant );
            if (descendant is GameMenuWidget) {
                Application.Pause();
                Actions.Disable();
            }
        }
        public override void OnAfterDescendantAttach(UIWidgetBase descendant) {
            base.OnAfterDescendantAttach( descendant );
        }
        public override void OnBeforeDescendantDetach(UIWidgetBase descendant) {
            base.OnBeforeDescendantDetach( descendant );
        }
        public override void OnAfterDescendantDetach(UIWidgetBase descendant) {
            if (IsAttached && descendant is GameMenuWidget) {
                Actions.Enable();
                Application.UnPause();
            }
            base.OnAfterDescendantDetach( descendant );
        }

        // Update
        public void Update() {
            if (Actions.UI.Cancel.WasPressedThisFrame()) {
                this.AttachChild( new GameMenuWidget() );
            }
            //if (Actions.Game.Move.WasPerformedThisFrame()) {
            //    Debug.Log( "Move: " + Actions.Game.Move.ReadValue<Vector2>() );
            //}
            //if (Actions.Game.Look.WasPerformedThisFrame()) {
            //    Debug.Log( "Look: " + Actions.Game.Move.ReadValue<Vector2>() );
            //}
            //if (Actions.Game.Fire.WasPerformedThisFrame()) {
            //    Debug.Log( "Fire" );
            //}
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget, UIFactory factory) {
            var view = new GameWidgetView( factory );
            return view;
        }

    }
}
