#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class VideoSettingsWidget : UIWidgetBase2<VideoSettingsWidgetView> {

        // Container
        private Application2 Application { get; }
        private Storage.VideoSettings VideoSettings => Application.VideoSettings;
        // View
        public override VideoSettingsWidgetView View { get; }

        // Constructor
        public VideoSettingsWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
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
            var view = new VideoSettingsWidgetView() {
                IsFullScreen = widget.VideoSettings.IsFullScreen,
                ScreenResolution = (widget.VideoSettings.ScreenResolution, widget.VideoSettings.ScreenResolutions.Cast<object?>().ToList()),
                IsVSync = widget.VideoSettings.IsVSync
            };
            view.OnIsFullScreen( evt => {
                widget.VideoSettings.IsFullScreen = evt.newValue;
            } );
            view.OnScreenResolution( evt => {
                widget.VideoSettings.ScreenResolution = (Resolution) evt.newValue!;
            } );
            view.OnIsVSync( evt => {
                widget.VideoSettings.IsVSync = evt.newValue;
            } );
            return view;
        }

    }
}
