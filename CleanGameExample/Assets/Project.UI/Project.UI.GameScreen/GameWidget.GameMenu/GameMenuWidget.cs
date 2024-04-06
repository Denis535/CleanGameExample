#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class GameMenuWidget : UIWidgetBase<GameMenuWidgetView> {

        // Deps
        private UIRouter Router { get; }

        // Constructor
        public GameMenuWidget() {
            Router = this.GetDependencyContainer().RequireDependency<UIRouter>( null );
            View = CreateView( this, Router );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
        }

        // Helpers
        private static GameMenuWidgetView CreateView(GameMenuWidget widget, UIRouter router) {
            var view = new GameMenuWidgetView();
            view.Resume.OnClick( evt => {
                widget.DetachSelf();
            } );
            view.Settings.OnClick( evt => {
                widget.AttachChild( new SettingsWidget() );
            } );
            view.Back.OnClick( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.LoadMainSceneAsync().Throw() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            return view;
        }

    }
}
