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
    using UnityEngine.Framework.UI;

    public class GameWidget : UIWidgetBase2<GameWidgetView> {

        // Deps
        private Application2 Application { get; }
        private Game Game => Application.Game ?? throw Exceptions.Internal.NullReference( $"Reference 'Game' is null" );
        private Player Player => Game.Player;
        // View
        public override GameWidgetView View { get; }

        // Constructor
        public GameWidget(IDependencyContainer container) : base( container ) {
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
            View.IsInputEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        protected override void OnDeactivate(object? argument) {
            Cursor.lockState = CursorLockMode.None;
            View.IsInputEnabled = false;
            Game.OnPauseChangeEvent -= OnGamePauseChange;
            Game.OnStateChangeEvent -= OnGameStateChange;
            HideSelf();
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            if (descendant is MenuWidget) {
                Game.IsPaused = true;
                View.IsInputEnabled = false;
            }
            if (descendant is TotalsWidget or MenuWidget) {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            if (!Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget )) {
                Game.IsPaused = false;
                View.IsInputEnabled = true;
            }
            if (!Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget or TotalsWidget )) {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // Update
        public void Update() {
            View.TargetEffect = GetTargetEffect( Player );
            if (View.IsCancelPressed) {
                AddChild( new MenuWidget( Container ) );
            }
        }
        public void LateUpdate() {
        }

        // OnGameStateChange
        private async void OnGameStateChange(GameState state) {
            try {
                if (state is GameState.Completed) {
                    await Task.Delay( 2000 ).WaitAsync( DisposeCancellationToken );
                    AddChild( new TotalsWidget( Container ) );
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
        private static TargetEffect GetTargetEffect(Player player) {
            if (player.Thing) return TargetEffect.Thing;
            if (player.Enemy) return TargetEffect.Enemy;
            return TargetEffect.Normal;
        }

    }
}
