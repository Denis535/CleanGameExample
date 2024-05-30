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

    public class MainMenuWidget : UIWidgetBase<MainMenuWidgetView> {

        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;
        // View
        public override MainMenuWidgetView View { get; }

        // Constructor
        public MainMenuWidget(IDependencyContainer container) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this, container, Router, ProfileSettings );
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
        private static MainMenuWidgetView CreateView(MainMenuWidget widget, IDependencyContainer container, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView();
            view.Push( CreateView_MainMenuView( widget, container, router, profileSettings ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuView CreateView_MainMenuView(MainMenuWidget widget, IDependencyContainer container, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView_MainMenuView();
            view.OnStartGame( evt => {
                widget.View.Push( CreateView_StartGameView( widget, container, router, profileSettings ) );
            } );
            view.OnSettings( evt => {
                widget.AttachChild( new SettingsWidget( container ) );
            } );
            view.OnQuit( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.Quit() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            return view;
        }
        private static MainMenuWidgetView_StartGameView CreateView_StartGameView(MainMenuWidget widget, IDependencyContainer container, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView_StartGameView();
            view.OnNewGame( evt => {
                widget.View.Push( CreateView_SelectLevelView( widget, container, router, profileSettings ) );
            } );
            view.OnContinue( evt => {
                widget.View.Push( CreateView_SelectLevelView( widget, container, router, profileSettings ) );
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelView CreateView_SelectLevelView(MainMenuWidget widget, IDependencyContainer container, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView_SelectLevelView();
            view.OnLevel1( evt => {
                widget.View.Push( CreateView_SelectCharacterView( widget, container, router, profileSettings, LevelEnum.Level1 ) );
            } );
            view.OnLevel2( evt => {
                widget.View.Push( CreateView_SelectCharacterView( widget, container, router, profileSettings, LevelEnum.Level2 ) );
            } );
            view.OnLevel3( evt => {
                widget.View.Push( CreateView_SelectCharacterView( widget, container, router, profileSettings, LevelEnum.Level3 ) );
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectCharacterView CreateView_SelectCharacterView(MainMenuWidget widget, IDependencyContainer container, UIRouter router, Storage.ProfileSettings profileSettings, LevelEnum level) {
            var view = new MainMenuWidgetView_SelectCharacterView();
            view.OnGray( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterEnum.Gray ).Throw();
            } );
            view.OnRed( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterEnum.Red ).Throw();
            } );
            view.OnGreen( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterEnum.Green ).Throw();
            } );
            view.OnBlue( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterEnum.Blue ).Throw();
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }

    }
}
