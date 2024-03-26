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
            //view.Title.Text = "Main Menu";
            view.PagesSlot.Push( CreateView_MainPage( widget, factory, router ) );
            view.PagesSlot.Peek().__GetVisualElement__().Focus2();
            return view;
        }
        private static MainMenuWidgetView_MainPage CreateView_MainPage(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_MainPage( factory );
            view.StartGame.OnClick( evt => {
                //widget.View.Title.Text = "Start Game";
                widget.View.PagesSlot.Peek().__GetVisualElement__().SaveFocus();
                widget.View.PagesSlot.Push( CreateView_StartGamePage( widget, factory, router ) );
                widget.View.PagesSlot.Peek().__GetVisualElement__().Focus2();
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
        private static MainMenuWidgetView_StartGamePage CreateView_StartGamePage(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_StartGamePage( factory );
            view.NewGame.OnClick( evt => {
                widget.View.PagesSlot.Peek().__GetVisualElement__().SaveFocus();
                widget.View.PagesSlot.Push( CreateView_SelectLevelPage( widget, factory, router ) );
                widget.View.PagesSlot.Peek().__GetVisualElement__().Focus2();
            } );
            view.Continue.OnClick( evt => {
                widget.View.PagesSlot.Peek().__GetVisualElement__().SaveFocus();
                widget.View.PagesSlot.Push( CreateView_SelectLevelPage( widget, factory, router ) );
                widget.View.PagesSlot.Peek().__GetVisualElement__().Focus2();
            } );
            view.Back.OnClick( evt => {
                //widget.View.Title.Text = "Main Menu";
                widget.View.PagesSlot.Pop();
                widget.View.PagesSlot.Peek().__GetVisualElement__().LoadFocus();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelPage CreateView_SelectLevelPage(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_SelectLevelPage( factory );
            view.Level1.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( World.World1 ).Throw();
            } );
            view.Level2.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( World.World2 ).Throw();
            } );
            view.Level3.OnClick( evt => {
                widget.AttachChild( new LoadingWidget() );
                router.LoadGameSceneAsync( World.World3 ).Throw();
            } );
            view.Back.OnClick( evt => {
                //widget.View.Title.Text = "Main Menu";
                widget.View.PagesSlot.Pop();
                widget.View.PagesSlot.Peek().__GetVisualElement__().LoadFocus();
            } );
            return view;
        }

    }
}
