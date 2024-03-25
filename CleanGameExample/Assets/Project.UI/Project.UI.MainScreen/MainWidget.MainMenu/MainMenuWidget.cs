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
    using UnityEngine.UIElements;

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
                view.MainPage.SetActive();
            }
            // MainPage
            view.MainPage.StartGame.OnClick( evt => {
                view.Title.Text = "Start Game";
                view.MainPage.__GetVisualElement__().SaveFocus();
                view.StartGamePage.SetActive();
                view.StartGamePage.__GetVisualElement__().Focus2();
            } );
            view.MainPage.Settings.OnClick( evt => {
                widget.AttachChild( new SettingsWidget() );
            } );
            view.MainPage.Quit.OnClick( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => router.Quit() ).OnCancel( "No", null );
                widget.AttachChild( dialog );
            } );
            // StartGamePage
            view.StartGamePage.NewGame.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync().Throw();
            } );
            view.StartGamePage.Continue.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync().Throw();
            } );
            view.StartGamePage.Back.OnClick( evt => {
                view.Title.Text = "Main Menu";
                view.MainPage.SetActive();
                view.MainPage.__GetVisualElement__().LoadFocus();
            } );
            return view;
        }

    }
}
