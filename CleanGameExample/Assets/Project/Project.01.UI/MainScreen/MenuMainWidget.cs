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

    public class MenuMainWidget : UIWidgetBase2<MenuMainWidgetView> {

        // Container
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;
        // View
        public override MenuMainWidgetView View { get; }

        // Constructor
        public MenuMainWidget(IDependencyContainer container) : base( container ) {
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
            view.AddView( CreateView_MenuView( widget ) );
            return view;
        }
        private static MenuMainWidgetView_MenuView CreateView_MenuView(MenuMainWidget widget) {
            var view = new MenuMainWidgetView_MenuView();
            view.OnStartGame.Register( evt => {
                widget.View.AddView( CreateView_StartGameView( widget ) );
            } );
            view.OnSettings.Register( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.OnQuit.Register( evt => {
                var dialog = new DialogWidget( "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.Quit() ).OnCancel( "No", null );
                widget.AddChild( dialog );
            } );
            return view;
        }
        private static MenuMainWidgetView_StartGameView CreateView_StartGameView(MenuMainWidget widget) {
            var view = new MenuMainWidgetView_StartGameView();
            view.OnNewGame.Register( evt => {
                widget.View.AddView( CreateView_SelectLevelView( widget ) );
            } );
            view.OnContinue.Register( evt => {
                widget.View.AddView( CreateView_SelectLevelView( widget ) );
            } );
            view.OnBack.Register( evt => {
                widget.View.RemoveView( view );
            } );
            return view;
        }
        private static MenuMainWidgetView_SelectLevelView CreateView_SelectLevelView(MenuMainWidget widget) {
            var view = new MenuMainWidgetView_SelectLevelView();
            view.OnLevel1.Register( evt => {
                widget.View.AddView( CreateView_SelectCharacterView( widget, GameLevel.Level1 ) );
            } );
            view.OnLevel2.Register( evt => {
                widget.View.AddView( CreateView_SelectCharacterView( widget, GameLevel.Level2 ) );
            } );
            view.OnLevel3.Register( evt => {
                widget.View.AddView( CreateView_SelectCharacterView( widget, GameLevel.Level3 ) );
            } );
            view.OnBack.Register( evt => {
                widget.View.RemoveView( view );
            } );
            return view;
        }
        private static MenuMainWidgetView_SelectCharacterView CreateView_SelectCharacterView(MenuMainWidget widget, GameLevel level) {
            var view = new MenuMainWidgetView_SelectCharacterView();
            view.OnGray.Register( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Gray ).Throw();
            } );
            view.OnRed.Register( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Red ).Throw();
            } );
            view.OnGreen.Register( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Green ).Throw();
            } );
            view.OnBlue.Register( evt => {
                widget.AddChild( new LoadingMainWidget( widget.Container ) );
                widget.Router.LoadGameSceneAsync( level, widget.ProfileSettings.Name, PlayerCharacterKind.Blue ).Throw();
            } );
            view.OnBack.Register( evt => {
                widget.View.RemoveView( view );
            } );
            return view;
        }

    }
}
