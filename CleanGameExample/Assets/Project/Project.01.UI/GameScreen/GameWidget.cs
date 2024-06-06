#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.Entities;
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
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            ShowSelf();
            Game.OnStateChangeEvent += OnGameStateChange;
            Actions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public override void OnDetach(object? argument) {
            Cursor.lockState = CursorLockMode.None;
            Actions.Disable();
            Game.OnStateChangeEvent -= OnGameStateChange;
            HideSelf();
        }

        // OnDescendantWidgetAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant, object? argument) {
            base.OnBeforeDescendantAttach( descendant, argument );
            if (descendant is WinWidget or LossWidget or GameMenuWidget) {
                Game.IsPaused = true;
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
            if (IsAttached && !Children.Where( i => i.IsAttached ).Any( i => i is WinWidget or LossWidget or GameMenuWidget )) {
                Cursor.lockState = CursorLockMode.Locked;
                Actions.Enable();
                Game.IsPaused = false;
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

        // OnGameStateChange
        private async void OnGameStateChange(GameState state, GameState prev) {
            try {
                if (state is GameState.Completed) {
                    if (Game.Player.State == PlayerState.Winner) {
                        await Task.Delay( 2000 ).WaitAsync( DisposeCancellationToken );
                        AttachChild( new WinWidget( Container ) );
                    } else if (Game.Player.State == PlayerState.Looser) {
                        await Task.Delay( 2000 ).WaitAsync( DisposeCancellationToken );
                        AttachChild( new LossWidget( Container ) );
                    }
                }
            } catch (OperationCanceledException) {
            }
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            return view;
        }

    }
}
