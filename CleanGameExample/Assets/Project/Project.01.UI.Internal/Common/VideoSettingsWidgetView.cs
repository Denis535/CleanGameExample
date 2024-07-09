#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly Toggle isFullScreen;
        private readonly PopupField<object?> screenResolution;
        private readonly Toggle isVSync;

        protected override VisualElement VisualElement => widget;
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
        public event EventCallback<ChangeEvent<bool>> OnIsFullScreenEvent {
            add => isFullScreen.RegisterCallback( value );
            remove => isFullScreen.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<object?>> OnScreenResolutionEvent {
            add => screenResolution.RegisterCallback( value );
            remove => screenResolution.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<bool>> OnIsVSyncEvent {
            add => isVSync.RegisterCallback( value );
            remove => isVSync.UnregisterCallback( value );
        }

        public VideoSettingsWidgetView() {
            VisualElementFactory_Common.VideoSettings( this, out widget, out isFullScreen, out screenResolution, out isVSync );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
