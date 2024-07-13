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
        public Widget Widget { get; }
        public Toggle IsFullScreen { get; }
        public PopupField<object?> ScreenResolution { get; }
        public Toggle IsVSync { get; }

        public VideoSettingsWidgetView() {
            Widget = VisualElementFactory.Widget( "video-settings-widget" ).Classes( "grow-1" ).UserData( this ).Children(
                VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).Children(
                    IsFullScreen = VisualElementFactory.ToggleField( "Full Screen" ).Classes( "label-width-25pc" ),
                    ScreenResolution = VisualElementFactory.PopupField( "Screen Resolution" ).Classes( "label-width-25pc" ),
                    IsVSync = VisualElementFactory.ToggleField( "V-Sync" ).Classes( "label-width-25pc" )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
