#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.App;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidget : UIWidgetBase2<MainWidgetView> {

        private UIRouter Router { get; }
        private Application2 Application { get; }

        public MainWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
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
            Children.OfType<MenuWidget>().First().__GetView__().style.display = DisplayStyle.None;
            try {
                await Application.InitializeTask.WaitAsync( DisposeCancellationToken );
                Children.OfType<MenuWidget>().First().__GetView__().style.display = DisplayStyle.Flex;
            } catch (OperationCanceledException) {
            } catch (Exception ex) {
                Root.AddChild( new ErrorDialogWidget( "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() ) );
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
