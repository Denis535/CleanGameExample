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
        public override void OnActivate(object? argument) {
            ShowSelf();
            Game.OnStateChangeEvent += OnGameStateChange;
            Game.OnPauseChangeEvent += OnGamePauseChange;
            //Cursor.lockState = GetCursorLockMode( this );
        }
        public override void OnDeactivate(object? argument) {
            //Cursor.lockState = GetCursorLockMode( this );
            Game.OnPauseChangeEvent -= OnGamePauseChange;
            Game.OnStateChangeEvent -= OnGameStateChange;
            HideSelf();
        }

        // OnDescendantActivate
        public override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            //Cursor.lockState = GetCursorLockMode( this );
        }
        public override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            //if (descendant is GameMenuWidget) {
            //    Game.IsPaused = false;
            //}
            //Cursor.lockState = GetCursorLockMode( this );
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
            if (isPause) {
                //AddChild( new GameMenuWidget( Container ) );
            } else {
                //RemoveChildren( i => i is GameMenuWidget );
            }
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            return view;
        }
        // Helpers
        private static CursorLockMode GetCursorLockMode(GameWidget widget) {
            if (widget.Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is WinWidget or LossWidget or GameMenuWidget )) {
                return CursorLockMode.None;
            } else {
                return CursorLockMode.Locked;
            }
        }

    }
}
