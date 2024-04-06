#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class VideoSettingsWidgetView : UIViewBase {

        // View
        public ElementWrapper Root { get; }
        public ToggleFieldWrapper<bool> IsFullScreen { get; }
        public PopupFieldWrapper<object> ScreenResolution { get; }
        public ToggleFieldWrapper<bool> IsVSync { get; }

        // Constructor
        public VideoSettingsWidgetView() {
            VisualElement = UIFactory.Common.VideoSettingsWidget( this, out var root, out var isFullScreen, out var screenResolution, out var isVSync );
            Root = root.Wrap();
            IsFullScreen = isFullScreen.Wrap();
            ScreenResolution = screenResolution.Wrap();
            IsVSync = isVSync.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
