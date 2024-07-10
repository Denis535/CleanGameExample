#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private Toggle IsFullScreen_ { get; }
        private PopupField<object?> ScreenResolution_ { get; }
        private Toggle IsVSync_ { get; }

        public bool IsFullScreen {
            get => IsFullScreen_.value;
            init => IsFullScreen_.value = value;
        }
        public object? ScreenResolution {
            get => ScreenResolution_.value;
            init => ScreenResolution_.value = value;
        }
        public List<object?> ScreenResolutionChoices {
            get => ScreenResolution_.choices;
            init => ScreenResolution_.choices = value;
        }
        public bool IsVSync {
            get => IsVSync_.value;
            init => IsVSync_.value = value;
        }
        public event EventCallback<ChangeEvent<bool>> OnIsFullScreenEvent {
            add => IsFullScreen_.RegisterCallback( value );
            remove => IsFullScreen_.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<object?>> OnScreenResolutionEvent {
            add => ScreenResolution_.RegisterCallback( value );
            remove => ScreenResolution_.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<bool>> OnIsVSyncEvent {
            add => IsVSync_.RegisterCallback( value );
            remove => IsVSync_.UnregisterCallback( value );
        }

        public VideoSettingsWidgetView() {
            Widget = VisualElementFactory.Widget( "video-settings-widget" ).Classes( "grow-1" ).UserData( this ).Children(
                VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).Children(
                    IsFullScreen_ = VisualElementFactory.ToggleField( "Full Screen" ).Classes( "label-width-25pc" ),
                    ScreenResolution_ = VisualElementFactory.PopupField( "Screen Resolution" ).Classes( "label-width-25pc" ),
                    IsVSync_ = VisualElementFactory.ToggleField( "V-Sync" ).Classes( "label-width-25pc" )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
