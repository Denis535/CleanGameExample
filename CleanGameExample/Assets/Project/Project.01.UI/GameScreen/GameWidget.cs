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

        // Constructor
        public GameWidget(IDependencyContainer container) {
            Container = container;
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
            Game.OnStateChangeEvent += OnGameStateChange;
            Game.OnPauseChangeEvent += OnGamePauseChange;
        }
        protected override void OnDeactivate(object? argument) {
            Game.OnPauseChangeEvent -= OnGamePauseChange;
            Game.OnStateChangeEvent -= OnGameStateChange;
            HideSelf();
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Update
        public void Update() {
            if (View.IsCancelPressed) {
                if (!Children.OfType<GameMenuWidget>().Any()) {
                    AddChild( new GameMenuWidget( Container ) );
                }
            }
            Game.IsPaused = IsPaused( this );
            Cursor.lockState = GetCursorLockMode( this );
            View.SetTargetEffect( GetTargetEffect( Player ) );
            View.Input.SetEnabled( !Game.IsPaused );
        }
        public void LateUpdate() {
        }

        // OnGameStateChange
        private async void OnGameStateChange(GameState state) {
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
        // OnGamePauseChange
        private void OnGamePauseChange(bool isPause) {
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            return view;
        }
        // Helpers
        private static bool IsPaused(GameWidget widget) {
            return widget.Children.OfType<GameMenuWidget>().Any();
        }
        private static CursorLockMode GetCursorLockMode(GameWidget widget) {
            if (widget.Children.Any( i => i is WinWidget or LossWidget or GameMenuWidget )) return CursorLockMode.None;
            return CursorLockMode.Locked;
        }
        private static TargetEffect GetTargetEffect(Player player) {
            if (player.Thing) return TargetEffect.Thing;
            if (player.Enemy) return TargetEffect.Enemy;
            return TargetEffect.Normal;
        }

    }
}
