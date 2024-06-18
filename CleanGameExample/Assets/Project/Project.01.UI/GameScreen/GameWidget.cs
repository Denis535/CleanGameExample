#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class GameWidget : UIWidgetBase2<GameWidgetView> {

        // Deps
        private Game Game { get; }
        // View
        public override GameWidgetView View { get; }

        // Constructor
        public GameWidget(IDependencyContainer container) : base( container ) {
            Game = container.RequireDependency<Game>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
            Game.OnStateChangeEvent += OnGameStateChange;
            View.IsInputEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        protected override void OnDeactivate(object? argument) {
            Cursor.lockState = CursorLockMode.None;
            View.IsInputEnabled = false;
            Game.OnStateChangeEvent -= OnGameStateChange;
            HideSelf();
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            Game.IsPaused = Children.Any( i => i is MenuWidget );
            View.IsInputEnabled = !Children.Any( i => i is MenuWidget );
            Cursor.lockState = Children.Any( i => i is MenuWidget or TotalsWidget ) ? CursorLockMode.None : CursorLockMode.Locked;
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            if (State is UIWidgetState.Active) {
                Game.IsPaused = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget );
                View.IsInputEnabled = !Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget );
                Cursor.lockState = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget or TotalsWidget ) ? CursorLockMode.None : CursorLockMode.Locked;
            }
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

        // Update
        public void Update() {
            View.TargetEffect = GetTargetEffect( Game.Player );
            if (View.IsCancelPressed) {
                AddChild( new MenuWidget( Container ) );
            }
        }
        public void LateUpdate() {
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
