#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class GameWidget : UIWidgetBase2<GameWidgetView> {

        private Game Game { get; }
        private bool IsCursorVisible {
            get => Cursor.lockState == CursorLockMode.None;
            set => Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public GameWidget(IDependencyContainer container) : base( container ) {
            Game = container.RequireDependency<Game>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            Game.OnStateChangeEvent += async state => {
                try {
                    if (state is GameState.Completed) {
                        await Awaitable.WaitForSecondsAsync( 2, DisposeCancellationToken );
                        AddChild( new TotalsWidget( Container ) );
                    }
                } catch (OperationCanceledException) {
                }
            };
            ShowSelf();
            IsCursorVisible = false;
        }
        protected override void OnDeactivate(object? argument) {
            IsCursorVisible = true;
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            Game.IsPaused = Children.Any( i => i is MenuWidget );
            IsCursorVisible = Children.Any( i => i is MenuWidget or TotalsWidget );
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            if (State is UIWidgetState.Active) {
                Game.IsPaused = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget );
                IsCursorVisible = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget or TotalsWidget );
            }
        }

        public void OnUpdate() {
            View.TargetEffect = GetTargetEffect( Game.Player );
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            view.OnCancel += evt => {
                if (!widget.Children.Any( i => i is MenuWidget )) {
                    widget.AddChild( new MenuWidget( widget.Container ) );
                }
            };
            return view;
        }
        // Helpers
        private static TargetEffect GetTargetEffect(Player player) {
            if (player.Thing != null) return TargetEffect.Thing;
            if (player.Enemy != null) return TargetEffect.Enemy;
            return TargetEffect.Normal;
        }

    }
}
