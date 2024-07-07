#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MenuWidget : UIWidgetBase2<MenuWidgetView> {

        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;

        public MenuWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

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
                widget.AddChild( new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.Quit() ).OnCancel( "No", null ) );
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
                widget.Router.LoadGameScene( new GameInfo( "Game", GameMode.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerCharacterType.Gray ) );
            };
            view.OnRed += evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameMode.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerCharacterType.Red ) );
            };
            view.OnGreen += evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameMode.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerCharacterType.Green ) );
            };
            view.OnBlue += evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameMode.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerCharacterType.Blue ) );
            };
            view.OnBack += evt => {
                widget.View.RemoveView( view );
            };
            return view;
        }

    }
}
