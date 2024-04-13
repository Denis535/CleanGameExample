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

        // Deps
        private Application2 Application { get; }
        // Actions
        private InputActions Actions { get; }

        // Constructor
        public GameWidget() {
            Application = this.GetDependencyContainer().RequireDependency<Application2>( null );
            View = CreateView( this );
            Actions = new InputActions();
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            Actions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public override void OnDetach(object? argument) {
            Actions.Disable();
            Cursor.lockState = CursorLockMode.None;
        }

        // OnDescendantWidgetAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant) {
            base.OnBeforeDescendantAttach( descendant );
            if (descendant is GameMenuWidget) {
                Application.Game!.IsPlaying = false;
                Actions.Disable();
                Cursor.lockState = CursorLockMode.None;
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
                Application.Game!.IsPlaying = true;
                Actions.Enable();
                Cursor.lockState = CursorLockMode.Locked;
            }
            base.OnAfterDescendantDetach( descendant );
        }

        // Update
        public void Update() {
            if (Actions.UI.Cancel.WasPressedThisFrame()) {
                this.AttachChild( new GameMenuWidget() );
            }
            if (Application.Game != null && Application.Game.Player.Character != null) {
                Application.Game.Camera.Target = Application.Game.Player.Character.transform;
                if (Application.Game.IsPlaying) {
                    Application.Game.Camera.RotationDeltaInput = Actions.Game.Look.ReadValue<Vector2>();
                    Application.Game.Camera.DistanceDeltaInput = Actions.Game.ScrollWheel.ReadValue<Vector2>().y;
                }
            }
        }
        public void LateUpdate() {
        }
        public void FixedUpdate() {
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            return view;
        }

    }
}
