#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class GameMenuWidget : UIWidgetBase<GameMenuWidgetView> {

        // UI
        private UIRouter Router { get; }
        // View
        public override GameMenuWidgetView View { get; }

        // Constructor
        public GameMenuWidget(IDependencyContainer container) {
            Router = container.RequireDependency<UIRouter>();
            View = CreateView( this, container, Router );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            ShowSelf();
        }
        public override void OnDetach(object? argument) {
            HideSelf();
        }

        // Helpers
        private static GameMenuWidgetView CreateView(GameMenuWidget widget, IDependencyContainer container, UIRouter router) {
            var view = new GameMenuWidgetView();
            view.OnResume( evt => {
                widget.DetachSelf();
            } );
            view.OnSettings( evt => {
                widget.AttachChild( new SettingsWidget( container ) );
            } );
            view.OnBack( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.LoadMainSceneAsync().Throw() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            return view;
        }

    }
}
