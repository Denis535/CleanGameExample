#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class VideoSettingsWidget : UIWidgetBase<VideoSettingsWidgetView> {

        // App
        private Application2 Application { get; }
        private Storage.VideoSettings VideoSettings => Application.VideoSettings;
        // View
        public override VideoSettingsWidgetView View { get; }

        // Constructor
        public VideoSettingsWidget(IDependencyContainer container) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this, VideoSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        public override void OnActivate(object? argument) {
            ShowSelf();
        }
        public override void OnDeactivate(object? argument) {
            HideSelf();
            if (argument is DeactivateReason.Submit) {
                VideoSettings.IsFullScreen = View.IsFullScreen;
                VideoSettings.ScreenResolution = (Resolution) View.ScreenResolution.Value!;
                VideoSettings.IsVSync = View.IsVSync;
                VideoSettings.Save();
            } else {
                VideoSettings.Load();
            }
        }

        // OnDescendantActivate
        public override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        public override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static VideoSettingsWidgetView CreateView(VideoSettingsWidget widget, Storage.VideoSettings videoSettings) {
            var view = new VideoSettingsWidgetView() {
                IsFullScreen = videoSettings.IsFullScreen,
                ScreenResolution = (videoSettings.ScreenResolution, videoSettings.ScreenResolutions.Cast<object?>().ToList()),
                IsVSync = videoSettings.IsVSync
            };
            view.OnIsFullScreen( evt => {
                videoSettings.IsFullScreen = evt.newValue;
            } );
            view.OnScreenResolution( evt => {
                videoSettings.ScreenResolution = (Resolution) evt.newValue!;
            } );
            view.OnIsVSync( evt => {
                videoSettings.IsVSync = evt.newValue;
            } );
            return view;
        }

    }
}
