#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.App;
    using Unity.Services.Authentication;
    using Unity.Services.Core;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidget : UIWidgetBase<MainWidgetView> {

        // Deps
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Storage Storage { get; set; } = default!;
        private IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;

        // Constructor
        public MainWidget() {
            Router = IDependencyContainer.Instance.RequireDependency<UIRouter>( null );
            Application = IDependencyContainer.Instance.RequireDependency<Application2>( null );
            Storage = IDependencyContainer.Instance.RequireDependency<Storage>( null );
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override async void OnAttach(object? argument) {
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
                    if (Storage.Profile != null) options.SetProfile( Storage.Profile );
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
        }

        // Update
        public void Update() {
            var widget = Descendants.FirstOrDefault( i => i.__GetView__()?.__GetVisualElement__().IsAttached() ?? false );
            if (widget is MainMenuWidget mainMenuWidget) {
                var view = mainMenuWidget.__GetView__().ContentSlot.Peek();
                if (view is MainMenuWidgetView_MainMenuView) {
                    View.SetEffect( Color.white, default, 0, 1.0f );
                    return;
                }
                if (view is MainMenuWidgetView_StartGameView) {
                    View.SetEffect( Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is MainMenuWidgetView_SelectLevelView) {
                    View.SetEffect( Color.white, default, 2, 1.2f );
                    return;
                }
                if (view is MainMenuWidgetView_SelectYourCharacterView) {
                    View.SetEffect( Color.white, default, 3, 1.3f );
                    return;
                }
            }
            if (widget is LoadingWidget) {
                View.SetEffect( Color.gray, default, 45, 2.5f );
                return;
            }
            if (widget is SettingsWidget) {
                View.SetEffect( Color.white, default, 1, 1.1f );
                return;
            }
            View.SetEffect( Color.white, default, 0, 1 );
        }
        public void LateUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

    }
}
