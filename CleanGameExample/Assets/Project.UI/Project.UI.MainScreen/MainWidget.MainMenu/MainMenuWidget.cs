#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class MainMenuWidget : UIWidgetBase<MainMenuWidgetView> {

        // Globals
        private UIFactory Factory { get; }
        private UIRouter Router { get; }
        // View
        protected override MainMenuWidgetView View { get; }

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
            {
                view.Title.Text = "Main Menu";
                view.MainPageSlot.IsDisplayed = true;
                view.StartGamePageSlot.IsDisplayed = false;
            }
            // MainPage
            view.MainPage_StartGame.OnClick( evt => {
                view.Title.Text = "Start Game";
                view.MainPageSlot.IsDisplayed = false;
                view.StartGamePageSlot.IsDisplayed = true;
            } );
            view.MainPage_Settings.OnClick( evt => {
                widget.AttachChild( new SettingsWidget() );
            } );
            view.MainPage_Quit.OnClick( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.Quit() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            // StartGamePage
            view.StartGamePage_NewGame.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync().Throw();
            } );
            view.StartGamePage_Continue.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync().Throw();
            } );
            view.StartGamePage_Back.OnClick( evt => {
                view.Title.Text = "Main Menu";
                view.MainPageSlot.IsDisplayed = true;
                view.StartGamePageSlot.IsDisplayed = false;
            } );
            return view;
        }

    }
}
