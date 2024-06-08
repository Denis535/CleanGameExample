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
        private InputActions_UI Actions { get; }

        // Constructor
        public GameWidget(IDependencyContainer container) {
            Container = container;
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
            Actions = new InputActions_UI();
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // OnActivate
        public override void OnActivate(object? argument) {
            ShowSelf();
            Game.OnStateChangeEvent += OnGameStateChange;
            Actions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public override void OnDeactivate(object? argument) {
            Cursor.lockState = CursorLockMode.None;
            Actions.Disable();
            Game.OnStateChangeEvent -= OnGameStateChange;
            HideSelf();
        }

        // OnDescendantActivate
        public override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            Game.IsPaused = Children.Any( i => i is GameMenuWidget );
            Cursor.lockState = Children.Any( i => i is WinWidget or LossWidget or GameMenuWidget ) ? CursorLockMode.None : CursorLockMode.Locked;
            Actions.SetEnabled( Cursor.lockState == CursorLockMode.Locked );
        }
        public override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            Cursor.lockState = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is WinWidget or LossWidget or GameMenuWidget ) ? CursorLockMode.None : CursorLockMode.Locked;
            Game.IsPaused = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is GameMenuWidget );
            Actions.SetEnabled( Cursor.lockState == CursorLockMode.Locked );
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
                if (!Children.OfType<GameMenuWidget>().Any()) AddChild( new GameMenuWidget( Container ) );
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
                        AddChild( new WinWidget( Container ) );
                    } else if (Game.Player.State == PlayerState.Looser) {
                        await Task.Delay( 2000 ).WaitAsync( DisposeCancellationToken );
                        AddChild( new LossWidget( Container ) );
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
