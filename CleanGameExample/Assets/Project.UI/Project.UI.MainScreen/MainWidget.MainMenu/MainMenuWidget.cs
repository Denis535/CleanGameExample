#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainMenuWidget : UIWidgetBase<MainMenuWidgetView> {

        // Globals
        private UIFactory Factory { get; }
        private UIRouter Router { get; }

        // Constructor
        public MainMenuWidget() {
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            Router = this.GetDependencyContainer().Resolve<UIRouter>( null );
            View = CreateView( this, Factory, Router );
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
        private static MainMenuWidgetView CreateView(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView( factory );
            view.ContentSlot.Push( CreateView_MainMenuView( widget, factory, router ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuView CreateView_MainMenuView(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_MainMenuView( factory );
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Main Menu";
            } );
            view.StartGame.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_StartGameView( widget, factory, router ) );
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
        private static MainMenuWidgetView_StartGameView CreateView_StartGameView(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_StartGameView( factory );
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Start Game";
            } );
            view.NewGame.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectLevelView( widget, factory, router ) );
            } );
            view.Continue.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectLevelView( widget, factory, router ) );
            } );
            view.Back.OnClick( evt => {
                widget.View.ContentSlot.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelView CreateView_SelectLevelView(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_SelectLevelView( factory );
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Select Level";
            } );
            view.Level1.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectYourCharacterView( widget, factory, router, World.World1 ) );
            } );
            view.Level2.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectYourCharacterView( widget, factory, router, World.World1 ) );
            } );
            view.Level3.OnClick( evt => {
                widget.View.ContentSlot.Push( CreateView_SelectYourCharacterView( widget, factory, router, World.World1 ) );
            } );
            view.Back.OnClick( evt => {
                widget.View.ContentSlot.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectYourCharacterView CreateView_SelectYourCharacterView(MainMenuWidget widget, UIFactory factory, UIRouter router, World world) {
            var view = new MainMenuWidgetView_SelectYourCharacterView( factory );
            view.Root.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Select Your Character";
            } );
            view.White.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( world, Character.White ).Throw();
            } );
            view.Red.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( world, Character.Red ).Throw();
            } );
            view.Green.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( world, Character.Green ).Throw();
            } );
            view.Blue.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( world, Character.Blue ).Throw();
            } );
            view.Back.OnClick( evt => {
                widget.View.ContentSlot.Pop();
            } );
            return view;
        }

    }
}
