#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : UIViewBase {

        private readonly VisualElement widget;
        private readonly Toggle isFullScreen;
        private readonly PopupField<object?> screenResolution;
        private readonly Toggle isVSync;

        // Layer
        public override int Layer => 0;
        // Values
        public bool IsFullScreen {
            get => isFullScreen.value;
            init => isFullScreen.value = value;
        }
        public object? ScreenResolution {
            get => screenResolution.value;
            init => screenResolution.value = value;
        }
        public List<object?> ScreenResolutionChoices {
            get => screenResolution.choices;
            init => screenResolution.choices = value;
        }
        public bool IsVSync {
            get => isVSync.value;
            init => isVSync.value = value;
        }
        // Events
        public Observable<ChangeEvent<bool>> OnIsFullScreen => isFullScreen.AsObservable<ChangeEvent<bool>>();
        public Observable<ChangeEvent<object?>> OnScreenResolution => screenResolution.AsObservable<ChangeEvent<object?>>();
        public Observable<ChangeEvent<bool>> OnIsVSync => isVSync.AsObservable<ChangeEvent<bool>>();

        // Constructor
        public VideoSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.VideoSettings( out widget, out isFullScreen, out screenResolution, out isVSync );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
