#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class VideoSettingsWidget : UIWidgetBase<VideoSettingsWidgetView> {

        // Deps
        private Storage.VideoSettings VideoSettings { get; }

        // Constructor
        public VideoSettingsWidget() {
            VideoSettings = IDependencyContainer.Instance.RequireDependency<Storage.VideoSettings>( null );
            View = CreateView( this, VideoSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
            if (argument is DetachReason.Submit) {
                VideoSettings.IsFullScreen = View.IsFullScreen.Value;
                VideoSettings.ScreenResolution = (Resolution) View.ScreenResolution.Value!;
                VideoSettings.IsVSync = View.IsVSync.Value;
                VideoSettings.Save();
            } else {
                VideoSettings.Load();
            }
        }

        // Helpers
        private static VideoSettingsWidgetView CreateView(VideoSettingsWidget widget, Storage.VideoSettings videoSettings) {
            var view = new VideoSettingsWidgetView();
            view.Root.OnAttachToPanel( evt => {
                view.IsFullScreen.Value = videoSettings.IsFullScreen;
                view.ScreenResolution.ValueChoices = (videoSettings.ScreenResolution, videoSettings.ScreenResolutions.Cast<object?>().ToArray());
                view.IsVSync.Value = videoSettings.IsVSync;
            } );
            view.IsFullScreen.OnChange( evt => {
                videoSettings.IsFullScreen = evt.newValue;
            } );
            view.ScreenResolution.OnChange( evt => {
                videoSettings.ScreenResolution = (Resolution) evt.newValue!;
            } );
            view.IsVSync.OnChange( evt => {
                videoSettings.IsVSync = evt.newValue;
            } );
            return view;
        }

    }
}
