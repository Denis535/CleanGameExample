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

    public class MainWidget : UIWidgetBase<MainWidgetView> {

        // UI
        private UIRouter Router { get; }
        // Application
        private Application2 Application { get; }
        // Storage
        private Storage Storage { get; set; } = default!;
        // AuthenticationService
        private IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;
        // View
        public override MainWidgetView View { get; }

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
            ShowSelf();
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
                    AttachChild( dialog );
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
                    AttachChild( dialog );
                    return;
                }
            }
            // Children
            AttachChild( new MainMenuWidget() );
        }
        public override void OnDetach(object? argument) {
            HideSelf();
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
            View.SetEffect( this.GetDescendants().Select( i => i.View ).OfType<UIViewBase>().FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() ) );
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