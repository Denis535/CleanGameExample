﻿#nullable enable
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

        // Container
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
            ShowSelf();
            // await MainScene
            while (Router.State != UIRouterState.MainSceneLoaded) {
                await Task.Yield();
            }
            // await UnityServices
            if (UnityServices.State != ServicesInitializationState.Initialized) {
                try {
                    var options = new InitializationOptions();
                    if (Storage.Profile != null) options.SetProfile( Storage.Profile );
                    await UnityServices.InitializeAsync( options );
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
                    await AuthenticationService.SignInAnonymouslyAsync( options );
                } catch (Exception ex) {
                    var dialog = new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() );
                    AddChild( dialog );
                    return;
                }
            }
            // Children
            AddChild( new MenuMainWidget( Container ) );
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
            View.SetBackgroundEffect( this.GetDescendants().Select( i => i.View ).OfType<UIViewBase>().FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() ) );
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
