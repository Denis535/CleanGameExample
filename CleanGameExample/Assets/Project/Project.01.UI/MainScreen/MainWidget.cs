#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MainWidget : UIWidgetBase2<MainWidgetView> {

        private Application2 Application { get; }

        public MainWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
            AddChild( new MenuWidget( Container ) );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            ShowSelf();
            Children.OfType<MenuWidget>().First().View.SetDisplayed( false );
            try {
                await Application.RunAsync( DisposeCancellationToken );
            } catch (OperationCanceledException) {
            } catch (Exception ex) {
                Root.AddChild( new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", null ) );
            } finally {
                Children.OfType<MenuWidget>().First().View.SetDisplayed( true );
            }
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        public void OnUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

    }
}
