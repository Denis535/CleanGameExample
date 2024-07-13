#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidget : UIWidgetBase2<VideoSettingsWidgetView> {

        private Application2 Application { get; }
        private Storage.VideoSettings VideoSettings => Application.VideoSettings;

        public VideoSettingsWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
            if (argument is DeactivateReason.Submit) {
                VideoSettings.IsFullScreen = View.IsFullScreen.value;
                VideoSettings.ScreenResolution = (Resolution) View.ScreenResolution.value!;
                VideoSettings.IsVSync = View.IsVSync.value;
                VideoSettings.Save();
            } else {
                VideoSettings.Load();
            }
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static VideoSettingsWidgetView CreateView(VideoSettingsWidget widget) {
            var view = new VideoSettingsWidgetView();
            view.IsFullScreen.SetValue( widget.VideoSettings.IsFullScreen );
            view.ScreenResolution.SetValue( widget.VideoSettings.ScreenResolution, widget.VideoSettings.ScreenResolutions.Cast<object?>().ToList() );
            view.IsVSync.SetValue( widget.VideoSettings.IsVSync );
            view.IsFullScreen.RegisterCallback<ChangeEvent<bool>>( evt => {
                widget.VideoSettings.IsFullScreen = evt.newValue;
            } );
            view.ScreenResolution.RegisterCallback<ChangeEvent<object?>>( evt => {
                widget.VideoSettings.ScreenResolution = (Resolution) evt.newValue!;
            } );
            view.IsVSync.RegisterCallback<ChangeEvent<bool>>( evt => {
                widget.VideoSettings.IsVSync = evt.newValue;
            } );
            return view;
        }

    }
}
