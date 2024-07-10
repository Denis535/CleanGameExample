#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private Slider MasterVolume_ { get; }
        private Slider MusicVolume_ { get; }
        private Slider SfxVolume_ { get; }
        private Slider GameVolume_ { get; }

        public float MasterVolume {
            get => MasterVolume_.value;
            init => MasterVolume_.value = value;
        }
        public (float Min, float Max) MasterVolumeMinMax {
            get => MasterVolume_.GetMinMax();
            init => MasterVolume_.SetMinMax( value );
        }
        public float MusicVolume {
            get => MusicVolume_.value;
            init => MusicVolume_.value = value;
        }
        public (float Min, float Max) MusicVolumeMinMax {
            get => MusicVolume_.GetMinMax();
            init => MusicVolume_.SetMinMax( value );
        }
        public float SfxVolume {
            get => SfxVolume_.value;
            init => SfxVolume_.value = value;
        }
        public (float Min, float Max) SfxVolumeMinMax {
            get => SfxVolume_.GetMinMax();
            init => SfxVolume_.SetMinMax( value );
        }
        public float GameVolume {
            get => GameVolume_.value;
            init => GameVolume_.value = value;
        }
        public (float Min, float Max) GameVolumeMinMax {
            get => GameVolume_.GetMinMax();
            init => GameVolume_.SetMinMax( value );
        }
        public event EventCallback<ChangeEvent<float>> OnMasterVolumeEvent {
            add => MasterVolume_.RegisterCallback( value );
            remove => MasterVolume_.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnMusicVolumeEvent {
            add => MusicVolume_.RegisterCallback( value );
            remove => MusicVolume_.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnSfxVolumeEvent {
            add => SfxVolume_.RegisterCallback( value );
            remove => SfxVolume_.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnGameVolumeEvent {
            add => GameVolume_.RegisterCallback( value );
            remove => GameVolume_.UnregisterCallback( value );
        }

        public AudioSettingsWidgetView() {
            Widget = VisualElementFactory.Widget( "audio-settings-widget" ).Classes( "grow-1" ).UserData( this ).Children(
                VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).Children(
                    MasterVolume_ = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Classes( "label-width-25pc" ),
                    MusicVolume_ = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Classes( "label-width-25pc" ),
                    SfxVolume_ = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Classes( "label-width-25pc" ),
                    GameVolume_ = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Classes( "label-width-25pc" )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
