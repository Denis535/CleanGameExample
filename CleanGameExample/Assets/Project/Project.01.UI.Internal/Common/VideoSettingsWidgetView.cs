#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Toggle isFullScreen;
        private readonly PopupField<object?> screenResolution;
        private readonly Toggle isVSync;

        // Props
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
        public event EventCallback<ChangeEvent<bool>> OnIsFullScreen {
            add => isFullScreen.RegisterCallback( value );
            remove => isFullScreen.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<object?>> OnScreenResolution {
            add => screenResolution.RegisterCallback( value );
            remove => screenResolution.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<bool>> OnIsVSync {
            add => isVSync.RegisterCallback( value );
            remove => isVSync.UnregisterCallback( value );
        }

        // Constructor
        public VideoSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.VideoSettings( out widget, out isFullScreen, out screenResolution, out isVSync );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
