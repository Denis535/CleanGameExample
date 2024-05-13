#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : UIViewBase {

        private readonly VisualElement view;
        private readonly Toggle isFullScreen;
        private readonly PopupField<object?> screenResolution;
        private readonly Toggle isVSync;

        // Props
        public bool IsFullScreen => isFullScreen.value;
        public object? ScreenResolution => screenResolution.value;
        public bool IsVSync => isVSync.value;

        // Constructor
        public VideoSettingsWidgetView(bool isFullScreen, (object? Value, object?[] Choices) screenResolution, bool isVSync) {
            VisualElement = VisualElementFactory_Common.VideoSettingsWidgetView( out view, out this.isFullScreen, out this.screenResolution, out this.isVSync );
            this.isFullScreen.value = isFullScreen;
            this.screenResolution.value = screenResolution.Value;
            this.screenResolution.choices = screenResolution.Choices.ToList();
            this.isVSync.value = isVSync;
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
