#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using Project.Entities.Characters;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MenuWidget : UIWidgetBase2<MenuWidgetView> {

        // Deps
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;
        // View
        public override MenuWidgetView View { get; }

        // Constructor
        public MenuWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override async void OnActivate(object? argument) {
            ShowSelf();
            try {
                View.SetDisplayed( false );
                while (!Application.AuthenticationService.IsSignedIn) {
                    await Task.Yield();
                    DisposeCancellationToken.ThrowIfCancellationRequested();
                }
                View.SetDisplayed( true );
            } catch (OperationCanceledException) {
            }
        }
        protected override void OnDeactivate(object? argument) {
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

        // Helpers
        private static MenuWidgetView CreateView(MenuWidget widget) {
            var view = new MenuWidgetView();
            view.AddView( CreateView_Menu( widget ) );
            return view;
        }
        private static MenuWidgetView_Menu CreateView_Menu(MenuWidget widget) {
            var view = new MenuWidgetView_Menu();
            view.OnStartGame += evt => {
                widget.View.AddView( CreateView_StartGame( widget ) );
            };
            view.OnSettings += evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            };
            view.OnQuit += evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.Quit() ).OnCancel( "No", null );
                widget.AddChild( dialog );
            };
            return view;
        }
        private static MenuWidgetView_StartGame CreateView_StartGame(MenuWidget widget) {
            var view = new MenuWidgetView_StartGame();
            view.OnNewGame += evt => {
                widget.View.AddView( CreateView_SelectLevel( widget ) );
            };
            view.OnContinue += evt => {
                widget.View.AddView( CreateView_SelectLevel( widget ) );
            };
            view.OnBack += evt => {
                widget.View.RemoveView( view );
            };
            return view;
        }
        private static MenuWidgetView_SelectLevel CreateView_SelectLevel(MenuWidget widget) {
            var view = new MenuWidgetView_SelectLevel();
            view.OnLevel1 += evt => {
                widget.View.AddView( CreateView_SelectCharacter( widget, GameLevel.Level1 ) );
            };
            view.OnLevel2 += evt => {
                widget.View.AddView( CreateView_SelectCharacter( widget, GameLevel.Level2 ) );
            };
            view.OnLevel3 += evt => {
                widget.View.AddView( CreateView_SelectCharacter( widget, GameLevel.Level3 ) );
            };
            view.OnBack += evt => {
                widget.View.RemoveView( view );
            };
            return view;
        }
        private static MenuWidgetView_SelectCharacter CreateView_SelectCharacter(MenuWidget widget, GameLevel level) {
            var view = new MenuWidgetView_SelectCharacter();
            view.OnGray += evt => {
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Gray );
            };
            view.OnRed += evt => {
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Red );
            };
            view.OnGreen += evt => {
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Green );
            };
            view.OnBlue += evt => {
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Blue );
            };
            view.OnBack += evt => {
                widget.View.RemoveView( view );
            };
            return view;
        }

    }
}
