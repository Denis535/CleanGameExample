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

    public class MenuMainWidget : UIWidgetBase<MenuMainWidgetView> {

        // Container
        private IDependencyContainer Container { get; }
        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;
        // View
        public override MenuMainWidgetView View { get; }

        // Constructor
        public MenuMainWidget(IDependencyContainer container) {
            Container = container;
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
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
        private static MenuMainWidgetView CreateView(MenuMainWidget widget) {
            var view = new MenuMainWidgetView();
            view.AddView( CreateView_MainMenuView( widget ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuView CreateView_MainMenuView(MenuMainWidget widget) {
            var view = new MainMenuWidgetView_MainMenuView();
            view.OnStartGame( evt => {
                widget.View.AddView( CreateView_StartGameView( widget ) );
            } );
            view.OnSettings( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.OnQuit( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.Quit() ).OnCancel( "No", null );
                widget.AddChild( dialog );
            } );
            return view;
        }
        private static MainMenuWidgetView_StartGameView CreateView_StartGameView(MenuMainWidget widget) {
            var view = new MainMenuWidgetView_StartGameView();
            view.OnNewGame( evt => {
                widget.View.AddView( CreateView_SelectLevelView( widget ) );
            } );
            view.OnContinue( evt => {
                widget.View.AddView( CreateView_SelectLevelView( widget ) );
            } );
            view.OnBack( evt => {
                widget.View.RemoveView( view );
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelView CreateView_SelectLevelView(MenuMainWidget widget) {
            var view = new MainMenuWidgetView_SelectLevelView();
            view.OnLevel1( evt => {
                widget.View.AddView( CreateView_SelectCharacterView( widget, Level.Level1 ) );
            } );
            view.OnLevel2( evt => {
                widget.View.AddView( CreateView_SelectCharacterView( widget, Level.Level2 ) );
            } );
            view.OnLevel3( evt => {
                widget.View.AddView( CreateView_SelectCharacterView( widget, Level.Level3 ) );
            } );
            view.OnBack( evt => {
                widget.View.RemoveView( view );
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectCharacterView CreateView_SelectCharacterView(MenuMainWidget widget, Level level) {
            var view = new MainMenuWidgetView_SelectCharacterView();
            view.OnGray( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Gray ).Throw();
            } );
            view.OnRed( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Red ).Throw();
            } );
            view.OnGreen( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Green ).Throw();
            } );
            view.OnBlue( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Blue ).Throw();
            } );
            view.OnBack( evt => {
                widget.View.RemoveView( view );
            } );
            return view;
        }

    }
}
