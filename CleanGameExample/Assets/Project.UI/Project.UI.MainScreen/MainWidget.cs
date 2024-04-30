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
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidget : UIWidgetBase<MainWidgetView> {

        // UI
        private UIRouter Router { get; }
        // Application
        private Application2 Application { get; }
        // Storage
        private Storage Storage { get; set; } = default!;
        // AuthenticationService
        private IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;

        // Constructor
        public MainWidget() {
            Router = Utils.Container.RequireDependency<UIRouter>( null );
            Application = Utils.Container.RequireDependency<Application2>( null );
            Storage = Utils.Container.RequireDependency<Storage>( null );
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

        // OnDescendantAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant, object? argument) {
            base.OnBeforeDescendantAttach( descendant, argument );
        }
        public override void OnAfterDescendantAttach(UIWidgetBase descendant, object? argument) {
            base.OnAfterDescendantAttach( descendant, argument );
        }
        public override void OnBeforeDescendantDetach(UIWidgetBase descendant, object? argument) {
            base.OnBeforeDescendantDetach( descendant, argument );
        }
        public override void OnAfterDescendantDetach(UIWidgetBase descendant, object? argument) {
            base.OnAfterDescendantDetach( descendant, argument );
        }

        // Update
        public void Update() {
            SetBackgroundEffect( View.Root, Descendants );
        }
        public void LateUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

        // Helpers
        private static void SetBackgroundEffect(ElementWrapper element, IReadOnlyList<UIWidgetBase> descendants) {
            var view = (UIViewBase?) descendants.Select( i => i.View ).OfType<UIViewBase>().FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() );
            view = view?.Children.FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() ) ?? view;
            // MainMenuWidgetView
            if (view is MainMenuWidgetView) {
                element.SetBackgroundEffect( Color.white, default, 0, 1.0f );
                return;
            }
            if (view is MainMenuWidgetView_MainMenuView) {
                element.SetBackgroundEffect( Color.white, default, 0, 1.0f );
                return;
            }
            if (view is MainMenuWidgetView_StartGameView) {
                element.SetBackgroundEffect( Color.white, default, 1, 1.1f );
                return;
            }
            if (view is MainMenuWidgetView_SelectLevelView) {
                element.SetBackgroundEffect( Color.white, default, 2, 1.2f );
                return;
            }
            if (view is MainMenuWidgetView_SelectYourCharacterView) {
                element.SetBackgroundEffect( Color.white, default, 3, 1.3f );
                return;
            }
            // SettingsWidgetView
            if (view is SettingsWidgetView) {
                element.SetBackgroundEffect( Color.white, default, 1, 1.1f );
                return;
            }
            if (view is ProfileSettingsWidgetView) {
                element.SetBackgroundEffect( Color.white, default, 1, 1.1f );
                return;
            }
            if (view is AudioSettingsWidgetView) {
                element.SetBackgroundEffect( Color.white, default, 1, 1.1f );
                return;
            }
            if (view is VideoSettingsWidgetView) {
                element.SetBackgroundEffect( Color.white, default, 1, 1.1f );
                return;
            }
            // LoadingWidgetView
            if (view is LoadingWidgetView) {
                element.SetBackgroundEffect( Color.gray, default, 45, 2.5f );
                return;
            }
        }

    }
}
