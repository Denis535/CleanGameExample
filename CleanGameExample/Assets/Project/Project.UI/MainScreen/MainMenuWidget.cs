#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.Entities;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MainMenuWidget : UIWidgetBase<MainMenuWidgetView> {

        // UI
        private UIRouter Router { get; }
        // View
        public override MainMenuWidgetView View { get; }

        // Constructor
        public MainMenuWidget() {
            Router = Utils.Container.RequireDependency<UIRouter>( null );
            View = CreateView( this, Router );
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
        private static MainMenuWidgetView CreateView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView();
            view.Push( CreateView_MainMenuView( widget, router ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuView CreateView_MainMenuView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView_MainMenuView();
            view.OnStartGame( evt => {
                widget.View.Push( CreateView_StartGameView( widget, router ) );
            } );
            view.OnSettings( evt => {
                widget.AttachChild( new SettingsWidget() );
            } );
            view.OnQuit( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.Quit() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            return view;
        }
        private static MainMenuWidgetView_StartGameView CreateView_StartGameView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView_StartGameView();
            view.OnNewGame( evt => {
                widget.View.Push( CreateView_SelectLevelView( widget, router ) );
            } );
            view.OnContinue( evt => {
                widget.View.Push( CreateView_SelectLevelView( widget, router ) );
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelView CreateView_SelectLevelView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView_SelectLevelView();
            view.OnLevel1( evt => {
                widget.View.Push( CreateView_SelectCharacterView( widget, router, LevelEnum.Level1 ) );
            } );
            view.OnLevel2( evt => {
                widget.View.Push( CreateView_SelectCharacterView( widget, router, LevelEnum.Level2 ) );
            } );
            view.OnLevel3( evt => {
                widget.View.Push( CreateView_SelectCharacterView( widget, router, LevelEnum.Level3 ) );
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectCharacterView CreateView_SelectCharacterView(MainMenuWidget widget, UIRouter router, LevelEnum level) {
            var view = new MainMenuWidgetView_SelectCharacterView();
            view.OnGray( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Gray ).Throw();
            } );
            view.OnRed( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Red ).Throw();
            } );
            view.OnGreen( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Green ).Throw();
            } );
            view.OnBlue( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Blue ).Throw();
            } );
            view.OnBack( evt => {
                widget.View.Pop();
            } );
            return view;
        }

    }
}
