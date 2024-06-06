#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : UIViewBase {

        private readonly VisualElement view;
        private readonly Toggle isFullScreen;
        private readonly PopupField<object?> screenResolution;
        private readonly Toggle isVSync;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;
        public bool IsFullScreen {
            get => isFullScreen.value;
            init => isFullScreen.value = value;
        }
        public (object? Value, List<object?>) ScreenResolution {
            get => screenResolution.GetValueChoices();
            init => screenResolution.SetValueChoices( value );
        }
        public bool IsVSync {
            get => isVSync.value;
            init => isVSync.value = value;
        }

        // Constructor
        public VideoSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.VideoSettingsWidgetView( out view, out isFullScreen, out screenResolution, out isVSync );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnIsFullScreen(EventCallback<ChangeEvent<bool>> callback) {
            isFullScreen.OnChange( callback );
        }
        public void OnScreenResolution(EventCallback<ChangeEvent<object?>> callback) {
            screenResolution.OnChange( callback );
        }
        public void OnIsVSync(EventCallback<ChangeEvent<bool>> callback) {
            isVSync.OnChange( callback );
        }

    }
}
