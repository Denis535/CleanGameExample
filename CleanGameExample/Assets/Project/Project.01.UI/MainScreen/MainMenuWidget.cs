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
            View = CreateView( container, this, Router, ProfileSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        public override void OnActivate(object? argument) {
            ShowSelf();
        }
        public override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // OnDescendantActivate
        public override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static MainMenuWidgetView CreateView(IDependencyContainer container, MainMenuWidget widget, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView();
            view.Push( CreateView_MainMenuView( container, widget, router, profileSettings ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuView CreateView_MainMenuView(IDependencyContainer container, MainMenuWidget widget, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView_MainMenuView();
            view.OnStartGame( evt => {
                widget.View.Push( CreateView_StartGameView( container, widget, router, profileSettings ) );
            } );
            view.OnSettings( evt => {
                widget.AddChild( new SettingsWidget( container ) );
            } );
            view.OnQuit( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.Quit() ).OnCancel( "No", null );
                widget.AddChild( dialog );
            } );
            return view;
        }
        private static MainMenuWidgetView_StartGameView CreateView_StartGameView(IDependencyContainer container, MainMenuWidget widget, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView_StartGameView();
            view.OnNewGame( evt => {
                widget.View.Push( CreateView_SelectLevelView( container, widget, router, profileSettings ) );
            } );
            view.OnContinue( evt => {
                widget.View.Push( CreateView_SelectLevelView( container, widget, router, profileSettings ) );
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelView CreateView_SelectLevelView(IDependencyContainer container, MainMenuWidget widget, UIRouter router, Storage.ProfileSettings profileSettings) {
            var view = new MainMenuWidgetView_SelectLevelView();
            view.OnLevel1( evt => {
                widget.View.Push( CreateView_SelectCharacterView( container, widget, router, profileSettings, Level.Level1 ) );
            } );
            view.OnLevel2( evt => {
                widget.View.Push( CreateView_SelectCharacterView( container, widget, router, profileSettings, Level.Level2 ) );
            } );
            view.OnLevel3( evt => {
                widget.View.Push( CreateView_SelectCharacterView( container, widget, router, profileSettings, Level.Level3 ) );
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectCharacterView CreateView_SelectCharacterView(IDependencyContainer container, MainMenuWidget widget, UIRouter router, Storage.ProfileSettings profileSettings, Level level) {
            var view = new MainMenuWidgetView_SelectCharacterView();
            view.OnGray( evt => {
                widget.AddChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterKind.Gray ).Throw();
            } );
            view.OnRed( evt => {
                widget.AddChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterKind.Red ).Throw();
            } );
            view.OnGreen( evt => {
                widget.AddChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterKind.Green ).Throw();
            } );
            view.OnBlue( evt => {
                widget.AddChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, profileSettings.Name, PlayerCharacterKind.Blue ).Throw();
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }

    }
}
