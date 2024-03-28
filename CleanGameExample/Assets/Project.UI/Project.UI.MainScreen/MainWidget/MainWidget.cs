#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.App;
    using Project.UI.Common;
    using Unity.Services.Authentication;
    using Unity.Services.Core;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class MainWidget : UIWidgetBase<MainWidgetView> {

        // Globals
        private UIRouter Router { get; }
        private UIFactory Factory { get; }
        private Application2 Application { get; }
        private Globals Globals { get; set; } = default!;
        private IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;

        // Constructor
        public MainWidget() {
            Router = this.GetDependencyContainer().Resolve<UIRouter>( null );
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            Application = this.GetDependencyContainer().Resolve<Application2>( null );
            Globals = this.GetDependencyContainer().Resolve<Globals>( null );
            View = CreateView( this, Factory );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override async void OnAttach(object? argument) {
            UIFactory.OnViewAttach += OnViewAttach;
            UIFactory.OnViewDetach += OnViewDetach;
            // await MainScene
            if (!Router.IsMainSceneLoaded) {
                while (!Router.IsMainSceneLoaded) {
                    await Task.Yield();
                }
            }
            // await UnityServices
            if (UnityServices.State != ServicesInitializationState.Initialized) {
                try {
                    var options = new InitializationOptions();
                    if (Globals.Profile != null) options.SetProfile( Globals.Profile );
                    await UnityServices.InitializeAsync( options );
                } catch (Exception ex) {
                    var dialog = new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() );
                    this.AttachChild( dialog );
                    return;
                }
            }
            // await AuthenticationService
            if (!AuthenticationService.IsSignedIn) {
                try {
                    var options = new SignInOptions();
                    options.CreateAccount = true;
                    await AuthenticationService.SignInAnonymouslyAsync( options );
                } catch (Exception ex) {
                    var dialog = new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() );
                    this.AttachChild( dialog );
                    return;
                }
            }
            // Children
            this.AttachChild( new MainMenuWidget() );
        }
        public override void OnDetach(object? argument) {
            UIFactory.OnViewAttach -= OnViewAttach;
            UIFactory.OnViewDetach -= OnViewDetach;
        }

        // OnViewAttach
        private void OnViewAttach(UIViewBase view) {
            OnViewChanged();
        }
        private void OnViewDetach(UIViewBase view) {
            OnViewChanged();
        }

        // OnViewChanged
        private void OnViewChanged() {
            var root = (RootWidget2) Parent!;
            var widget = root.Widgets.LastOrDefault();
            if (widget is MainMenuWidget mainMenuWidget) {
                var view = mainMenuWidget.__GetView__().ContentSlot.Peek();
                if (view is MainMenuWidgetView_MainMenuView) {
                    View.SetEffect( Color.white, default, 0, 1.0f );
                    return;
                }
                if (view is MainMenuWidgetView_StartGameView) {
                    View.SetEffect( Color.white, default, 0, 1.1f );
                    return;
                }
                if (view is MainMenuWidgetView_SelectLevelView) {
                    View.SetEffect( Color.white, default, 0, 1.2f );
                    return;
                }
                if (view is MainMenuWidgetView_SelectYourCharacterView) {
                    View.SetEffect( Color.white, default, 0, 1.3f );
                    return;
                }
            }
            if (widget is LoadingWidget) {
                View.SetEffect( Color.gray, default, 45, 2.5f );
                return;
            }
            if (widget is SettingsWidget) {
                View.SetEffect( Color.white, default, 0, 1.1f );
                return;
            }
            View.SetEffect( Color.white, default, 0, 1 );
        }

        // Update
        public void Update() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget, UIFactory factory) {
            var view = new MainWidgetView( factory );
            return view;
        }

    }
}
