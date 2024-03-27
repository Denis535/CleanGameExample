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
            view.PagesSlot.Push( CreateView_MainMenuPage( widget, factory, router ) );
            return view;
        }
        private static MainMenuWidgetView_MainMenuPage CreateView_MainMenuPage(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_MainMenuPage( factory );
            view.Scope.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Main Menu";
                view.Scope.__GetVisualElement__().Focus2();
                view.Scope.__GetVisualElement__().LoadFocus();
            } );
            view.Scope.OnDetachFromPanel( evt => {
                view.Scope.__GetVisualElement__().SaveFocus();
            } );
            view.StartGame.OnClick( evt => {
                widget.View.PagesSlot.Push( CreateView_StartGamePage( widget, factory, router ) );
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
            view.Scope.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Start Game";
                view.Scope.__GetVisualElement__().Focus2();
                view.Scope.__GetVisualElement__().LoadFocus();
            } );
            view.Scope.OnDetachFromPanel( evt => {
                view.Scope.__GetVisualElement__().SaveFocus();
            } );
            view.NewGame.OnClick( evt => {
                widget.View.PagesSlot.Push( CreateView_SelectLevelPage( widget, factory, router ) );
            } );
            view.Continue.OnClick( evt => {
                widget.View.PagesSlot.Push( CreateView_SelectLevelPage( widget, factory, router ) );
            } );
            view.Back.OnClick( evt => {
                widget.View.PagesSlot.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevelPage CreateView_SelectLevelPage(MainMenuWidget widget, UIFactory factory, UIRouter router) {
            var view = new MainMenuWidgetView_SelectLevelPage( factory );
            view.Scope.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Select Level";
                view.Scope.__GetVisualElement__().Focus2();
                view.Scope.__GetVisualElement__().LoadFocus();
            } );
            view.Scope.OnDetachFromPanel( evt => {
                view.Scope.__GetVisualElement__().SaveFocus();
            } );
            view.Level1.OnClick( evt => {
                widget.View.PagesSlot.Push( CreateView_SelectYourCharacter( widget, factory, router, World.World1 ) );
            } );
            view.Level2.OnClick( evt => {
                widget.View.PagesSlot.Push( CreateView_SelectYourCharacter( widget, factory, router, World.World1 ) );
            } );
            view.Level3.OnClick( evt => {
                widget.View.PagesSlot.Push( CreateView_SelectYourCharacter( widget, factory, router, World.World1 ) );
            } );
            view.Back.OnClick( evt => {
                widget.View.PagesSlot.Pop();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectYourCharacter CreateView_SelectYourCharacter(MainMenuWidget widget, UIFactory factory, UIRouter router, World world) {
            var view = new MainMenuWidgetView_SelectYourCharacter( factory );
            view.Scope.OnAttachToPanel( evt => {
                widget.View.Title.Text = "Select Your Character";
                view.Scope.__GetVisualElement__().Focus2();
                view.Scope.__GetVisualElement__().LoadFocus();
            } );
            view.Scope.OnDetachFromPanel( evt => {
                view.Scope.__GetVisualElement__().SaveFocus();
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
                widget.View.PagesSlot.Pop();
            } );
            return view;
        }

    }
}
