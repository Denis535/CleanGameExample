#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        private readonly VisualElement widget;
        private readonly Slider masterVolume;
        private readonly Slider musicVolume;
        private readonly Slider sfxVolume;
        private readonly Slider gameVolume;

        // Layer
        public override int Layer => 0;
        // Props
        public float MasterVolume {
            get => masterVolume.value;
            init => masterVolume.value = value;
        }
        public (float Min, float Max) MasterVolumeMinMax {
            get => masterVolume.GetMinMax();
            init => masterVolume.SetMinMax( value );
        }
        public float MusicVolume {
            get => musicVolume.value;
            init => musicVolume.value = value;
        }
        public (float Min, float Max) MusicVolumeMinMax {
            get => musicVolume.GetMinMax();
            init => musicVolume.SetMinMax( value );
        }
        public float SfxVolume {
            get => sfxVolume.value;
            init => sfxVolume.value = value;
        }
        public (float Min, float Max) SfxVolumeMinMax {
            get => sfxVolume.GetMinMax();
            init => sfxVolume.SetMinMax( value );
        }
        public float GameVolume {
            get => gameVolume.value;
            init => gameVolume.value = value;
        }
        public (float Min, float Max) GameVolumeMinMax {
            get => gameVolume.GetMinMax();
            init => gameVolume.SetMinMax( value );
        }
        // Events
        public event EventCallback<ChangeEvent<float>> OnMasterVolume {
            add => masterVolume.RegisterCallback( value );
            remove => masterVolume.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnMusicVolume {
            add => musicVolume.RegisterCallback( value );
            remove => musicVolume.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnSfxVolume {
            add => sfxVolume.RegisterCallback( value );
            remove => sfxVolume.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnGameVolume {
            add => gameVolume.RegisterCallback( value );
            remove => gameVolume.UnregisterCallback( value );
        }

        // Constructor
        public AudioSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.AudioSettings( out widget, out masterVolume, out musicVolume, out sfxVolume, out gameVolume );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
