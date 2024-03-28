#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class VideoSettingsWidget : UIWidgetBase<VideoSettingsWidgetView> {

        // Globals
        private UIFactory Factory { get; }
        private Globals.VideoSettings VideoSettings { get; }

        // Constructor
        public VideoSettingsWidget() {
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            VideoSettings = this.GetDependencyContainer().Resolve<Globals.VideoSettings>( null );
            View = CreateView( this, Factory, VideoSettings );
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
        private static VideoSettingsWidgetView CreateView(VideoSettingsWidget widget, UIFactory factory, Globals.VideoSettings videoSettings) {
            var view = new VideoSettingsWidgetView( factory );
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
