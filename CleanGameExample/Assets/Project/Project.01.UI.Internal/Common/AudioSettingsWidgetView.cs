#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        public Widget Widget { get; }
        public Slider MasterVolume { get; }
        public Slider MusicVolume { get; }
        public Slider SfxVolume { get; }
        public Slider GameVolume { get; }

        public AudioSettingsWidgetView() {
            Widget = VisualElementFactory.Widget( "audio-settings-widget" ).Classes( "grow-1" ).Children(
                VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).Children(
                    MasterVolume = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Classes( "label-width-25pc" ),
                    MusicVolume = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Classes( "label-width-25pc" ),
                    SfxVolume = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Classes( "label-width-25pc" ),
                    GameVolume = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Classes( "label-width-25pc" )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
