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
    using UnityEngine.UIElements;

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
            Show();
        }
        public override void OnDetach(object? argument) {
            Hide();
        }

        // Helpers
        private static MainMenuWidgetView CreateView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView();
            view.ContentSlot.Push( CreateView_MainMenuView( widget, router ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuView CreateView_MainMenuView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView_MainMenuView();
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Main Menu";
            } );
            view.StartGame.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_StartGameView( widget, router ) );
            } );
            view.Settings.OnClick( evt => {
                widget.AttachChild( new SettingsWidget() );
            } );
            view.Quit.OnClick( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.Quit() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            return view;
        }
        private static MainMenuWidgetView_StartGameView CreateView_StartGameView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView_StartGameView();
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Start Game";
            } );
            view.NewGame.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectLevelView( widget, router ) );
            } );
            view.Continue.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectLevelView( widget, router ) );
            } );
            view.Back.OnClick( evt => {
                widget.View.ContentSlot.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelView CreateView_SelectLevelView(MainMenuWidget widget, UIRouter router) {
            var view = new MainMenuWidgetView_SelectLevelView();
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Select Level";
            } );
            view.Level1.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectYourCharacterView( widget, router, LevelEnum.Level1 ) );
            } );
            view.Level2.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectYourCharacterView( widget, router, LevelEnum.Level2 ) );
            } );
            view.Level3.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectYourCharacterView( widget, router, LevelEnum.Level3 ) );
            } );
            view.Back.OnClick( evt => {
                widget.View.ContentSlot.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectYourCharacterView CreateView_SelectYourCharacterView(MainMenuWidget widget, UIRouter router, LevelEnum level) {
            var view = new MainMenuWidgetView_SelectYourCharacterView();
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Select Your Character";
            } );
            view.Gray.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Gray ).Throw();
            } );
            view.Red.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Red ).Throw();
            } );
            view.Green.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Green ).Throw();
            } );
            view.Blue.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( level, PlayerCharacterEnum.Blue ).Throw();
            } );
            view.Back.OnClick( evt => {
                widget.View.ContentSlot.Pop();
            } );
            return view;
        }

    }
}
