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

    public class MainWidget : UIWidgetBase2<MainWidgetView> {

        // Deps
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Storage Storage => Application.Storage;
        private IAuthenticationService AuthenticationService => Application.AuthenticationService;
        // View
        public override MainWidgetView View { get; }

        // Constructor
        public MainWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override async void OnActivate(object? argument) {
            try {
                ShowSelf();
                // await MainScene
                while (!Router.IsMainSceneLoaded) {
                    await Task.Yield();
                    DisposeCancellationToken.ThrowIfCancellationRequested();
                }
                // await UnityServices
                if (UnityServices.State != ServicesInitializationState.Initialized) {
                    try {
                        var options = new InitializationOptions();
                        if (Storage.Profile != null) options.SetProfile( Storage.Profile );
                        await UnityServices.InitializeAsync( options ).WaitAsync( DisposeCancellationToken );
                    } catch (Exception ex) {
                        var dialog = new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() );
                        AddChild( dialog );
                        return;
                    }
                }
                // await AuthenticationService
                if (!AuthenticationService.IsSignedIn) {
                    try {
                        var options = new SignInOptions();
                        options.CreateAccount = true;
                        await AuthenticationService.SignInAnonymouslyAsync( options ).WaitAsync( DisposeCancellationToken );
                    } catch (Exception ex) {
                        var dialog = new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() );
                        AddChild( dialog );
                        return;
                    }
                }
                // Children
                AddChild( new MenuWidget( Container ) );
            } catch (OperationCanceledException) {
            }
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

        // Update
        public void Update() {
            View.Effect = GetEffect( this );
        }
        public void LateUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

        // Helpers
        private static (Color Color, Vector2 Translate, float Rotate, float Scale) GetEffect(MainWidget widget) {
            var view = widget.Descendants.Where( i => i.IsViewable ).Select( i => i.View! ).FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() );
            // Menu
            if (view is MenuWidgetView menu) {
                view = menu.Views.FirstOrDefault( i => i.IsDisplayedInHierarchy() );
                if (view is MenuMainWidgetView_MenuView) {
                    return (Color.white, default, 0, 1.0f);
                }
                if (view is MenuMainWidgetView_StartGameView) {
                    return (Color.white, default, 1, 1.1f);
                }
                if (view is MenuMainWidgetView_SelectLevelView) {
                    return (Color.white, default, 2, 1.2f);
                }
                if (view is MenuMainWidgetView_SelectCharacterView) {
                    return (Color.white, default, 3, 1.3f);
                }
                return (Color.white, default, 3, 1.3f);
            }
            // Settings
            if (view is SettingsWidgetView settings) {
                if (settings.ProfileSettings!.IsDisplayedInHierarchy()) {
                    return (Color.white, default, 1, 1.1f);
                }
                if (settings.AudioSettings!.IsDisplayedInHierarchy()) {
                    return (Color.white, default, 1, 1.1f);
                }
                if (settings.VideoSettings!.IsDisplayedInHierarchy()) {
                    return (Color.white, default, 1, 1.1f);
                }
                return (Color.white, default, 1, 1.1f);
            }
            // Loading
            if (view is LoadingWidgetView loading) {
                return (Color.gray, default, 45, 2.5f);
            }
            return (Color.white, default, 0, 1.0f);
        }

    }
}
