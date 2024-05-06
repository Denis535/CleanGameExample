#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;

    public class GameWidget : UIWidgetBase<GameWidgetView> {

        // Application
        private Application2 Application { get; }
        // Entities
        private Game Game => Application.Game!;
        private Player Player => Application.Game!.Player;
        // View
        public override GameWidgetView View { get; }
        // Actions
        private InputActions Actions { get; }

        // Constructor
        public GameWidget() {
            Application = Utils.Container.RequireDependency<Application2>( null );
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
            Show();
        }
        public override void OnDetach(object? argument) {
            Hide();
            Actions.Disable();
            Cursor.lockState = CursorLockMode.None;
        }

        // OnDescendantWidgetAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant, object? argument) {
            base.OnBeforeDescendantAttach( descendant, argument );
            if (descendant is GameMenuWidget) {
                Game.SetPaused( true );
                Actions.Disable();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        public override void OnAfterDescendantAttach(UIWidgetBase descendant, object? argument) {
            base.OnAfterDescendantAttach( descendant, argument );
        }
        public override void OnBeforeDescendantDetach(UIWidgetBase descendant, object? argument) {
            base.OnBeforeDescendantDetach( descendant, argument );
        }
        public override void OnAfterDescendantDetach(UIWidgetBase descendant, object? argument) {
            if (IsAttached && descendant is GameMenuWidget) {
                Game.SetPaused( false );
                Actions.Enable();
                Cursor.lockState = CursorLockMode.Locked;
            }
            base.OnAfterDescendantDetach( descendant, argument );
        }

        // Update
        public void Update() {
            if (Player.Enemy) {
                View.Target.SetTargetEffect( TargetEffect.Enemy );
            } else if (Player.Loot) {
                View.Target.SetTargetEffect( TargetEffect.Loot );
            } else {
                View.Target.SetTargetEffect( TargetEffect.Normal );
            }
            if (Actions.UI.Cancel.WasPressedThisFrame()) {
                AttachChild( new GameMenuWidget() );
            }
        }
        public void LateUpdate() {
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            return view;
        }

    }
}
