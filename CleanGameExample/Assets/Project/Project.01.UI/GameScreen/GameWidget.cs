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

        // Container
        private IDependencyContainer Container { get; }
        // App
        private Application2 Application { get; }
        // Entities
        private Game Game => Application.Game!;
        private Player Player => Application.Game!.Player;
        // View
        public override GameWidgetView View { get; }
        // Actions
        private InputActions Actions { get; }

        // Constructor
        public GameWidget(IDependencyContainer container) {
            Container = container;
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
            Actions = new InputActions();
            Game.OnStateChangeEvent += (state, prev) => {
                //Debug.Log( state );
                //Debug.Log( Player.State );
            };
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            ShowSelf();
            Actions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public override void OnDetach(object? argument) {
            Cursor.lockState = CursorLockMode.None;
            Actions.Disable();
            HideSelf();
        }

        // OnDescendantWidgetAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant, object? argument) {
            base.OnBeforeDescendantAttach( descendant, argument );
            if (descendant is GameMenuWidget) {
                Cursor.lockState = CursorLockMode.None;
                Actions.Disable();
                Game.IsPaused = true;
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
                Game.IsPaused = false;
                Actions.Enable();
                Cursor.lockState = CursorLockMode.Locked;
            }
            base.OnAfterDescendantDetach( descendant, argument );
        }

        // Update
        public void Update() {
            if (Player?.Thing) {
                View.SetEffect( TargetEffect.Thing );
            } else if (Player?.Enemy) {
                View.SetEffect( TargetEffect.Enemy );
            } else {
                View.SetEffect( TargetEffect.Normal );
            }
            if (Actions.UI.Cancel.WasPressedThisFrame()) {
                AttachChild( new GameMenuWidget( Container ) );
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
