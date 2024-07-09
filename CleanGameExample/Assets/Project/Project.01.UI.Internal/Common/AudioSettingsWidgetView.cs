#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly Slider masterVolume;
        private readonly Slider musicVolume;
        private readonly Slider sfxVolume;
        private readonly Slider gameVolume;

        protected override VisualElement VisualElement => widget;
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
        public event EventCallback<ChangeEvent<float>> OnMasterVolumeEvent {
            add => masterVolume.RegisterCallback( value );
            remove => masterVolume.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnMusicVolumeEvent {
            add => musicVolume.RegisterCallback( value );
            remove => musicVolume.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnSfxVolumeEvent {
            add => sfxVolume.RegisterCallback( value );
            remove => sfxVolume.UnregisterCallback( value );
        }
        public event EventCallback<ChangeEvent<float>> OnGameVolumeEvent {
            add => gameVolume.RegisterCallback( value );
            remove => gameVolume.UnregisterCallback( value );
        }

        public AudioSettingsWidgetView() {
            VisualElementFactory_Common.AudioSettings( this, out widget, out masterVolume, out musicVolume, out sfxVolume, out gameVolume );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
