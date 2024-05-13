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

        // Storage
        private Storage.VideoSettings VideoSettings { get; }
        // View
        public override VideoSettingsWidgetView View { get; }

        // Constructor
        public VideoSettingsWidget() {
            VideoSettings = Utils.Container.RequireDependency<Storage.VideoSettings>( null );
            View = CreateView( this, VideoSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            ShowSelf();
        }
        public override void OnDetach(object? argument) {
            HideSelf();
            if (argument is DetachReason.Submit) {
                VideoSettings.IsFullScreen = View.IsFullScreen;
                VideoSettings.ScreenResolution = (Resolution) View.ScreenResolution!;
                VideoSettings.IsVSync = View.IsVSync;
                VideoSettings.Save();
            } else {
                VideoSettings.Load();
            }
        }

        // Helpers
        private static VideoSettingsWidgetView CreateView(VideoSettingsWidget widget, Storage.VideoSettings videoSettings) {
            var view = new VideoSettingsWidgetView( videoSettings.IsFullScreen, (videoSettings.ScreenResolution, videoSettings.ScreenResolutions.Cast<object?>().ToArray()), videoSettings.IsVSync );
            view.OnIsFullScreenChange( evt => {
                videoSettings.IsFullScreen = evt.newValue;
            } );
            view.OnScreenResolutionChange( evt => {
                videoSettings.ScreenResolution = (Resolution) evt.newValue!;
            } );
            view.OnIsVSyncChange( evt => {
                videoSettings.IsVSync = evt.newValue;
            } );
            return view;
        }

    }
}
