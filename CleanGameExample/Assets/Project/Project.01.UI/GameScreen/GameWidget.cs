#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities;
    using Project.Entities.Actors;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidget : UIWidgetBase2<GameWidgetView> {

        private Game Game { get; }

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
            View.IsCursorVisible = false;
        }
        protected override void OnDeactivate(object? argument) {
            View.IsCursorVisible = true;
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            Game.IsPaused = Children.Any( i => i is MenuWidget );
            View.IsCursorVisible = Children.Any( i => i is MenuWidget or TotalsWidget );
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            if (State is UIWidgetState.Active) {
                Game.IsPaused = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget );
                View.IsCursorVisible = Children.Where( i => i.State is UIWidgetState.Active ).Any( i => i is MenuWidget or TotalsWidget );
            }
        }

        public void OnUpdate() {
            if (Game.Player.Camera != null) {
                View.Target.style.color = GetTargetColor( Game.Player.Camera );
            } else {
                View.Target.style.color = default;
            }
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            view.RegisterCallback<NavigationCancelEvent>( evt => {
                if (!widget.Children.Any( i => i is MenuWidget )) {
                    widget.AddChild( new MenuWidget( widget.Container ) );
                }
            } );
            return view;
        }
        // Helpers
        private static Color GetTargetColor(Camera2 camera) {
            if (camera.Hit?.Thing != null) return Color.yellow;
            if (camera.Hit?.Enemy != null) return Color.red;
            return Color.white;
        }

    }
}
