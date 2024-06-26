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

        // Framework
        private Game Game { get; }
        // IsCursorLocked
        public bool IsCursorVisible {
            get => Cursor.lockState == CursorLockMode.None;
            set => Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        // Constructor
        public GameWidget(IDependencyContainer container) : base( container ) {
            Game = container.RequireDependency<Game>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        // OnActivate
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

        // OnDescendantActivate
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

        // Update
        public void Update() {
            View.TargetEffect = GetTargetEffect( Game.Player );
        }
        public void LateUpdate() {
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
            if (player.Thing) return TargetEffect.Thing;
            if (player.Enemy) return TargetEffect.Enemy;
            return TargetEffect.Normal;
        }

    }
}
