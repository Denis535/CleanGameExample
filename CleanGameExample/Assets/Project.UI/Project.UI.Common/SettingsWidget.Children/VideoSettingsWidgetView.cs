#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class VideoSettingsWidgetView : UIViewBase {

        // View
        public ElementWrapper Group { get; }
        public ToggleFieldWrapper<bool> IsFullScreen { get; }
        public PopupFieldWrapper<object> ScreenResolution { get; }
        public ToggleFieldWrapper<bool> IsVSync { get; }

        // Constructor
        public VideoSettingsWidgetView(UIFactory factory) {
            VisualElement = factory.VideoSettingsWidget( out var group, out var isFullScreen, out var screenResolution, out var isVSync );
            Group = group.Wrap();
            IsFullScreen = isFullScreen.Wrap();
            ScreenResolution = screenResolution.Wrap();
            IsVSync = isVSync.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
